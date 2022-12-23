/// <summary>
/// Day 9: Rope Bridge
/// https://adventofcode.com/2022/day/9
/// </summary>

using System.Drawing;

namespace Aoc.Year2022.Day09;

public abstract class DayNine : IDay<int>
{
    public static int PartOne(string[] lines)
    {
        var moves = new HashSet<Point>();
        var head = new Point();
        var tail = new Point();

        moves.Add(tail);

        foreach (var line in lines)
        {
            var lineSplit = line.Split(" ");
            var direction = lineSplit[0][0];
            var steps = int.Parse(lineSplit[1]);

            switch (direction)
            {
                case 'U':
                    MoveUp(moves, steps, ref head, ref tail);
                    break;
                case 'R':
                    MoveRight(moves, steps, ref head, ref tail);
                    break;
                case 'D':
                    MoveDown(moves, steps, ref head, ref tail);
                    break;
                case 'L':
                    MoveLeft(moves, steps, ref head, ref tail);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), $"The direction value '{direction}' is out of range");
            }
        }

        PrintMoves(moves);

        return moves.Count;
    }

    private static void PrintMoves(HashSet<Point> moves)
    {
        var highest = new Point(0, 0);
        var lowest = new Point(0, 0);

        foreach (var move in moves)
        {
            lowest.X = int.Min(lowest.X, move.X);
            lowest.Y = int.Min(lowest.Y, move.Y);
            highest.X = int.Max(highest.X, move.X);
            highest.Y = int.Max(highest.Y, move.Y);
        }

        for (var y = lowest.Y; y <= highest.Y; y++)
        {
            for (var x = lowest.X; x <= highest.X; x++)
            {
                if (moves.Contains(new Point(x, y)))
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write('.');
                }
            }

            Console.WriteLine(String.Empty);
        }

    }

    private static void MoveUp(HashSet<Point> moves, int steps, ref Point head, ref Point tail)
    {
        for (var i = 0; i < steps; i++)
        {
            head.Y--;

            if (!IsAdjacent(head, tail))
            {
                tail.Y = head.Y;
                tail.X = head.X;

                tail.Y += 1;
                moves.Add(tail);
            }
        }
    }

    private static void MoveRight(HashSet<Point> moves, int steps, ref Point head, ref Point tail)
    {
        for (var i = 0; i < steps; i++)
        {
            head.X++;

            if (!IsAdjacent(head, tail))
            {
                tail.Y = head.Y;
                tail.X = head.X;

                tail.X -= 1;
                moves.Add(tail);
            }
        }
    }

    private static void MoveDown(HashSet<Point> moves, int steps, ref Point head, ref Point tail)
    {
        for (var i = 0; i < steps; i++)
        {
            head.Y++;

            if (!IsAdjacent(head, tail))
            {
                tail.Y = head.Y;
                tail.X = head.X;

                tail.Y -= 1;
                moves.Add(tail);
            }
        }
    }

    private static void MoveLeft(HashSet<Point> moves, int steps, ref Point head, ref Point tail)
    {
        for (var i = 0; i < steps; i++)
        {
            head.X--;

            if (!IsAdjacent(head, tail))
            {
                tail.Y = head.Y;
                tail.X = head.X;

                tail.X += 1;
                moves.Add(tail);
            }
        }
    }

    private static bool IsAdjacent(Point head, Point tail)
    {
        // Tail on the same column but one position to the left or right
        if (head.Y == tail.Y && Math.Abs(head.X - tail.X) <= 1)
        {
            return true;
        }

        // Tail on same line but one position up or down
        if (head.X == tail.X && Math.Abs(head.Y - tail.Y) <= 1)
        {
            return true;
        }

        // Diagonally
        var xAbs = Math.Abs(head.X - tail.X);
        var yAbs = Math.Abs(head.Y - tail.Y);

        return xAbs == 1 && yAbs == 1;
    }

    public static int PartTwo(string[] lines)
    {
        return 0;
    }
}