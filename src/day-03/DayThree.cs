/// <summary>
/// https://adventofcode.com/2022/day/3
/// </summary>
public sealed class DayThree : IDay<int>
{
    public static int PartOne(string[] lines)
    {
        var totalScore = 0;
        foreach (var line in lines)
        {
            var (left, right) = SplitInHalf(line);
            var inBoth = left.Intersect(right).ToList();
            totalScore += Score(inBoth);
        }
        return totalScore;
    }

    public static int PartTwo(string[] lines)
    {
        var totalScore = 0;
        for (int i = 0; i < lines.Length; i += 3)
        {
            var inBoth = lines[i].Intersect(lines[i + 1])
                                 .Intersect(lines[i + 2])
                                 .ToList();

            totalScore += Score(inBoth);
        }

        return totalScore;
    }

    private static (string left, string right) SplitInHalf(string input)
    {
        var splitAt = input.Length / 2;
        return (input[..splitAt], input[splitAt..]);
    }

    private static int Score(IEnumerable<char> inBoth)
    {
        var total = 0;
        foreach (var chr in inBoth)
        {
            // Handle: a-z
            if (chr >= 'a')
            {
                total += chr - 96;
                continue;
            }
            // Handle: A-Z
            total += chr - 38;
        }
        return total;
    }
}