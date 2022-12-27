/// <summary>
/// Day 12: Hill Climbing Algorithm
/// https://adventofcode.com/2022/day/12.
/// </summary>
namespace Aoc.Year2022.Day12;

using System.Text;

public sealed class DayTwelve : IDay<int>
{
    public static int PartOne(string[] lines)
    {
        // S = Current position (elevation always a)
        // E = Location with best signal (elevation always z)
        var map = CreateHeightMap(lines);
        return FindShortestPath(map);
    }

    public static int PartTwo(string[] lines)
    {
        // S = Current position (elevation always a)
        // E = Location with best signal (elevation always z)
        var map = CreateHeightMap(lines);
        var fewestSteps = int.MaxValue;

        for (var i = 0; i < map.Grid.Length; i++)
        {
            if (map.Grid[i] == 'a')
            {
                map.StartPosition = i;
                fewestSteps = int.Min(fewestSteps, FindShortestPath(map));
            }
        }

        return fewestSteps;
    }

    private static int FindShortestPath(HeightMap map)
    {
        var itemCovered = new HashSet<int>();
        var queue = new Queue<CellInfo>();
        queue.Enqueue(new CellInfo(map.StartPosition, 0));

        PrintMap(map);

        while (queue.Count > 0)
        {
            var cellInfo = queue.Dequeue();
            var cellIndex = cellInfo.Index;

            if (itemCovered.Contains(cellIndex))
            {
                continue;
            }

            // Keep track on the cells we have checked
            itemCovered.Add(cellIndex);

            var cellValue = map.Grid[cellIndex];

            // Found the highest point. No need to continue.
            if (cellIndex == map.BestSignalPosition)
            {
                ReportShowCellUpdate(cellInfo.StepNumber, cellIndex, map);
                Console.WriteLine();
                Console.WriteLine($"Best signal found after {cellInfo.StepNumber} steps.");

                return cellInfo.StepNumber;
            }

            ReportShowCellUpdate(cellInfo.StepNumber, cellIndex, map);

            ConsiderStepUp(map, queue, cellInfo, cellIndex, cellValue);
            ConsiderStepRight(map, queue, cellInfo, cellIndex, cellValue);
            ConsiderStepDown(map, queue, cellInfo, cellIndex, cellValue);
            ConsiderStepLeft(map, queue, cellInfo, cellIndex, cellValue);
        }

        return int.MaxValue;
    }

    private static void ConsiderCell(
            HeightMap map,
            Queue<CellInfo> queue,
            CellInfo cellInfo,
            char moveFromCellValue,
            int moveToCellIndex,
            bool canMakeMove)
    {
        if (canMakeMove)
        {
            var cellRightValue = map.Grid[moveToCellIndex];
            var offSet = cellRightValue - moveFromCellValue + 0;
            if (offSet <= 1)
            {
                queue.Enqueue(new CellInfo(moveToCellIndex, cellInfo.StepNumber + 1));
            }
        }
    }

    private static void ConsiderStepUp(HeightMap map, Queue<CellInfo> queue, CellInfo cellInfo, int cellIndex, char cellValue)
    {
        var moveToIndex = cellIndex - map.ColumnCount;
        var canMakeMove = moveToIndex >= 0;

        ConsiderCell(map, queue, cellInfo, cellValue, moveToIndex, canMakeMove);
    }

    private static void ConsiderStepRight(HeightMap map, Queue<CellInfo> queue, CellInfo cellInfo, int cellIndex, char cellValue)
    {
        var moveToIndex = cellIndex + 1;
        var canMakeMove = moveToIndex % map.ColumnCount != 0;

        ConsiderCell(map, queue, cellInfo, cellValue, moveToIndex, canMakeMove);
    }

    private static void ConsiderStepDown(HeightMap map, Queue<CellInfo> queue, CellInfo cellInfo, int cellIndex, char cellValue)
    {
        var moveToIndex = cellIndex + map.ColumnCount;
        var canMakeMove = moveToIndex < map.Grid.Length;

        ConsiderCell(map, queue, cellInfo, cellValue, moveToIndex, canMakeMove);
    }

