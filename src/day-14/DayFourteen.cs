
using System.Drawing;
/// <summary>
/// Day 14: Regolith Reservoir
/// https://adventofcode.com/2022/day/14.
/// </summary>
namespace Aoc.Year2022.Day14;

public abstract class DayFourteen : IDay<int>
{
    public static int PartOne(string[] lines)
    {
        var rockPaths = Parse(lines);



        DisplayGrid(rockPaths);

        return 0;
    }

    public static int PartTwo(string[] lines)
    {
        throw new NotImplementedException();
    }

    private static IList<IList<Point>> Parse(string[] lines)
    {
        var linesOfRock = new List<IList<Point>>();

        foreach (var line in lines)
        {
            linesOfRock.Add(ParseLine(line));
        }

        return linesOfRock;
    }

    private static IList<Point> ParseLine(string line)
    {
        var rockPath = new List<Point>();

        foreach (var rock in line.Split(" -> "))
        {
            var rockCoordinates = rock.Split(',');
            var pointX = int.Parse(rockCoordinates[0]);
            var pointY = int.Parse(rockCoordinates[1]);
            rockPath.Add(new Point(pointX, pointY));
        }

        return rockPath;
    }

    private static void DisplayGrid(IList<IList<Point>> rockPaths)
    {
        // Find the max and min coordinates of X and Y
        var minPoint = new Point(500, 0); // Sand starts falling down from 500,0
        var maxPoint = new Point(rockPaths.First().First().X, rockPaths.First().First().Y);

        foreach (var rockPath in rockPaths)
        {
            foreach (var rockPosition in rockPath)
            {
                minPoint.X = int.Min(minPoint.X, rockPosition.X);
                maxPoint.X = int.Max(maxPoint.X, rockPosition.X);

                maxPoint.Y = int.Max(maxPoint.Y, rockPosition.Y);
            }
        }

        for (var i = 0; i < 3; i++)
        {
            Console.WriteLine();
            Console.Write("    ");
            for (var x = minPoint.X; x <= maxPoint.X; x++)
            {
                Console.Write(x.ToString()[i]);
            }
        }

        // Find the highest Y coordinate
        for (var y = minPoint.Y; y <= maxPoint.Y + 1; y++)
        {
            Console.WriteLine();
            Console.Write($"{y,3} ");
            for (var x = minPoint.X; x <= maxPoint.X; x++)
            {
                if (x == 500 && y == 0)
                {
                    Console.Write("+");
                    continue;
                }
                Console.Write(".");
            }
        }
    }
}
