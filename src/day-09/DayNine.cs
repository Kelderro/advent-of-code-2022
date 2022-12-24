/// <summary>
/// Day 9: Rope Bridge
/// https://adventofcode.com/2022/day/9
/// </summary>

namespace Aoc.Year2022.Day09;

using System.Drawing;
using System.Text;

public abstract class DayNine : IDay<int>
{
    private delegate void MoveAction(ref Point point);

    public static int PartOne(string[] lines)
    {
        var moves = new HashSet<Point>();
        var rope = new Point[2];

        // Add the tail
        moves.Add(rope.Last());

        foreach (var line in lines)
        {
            var lineSplit = line.Split(" ");
            var direction = lineSplit[0][0];
            var steps = int.Parse(lineSplit[1]);

            switch (direction)
            {
                case 'U':
                    Move(moves, steps, rope, (ref Point h) => h.Y--);
                    break;
                case 'R':
                    Move(moves, steps, rope, (ref Point h) => h.X++);
                    break;
                case 'D':
                    Move(moves, steps, rope, (ref Point h) => h.Y++);
                    break;
                case 'L':
                    Move(moves, steps, rope, (ref Point h) => h.X--);
                    break;
                default:
                    throw new NotSupportedException($"The provided direction character '{direction}' is not supported.");
            }
        }

#if DEBUG
        PrintMoves(moves);
#endif

        return moves.Count;
    }

    public static int PartTwo(string[] lines)
    {
        var moves = new HashSet<Point>();
        var rope = new Point[10];

        // Add the tail
        moves.Add(rope.Last());

        foreach (var line in lines)
        {
            var lineSplit = line.Split(" ");
            var direction = lineSplit[0][0];
            var steps = int.Parse(lineSplit[1]);

            switch (direction)
            {
                case 'U':
                    Move(moves, steps, rope, (ref Point h) => h.Y--);
                    break;
                case 'R':
                    Move(moves, steps, rope, (ref Point h) => h.X++);
                    break;
                case 'D':
                    Move(moves, steps, rope, (ref Point h) => h.Y++);
                    break;
                case 'L':
                    Move(moves, steps, rope, (ref Point h) => h.X--);
                    break;
                default:
                    throw new NotSupportedException($"The provided direction character '{direction}' is not supported.");
            }
        }

#if DEBUG
        Console.WriteLine();
        Console.WriteLine("Rope tail movements:");
        PrintMoves(moves);
        Console.WriteLine();
#endif

        return moves.Count;
    }

    private static void Move(HashSet<Point> moves, int steps, Point[] rope, MoveAction moveHead)
    {
        for (var s = 0; s < steps; s++)
        {
            moveHead(ref rope[0]);

            // Update the none head knots
            for (var y = 1; y < rope.Length; y++)
            {
                if (!UpdateFollowingKnotPosition(rope[y - 1], ref rope[y]))
                {
                    // The knot didn't need an update
                    // No need to check the other knots of the rope as they didn't move
                    break;
                }
            }

            moves.Add(rope.Last());
        }
    }

    private static bool UpdateFollowingKnotPosition(Point previousKnot, ref Point knot)
    {
        // Previous knot moved two position out of range on both axes
        if (Math.Abs(previousKnot.X - knot.X) == 2
        && Math.Abs(previousKnot.Y - knot.Y) == 2)
        {
            if (previousKnot.X < knot.X)
            {
                knot.X--;
            }
            else
            {
                knot.X++;
            }

            if (previousKnot.Y < knot.Y)
            {
                knot.Y--;
            }
            else
            {
                knot.Y++;
            }

            return true;
        }

        // Previous knot is two column apart
        if (Math.Abs(previousKnot.X - knot.X) == 2)
        {
            knot.Y = previousKnot.Y;
            if (previousKnot.X < knot.X)
            {
                knot.X--;
            }
            else
            {
                knot.X++;
            }

            return true;
        }

        // Previous knot is two rows apart
        if (Math.Abs(previousKnot.Y - knot.Y) == 2)
        {
            knot.X = previousKnot.X;
            if (previousKnot.Y < knot.Y)
            {
                knot.Y--;
            }
            else
            {
                knot.Y++;
            }

            return true;
        }

        return false;
    }

    private static void PrintRope(Point[] rope)
    {
        var sb = new StringBuilder();

        var highest = new Point(0, 0);
        var lowest = new Point(0, 0);

        foreach (var knot in rope)
        {
            lowest.X = int.Min(lowest.X, knot.X);
            lowest.Y = int.Min(lowest.Y, knot.Y);
            highest.X = int.Max(highest.X, knot.X);
            highest.Y = int.Max(highest.Y, knot.Y);
        }

        for (var y = lowest.Y; y <= highest.Y; y++)
        {
            for (var x = lowest.X; x <= highest.X; x++)
            {
                var indexNumber = -1;
                var count = 0;
                foreach (var knot in rope)
                {
                    if (knot == new Point(x, y))
                    {
                        indexNumber = count;
                        break;
                    }

                    count++;
                }

                switch (indexNumber)
                {
                    case 0:
                        sb.Append("H");
                        break;
                    case > 0:
                        sb.Append(indexNumber);
                        break;
                    default:
                        sb.Append('.');
                        break;
                }
            }

            sb.AppendLine();
        }

        Console.WriteLine(sb.ToString());
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

            Console.WriteLine(string.Empty);
        }
    }
}
