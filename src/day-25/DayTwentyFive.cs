/// <summary>
/// Day 25: Full of Hot Air
/// https://adventofcode.com/2022/day/25
/// </summary>
namespace Aoc.Year2022.Day25;

using System.Text;

public sealed class DayTwentyFive : IDay<string>
{
    public static string PartOne(string[] lines)
    {
        var fuelAmounts = 0L;

        // Convert to decimal numbers
        foreach (var line in lines)
        {
            fuelAmounts += Parse(line);
        }

        return Parse(fuelAmounts);
    }

    public static string PartTwo(string[] lines)
    {
        throw new NotImplementedException();
    }

    public static long Parse(string input)
    {
        var snafu = 0L;
        var reversedInput = input.Reverse().ToArray();

        for (var i = 0; i < reversedInput.Length; i++)
        {
            var chr = reversedInput[i];
            var number = chr switch
            {
                '-' => -1,
                '=' => -2,
                _ => long.Parse(chr.ToString()),
            };

            if (i > 0)
            {
                snafu += number * (long)Math.Pow(5, i);
                continue;
            }

            snafu += number;
        }

        return snafu;
    }

    public static string Parse(long input)
    {
        var numberArray = new List<char>();

        for (var i = 0; input > 0; i++)
        {
            var number = input % 5;
            numberArray.Add(number switch
            {
                3 => '=',
                4 => '-',
                _ => number.ToString()[0],
            });

            input = input / 5;

            if (number >= 3)
            {
                input++;
            }
        }

        numberArray.Reverse();
        return string.Join(string.Empty, numberArray);
    }
}