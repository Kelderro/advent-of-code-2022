
using System.Collections.ObjectModel;
/// <summary>
/// Day 12: Hill Climbing Algorithm
/// https://adventofcode.com/2022/day/12.
/// </summary>
namespace Aoc.Year2022.Day12;

public sealed class DayTwelve : IDay<int>
{
    public static int PartOne(string[] lines)
    {
        // input = heightmap
        // S = Current position (elevation always a)
        // E = Location with best signal (elevation always z)
        var grid = CreateGrid(lines);

        var visisted = new List<int>();
        var path = new List<int>();
        var solutions = new List<IList<int>>();



        // Backtrack - Optimization problem
        //BackTrack(grid, lines[0].Length, 3220, visisted, path, solutions);

        return path.Count;
    }

    public static int PartTwo(string[] lines)
    {
        return 0;
    }
    /*
        private static bool BackTrack(char[,] grid, int columnCount, int position, IList<int> visisted, IList<int> path, List<IList<int>> solutions)
        {
            // ReportPath(grid, path);
            Console.WriteLine(path.Count);

            var positionChar = grid[position];

            // Console.WriteLine($"Visiting cell with value {positionChar} position: {position}");

            // Starting position (S) has elvation a
            if (positionChar == 'S')
            {
                positionChar = 'a';
            }

            // Best signal location (E) has elvation z
            if (positionChar == 'E')
            {
                positionChar = 'z';
            }

            // Step to big. Return
            if (path.Any() &&
                (Math.Abs(positionChar - grid[path.Last()]) > 1))
            {
                // Console.WriteLine("Out of range character");
                return false;
            }

            if (positionChar == 'z')
            {
                path.Add(position);
                solutions.Add(path);
                return true;
            }

            if (visisted.Contains(position))
            {
                // Console.WriteLine("Already visisted this cell");
                return false;
            }

            visisted.Add(position);

            // Check if we can navigate a cell upwards
            var upCellPosition = position - columnCount;
            if (upCellPosition >= 0)
            {
                path.Add(position);
                // Console.WriteLine("Moving up");
                if (BackTrack(grid, columnCount, upCellPosition, visisted, path, solutions))
                {
                    return true;
                }

                path.Remove(position);
            }

            // Check if we can navigate a cell to the right
            var rightCellPosition = position + 1;
            if (rightCellPosition % columnCount != 0)
            {
                path.Add(position);
                // Console.WriteLine("Moving right");
                if (BackTrack(grid, columnCount, rightCellPosition, visisted, path, solutions))
                {
                    return true;
                }

                path.Remove(position);
            }

            // Check if we can navigate a cell downwards
            var downCellPosition = position + columnCount;
            if (downCellPosition < grid.Count)
            {
                path.Add(position);
                // Console.WriteLine("Moving down");
                if (BackTrack(grid, columnCount, downCellPosition, visisted, path, solutions))
                {
                    return true;
                }

                path.Remove(position);
            }

            // Check if we can navigate a cell to the left
            var leftCellPosition = position - 1;
            if (leftCellPosition > 0)
            {
                path.Add(position);
                // Console.WriteLine("Moving left");
                if (BackTrack(grid, columnCount, leftCellPosition, visisted, path, solutions))
                {
                    return true;
                }

                path.Remove(position);
            }

            // Console.WriteLine(string.Empty);
            return false;
        }

        private static void ReportPath(ReadOnlyCollection<char> grid, IList<int> solution)
        {
            for (var i = 0; i < solution.Count; i++)
            {
                Console.WriteLine($"{i} - {grid[solution[i]]} - {solution[i]}");
            }
        }
    */
    private static char[,] CreateGrid(string[] lines)
    {
        var totalRows = lines.Length;
        var totalColumns = lines[0].Length;

        var grid = new char[totalRows, totalColumns];

        for (var row = 0; row < lines.Length; row++)
        {
            var line = lines[row];

            for (var column = 0; column < lines[0].Length; column++)
            {
                var chr = line[column];
                if (chr == 'S')
                {
                    chr = 'a';
                }

                if (chr == 'E')
                {
                    chr = 'z';
                }

                grid[row, column] = chr;
            }
        }

        return grid;
    }
}
