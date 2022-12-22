/// <summary>
/// https://adventofcode.com/2022/day/6.
/// </summary>

namespace Aoc.Year2022.Day06;

public sealed class DaySix : IDay<int>
{
    public static int PartOne(string[] lines)
    {
        var markerLength = 4;
        return FindMarkerEnd(lines[0], markerLength);
    }

    public static int PartTwo(string[] lines)
    {
        var markerLength = 14;
        return FindMarkerEnd(lines[0], markerLength);
    }

    private static int FindMarkerEnd(string input, int markerLength)
    {
        for (var i = 0; i < input.Length - (markerLength - 1); i++)
        {
            var str = input[i..(i + markerLength)];

            // Script can be optimized by moving the starting position to
            // the first occurance of the duplicate character.
            if (str.Distinct().Count() == markerLength)
            {
                // Found the marker!
                return i + markerLength;
            }
        }

        throw new NotSupportedException("The input string does not contain a marker");
    }
}
