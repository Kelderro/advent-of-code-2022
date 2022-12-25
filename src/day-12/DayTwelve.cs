
using System.Text;
/// <summary>
/// Day 12: Hill Climbing Algorithm
/// https://adventofcode.com/2022/day/12.
/// </summary>
namespace Aoc.Year2022.Day12;

public sealed class DayTwelve : IDay<int>
{
    public static int PartOne(string[] lines)
    {
        var leastSteps = 0;

        // S = Current position (elevation always a)
        // E = Location with best signal (elevation always z)
        var map = CreateHeightMap(lines);

        var itemCovered = new HashSet<int>();
        var queue = new Queue<CellInfo>();
        queue.Enqueue(new CellInfo(map.StartPosition, 0));
        Console.Clear();
        Console.CursorVisible = true;

        PrintMap(map);

        while (queue.Count > 0)
        {
            var cellInfo = queue.Dequeue();
            var cellIndex = cellInfo.Index;

            if (itemCovered.Contains(cellIndex))
                continue;

            itemCovered.Add(cellIndex);

            leastSteps = cellInfo.StepNumber;

            // Cell above
            var cellValue = map.Grid[cellIndex];

            UpdateCell(cellIndex, map);

            // Check if there is a cell above the element
            var cellUpIndex = cellIndex - map.ColumnCount;
            if (cellUpIndex > 0)
            {
                var cellUpValue = map.Grid[cellUpIndex];
                var offSet = cellUpValue - cellValue + 0;
                if (offSet == 0 || offSet == 1)
                {
                    queue.Enqueue(new CellInfo(cellUpIndex, cellInfo.StepNumber + 1));
                }
            }

            // Cell on the right
            var cellRightIndex = cellIndex + 1;
            // Check if the cell on the right is not on the next line
            if (cellRightIndex % map.ColumnCount != 0)
            {
                var cellRightValue = map.Grid[cellRightIndex];
                var offSet = cellRightValue - cellValue + 0;
                if (offSet == 0 || offSet == 1)
                {
                    queue.Enqueue(new CellInfo(cellRightIndex, cellInfo.StepNumber + 1));
                }
            }

            // Cell under
            var cellDownIndex = cellIndex + map.ColumnCount;
            // Check if the cell under it is in range
            if (cellDownIndex < map.Grid.Length)
            {
                var cellDownValue = map.Grid[cellDownIndex];
                var offSet = cellDownValue - cellValue + 0;
                if (offSet == 0 || offSet == 1)
                {
                    queue.Enqueue(new CellInfo(cellDownIndex, cellInfo.StepNumber + 1));
                }
            }

            // Cell on the left
            var cellLeftIndex = cellIndex - 1;
            var cellRowNumber = Math.Floor((decimal)cellIndex / map.ColumnCount);
            var cellLeftRowNumber = Math.Floor((decimal)cellLeftIndex / map.ColumnCount);
            // Check if the cell on the left is on the same row
            if (cellLeftIndex >= 0
             && cellRowNumber == cellLeftRowNumber)
            {
                var cellLeftValue = map.Grid[cellLeftIndex];
                var offSet = cellLeftValue - cellValue + 0;
                if (offSet == 0 || offSet == 1)
                {
                    queue.Enqueue(new CellInfo(cellLeftIndex, cellInfo.StepNumber + 1));
                }
            }
        }

        PrintItemsCovered(itemCovered, map);
        Console.CursorVisible = true;

        Console.WriteLine();
        Console.WriteLine();

        return leastSteps;
    }

    private static void PrintMap(HeightMap map)
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
    }

    private static void UpdateCell(int cellIndex, HeightMap map)
    {
        Console.CursorVisible = false;
        var column = cellIndex % map.ColumnCount;
        var row = (int)Math.Floor((float)cellIndex / map.ColumnCount);
        Console.SetCursorPosition(column, row);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(map.Grid[cellIndex]);
        Thread.Sleep(5);
        Console.CursorVisible = true;
    }

    private static void PrintItemsCovered(HashSet<int> itemCovered, HeightMap map)
    {
        Console.SetCursorPosition(0, 0);
        for (var i = 0; i < map.RowCount * map.ColumnCount; i++)
        {
            if (i % map.ColumnCount == 0)
            {
                Console.WriteLine();
            }

            Console.ForegroundColor = itemCovered.Contains(i)
                ? ConsoleColor.DarkGreen
                : ConsoleColor.DarkGray;
            Console.Write(map.Grid[i]);
        }
    }

    public static int PartTwo(string[] lines)
    {
        return 0;
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
                var gridPosition = row * lines[0].Length + column;

                if (chr == 'S')
                {
                    startPosition = gridPosition;
                    chr = 'a';
                }

                if (chr == 'E')
                {
                    bestSignalPosition = gridPosition;
                    chr = '{';
                }

                if (chr == '!')
                {
                    Console.WriteLine();
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

        required public int StartPosition { get; init; }

        required public int BestSignalPosition { get; init; }

        required public char[] Grid { get; init; }
    }
}