    private static void ConsiderStepLeft(HeightMap map, Queue<CellInfo> queue, CellInfo cellInfo, int cellIndex, char cellValue)
    {
        var moveToIndex = cellIndex - 1;

        var currentCellRowNumber = Math.Floor((decimal)cellIndex / map.ColumnCount);
        var moveToColumnNumber = Math.Floor((decimal)moveToIndex / map.ColumnCount);

        var canMakeMove = moveToIndex >= 0
            && currentCellRowNumber == moveToColumnNumber;

        ConsiderCell(map, queue, cellInfo, cellValue, moveToIndex, canMakeMove);
    }

    private static void PrintMap(HeightMap map)
    {
        try
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            var sb = new StringBuilder();
            for (var i = 0; i < map.RowCount * map.ColumnCount; i++)
            {
                if (i % map.ColumnCount == 0 && i != 0)
                {
                    sb.AppendLine();
                }

                sb.Append(map.Grid[i]);
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(sb.ToString());
            Console.ResetColor();
        }
        catch (System.IO.IOException ex)
        {
            // Swollow exception only when the error message match
            // This exception happens on the build server
            // as for example Console.Clear is not supported on a Windows machine.
            if (!ex.Message.Equals("The handle is invalid.", StringComparison.InvariantCultureIgnoreCase))
            {
                // Throw as the error message does not match
                throw;
            }
        }
    }

    private static void ReportShowCellUpdate(int stepNumber, int cellIndex, HeightMap map)
    {
        try
        {
            Console.CursorVisible = false;
            var column = cellIndex % map.ColumnCount;
            var row = (int)Math.Floor((float)cellIndex / map.ColumnCount);
            Console.SetCursorPosition(column, row);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(map.Grid[cellIndex]);
            Console.ResetColor();

            Console.SetCursorPosition(0, map.RowCount + 1);
            Console.WriteLine($"Number of steps taken: {stepNumber}.");
            Console.CursorVisible = true;
        }
        catch (System.IO.IOException ex)
        {
            // Swollow exception only when the error message match
            // This exception happens on the build server
            // as for example Console.Clear is not supported on a Windows machine.
            if (!ex.Message.Equals("System.IO.IOException : The handle is invalid.", StringComparison.InvariantCultureIgnoreCase))
            {
                // Throw as the error message does not match
                throw;
            }
        }
    }

    private static HeightMap CreateHeightMap(string[] lines)
    {
        // Int is an struct and must contain a value. The default value would be 0 but this is a valid
        // location in the grid. Assigning int.MinValue to be able to check if a certain position was
        // not found in the grid.
        var startPosition = int.MinValue;
        var bestSignalPosition = int.MinValue;

        var totalRows = lines.Length;
        var totalColumns = lines[0].Length;

        var grid = new char[totalRows * totalColumns];

        for (var row = 0; row < lines.Length; row++)
        {
            for (var column = 0; column < lines[0].Length; column++)
            {
                var chr = lines[row][column];
                var gridPosition = (row * lines[0].Length) + column;

                if (chr == 'S')
                {
                    startPosition = gridPosition;
                    chr = 'a';
                }

                if (chr == 'E')
                {
                    bestSignalPosition = gridPosition;
                    chr = 'z';
                }

                grid[gridPosition] = chr;
            }
        }

        var invalidGrid = "The provided grid is not valid as there is no {0} location." +
            " The {0} location is indicated with the letter '{1}'.";

        if (startPosition == int.MinValue)
        {
            throw new NotSupportedException(string.Format(invalidGrid, "starting", "S"));
        }

        if (bestSignalPosition == int.MinValue)
        {
            throw new NotSupportedException(string.Format(invalidGrid, "best signal", "E"));
        }

        return new HeightMap
        {
            RowCount = lines.Length,
            ColumnCount = lines[0].Length,
            StartPosition = startPosition,
            BestSignalPosition = bestSignalPosition,
            Grid = grid,
        };
    }

    public class CellInfo
    {
        public CellInfo(int index, int stepNumber)
        {
            this.Index = index;
            this.StepNumber = stepNumber;
        }

        public int Index { get; init; }

        public int StepNumber { get; set; }
    }

    public class HeightMap
    {
        required public int RowCount { get; init; }

        required public int ColumnCount { get; init; }

        required public int StartPosition { get; set; }

        required public int BestSignalPosition { get; init; }

        required public char[] Grid { get; init; }
    }
}
