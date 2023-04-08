/// <summary>
/// Day 22: Monkey Math
/// https://adventofcode.com/2022/day/22
/// </summary>
namespace Aoc.Year2022.Day22;

public sealed class DayTwentyTwo : IDay<int>
{
    // Open tile: .
    // Solid wall: #
    private static Facing[]? facings;

    public static int PartOne(string[] lines)
    {
        var (map, path) = Parse(lines);
        var (endPosition, facing) = Walk(map, path);
        var password = GeneratePassword(endPosition, map.LineLength, facing);
        return password;
    }

    public static int PartTwo(string[] lines)
    {
        throw new NotImplementedException();
    }

    private static (int EndPosition, Facing Facing) Walk((char[] Map, int ColumnCount) map, IList<(int Steps, char Turn)> path)
    {
        if (facings == null)
        {
            facings = CreateFacings();
        }
#if DEBUG
        Console.CursorVisible = false;
#endif

        // Initially, you are facing to the right (from the perspective of how the map is drawn).
        var facing = facings.Single(x => x.Name.Equals("Right", StringComparison.InvariantCultureIgnoreCase));

        // You begin the path in the leftmost open tile of the top row of tiles.
        var position = Array.IndexOf(map.Map, '.') - 1;

        var rowCount = map.Map.Length / map.ColumnCount;
#if DEBUG
        Console.Clear();
#endif
        for (var i = 0; i < map.Map.Length; i++)
        {
            if (i % map.ColumnCount == 0 && i != 0)
            {
                Console.WriteLine($" - {i - 1,3}");
            }

            if (map.Map[i] == '#')
            {
#if DEBUG
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(map.Map[i]);
                Console.ResetColor();
#endif
                continue;
            }

            Console.Write(map.Map[i]);
        }

        Console.WriteLine($" - {map.Map.Length - 1,3}");

        foreach (var p in path)
        {
            if (p.Turn != default(char))
            {
                facing = Turn(p.Turn.Equals('R'), facing);
            }
#if DEBUG
            Console.SetCursorPosition(0, rowCount);
            Console.Write($"\nDirection: {facing.Name}");
#endif

            for (var i = 1; i <= p.Steps; i++)
            {
                var changeTo = facing.NextPosition(position, map.ColumnCount, rowCount);
#if DEBUG
                Console.SetCursorPosition(0, rowCount + 2);
                Console.Write($"Changed from {position} to {changeTo}");
#endif

                // Check if we hit an empty space
                while (map.Map[changeTo] == ' ')
                {
                    changeTo = facing.NextPosition(changeTo, map.ColumnCount, rowCount);
                }

                // Check if we hit a solid wall
                if (map.Map[changeTo] == '#')
                {
                    break;
                }
#if DEBUG
                Console.SetCursorPosition(0, rowCount + 3);
                Console.WriteLine($"Showing situation after taking {i} steps");
#endif
                map.Map[changeTo] = facing.Symbol;
                UpdateMap(map.Map, map.ColumnCount, changeTo);

                position = changeTo;
                Thread.Sleep(2);
            }
        }
#if DEBUG
        Console.SetCursorPosition(0, rowCount + 4);
        Console.CursorVisible = true;
#endif
        return (position, facing);
    }

    private static void UpdateMap(char[] map, int columnCount, int changeTo)
    {
#if DEBUG
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.SetCursorPosition(changeTo % columnCount, changeTo / columnCount);
        Console.Write(map[changeTo]);
        Console.ResetColor();
#endif
    }

    private static int GeneratePassword(int endPosition, int rowLength, Facing facing)
    {
        var multiplyRowNumberBy = 1000;
        var multiplyColumnNumberBy = 4;

        var columnNumber = (endPosition % rowLength) + 1;
        var rowNumber = (endPosition / rowLength) + 1;

        return (multiplyRowNumberBy * rowNumber)
             + (multiplyColumnNumberBy * columnNumber)
             + facing.Score;
    }

    private static Facing[] CreateFacings()
    {
        return new Facing[]
        {
            new Facing
            {
                Name = "Right",
                Score = 0,
                Symbol = '>',
                NextPosition = (position, columnCount, rowCount) =>
                    (position + 1) % columnCount == 0
                        ? (position + 1) - columnCount
                        : position + 1,
            },
            new Facing
            {
                Name = "Down",
                Score = 1,
                Symbol = 'V',
                NextPosition = (position, columnCount, rowCount) =>
                    position / columnCount == rowCount - 1
                        ? position % columnCount
                        : position + columnCount,
            },
            new Facing
            {
                Name = "Left",
                Score = 2,
                Symbol = '<',
                NextPosition = (position, columnCount, rowCount) =>
                    position % columnCount == 0
                        ? position + (columnCount - 1)
                        : position - 1,
            },
            new Facing
        {
                Name = "Up",
                Score = 3,
                Symbol = '^',
                NextPosition = (position, columnCount, rowCount) =>
                    (position / columnCount == 0)
                        ? (position % columnCount) + ((rowCount - 1) * columnCount)
                        : position - columnCount,
        },
        };
    }

    private static Facing Turn(bool clockWise, Facing currentFacing)
    {
        if (facings == null)
        {
            facings = CreateFacings();
        }

        var indexOf = Array.IndexOf(facings, currentFacing);

        if (clockWise)
        {
            indexOf++;
            return facings[indexOf % facings.Length];
        }

        indexOf--;
        if (indexOf < 0)
        {
            indexOf = facings.Length - 1;
        }

        return facings[indexOf];
    }

    private static ((char[] Map, int LineLength) Map, IList<(int Steps, char Turn)> Path) Parse(string[] lines)
    {
        // First part is the map
        // Second part is the path
        // Turn clockwise (R)
        // Turn counterclockwise (L)

        // You begin the path in the leftmost open tile of the top row of tiles.
        // Initially, you are facing to the right (from the perspective of how the map is drawn).
        // Find the empty row
        var mapLines = lines[..(lines.Length - 2)];
        var pathLine = lines[lines.Length - 1];

        var map = ParseMap(mapLines);
        var path = ParsePath(pathLine);

        return (map, path);
    }

    private static (char[] Map, int LineLength) ParseMap(string[] mapLines)
    {
        var longestMapRow = mapLines.Max(x => x.Length);

        var map = new char[longestMapRow * mapLines.Length];

        for (var i = 0; i < mapLines.Length; i++)
        {
            var mapLine = mapLines[i];
            mapLine = mapLine.PadRight(longestMapRow, ' ');
            Array.Copy(mapLine.ToArray(), 0, map, i * longestMapRow, longestMapRow);
        }

        return (map, longestMapRow);
    }

    private static IList<(int Steps, char Turn)> ParsePath(string pathLine)
    {
        var path = new List<(int Steps, char Turn)>();
        var steps = string.Empty;
        char turn = default(char);

        foreach (var chr in pathLine)
        {
            if (char.IsDigit(chr))
            {
                steps += chr;
                continue;
            }

            path.Add((int.Parse(steps), turn));

            turn = chr;
            steps = string.Empty;
        }

        path.Add((int.Parse(steps), turn));

        return path;
    }

    public class Facing
    {
        required public string Name { get; init; }

        required public int Score { get; init; }

        required public char Symbol { get; init; }

        required public Func<int, int, int, int> NextPosition { get; init; }
    }
}