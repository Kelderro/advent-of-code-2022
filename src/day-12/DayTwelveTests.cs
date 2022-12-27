namespace Aoc.Year2022.Day12;

using System.Text;
using FluentAssertions;
using Xunit;

public class DayTwelveTests
{
    private readonly string[] unitTestInput = """
    Sabqponm
    abcryxxl
    accszExk
    acctuvwj
    abdefghi
    """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnExampleTestCase_ReturnFewestStepsRequired()
    {
        var result = DayTwelve.PartOne(this.unitTestInput);

        result.Should().Be(31);
    }

    [Fact]
    public void PartOne_OnTopStart_ReturnAnswer()
    {
        var input = this.GenerateTopToBottomArray();

        var steps = DayTwelve.PartOne(input);

        Assert.Equal(25, steps);
    }

    [Fact]
    public void PartOne_OnLeftStart_ReturnAnswer()
    {
        var input = this.GenerateLeftToRightArray();

        var steps = DayTwelve.PartOne(input);

        Assert.Equal(25, steps);
    }

    [Fact]
    public void PartOne_OnBottomStart_ReturnAnswer()
    {
        var input = this.GenerateBottomToTopArray();

        var steps = DayTwelve.PartOne(input);

        Assert.Equal(25, steps);
    }

    [Fact]
    public void PartOne_OnRightStart_ReturnAnswer()
    {
        var input = this.GenerateRightToLeftArray();

        var steps = DayTwelve.PartOne(input);

        Assert.Equal(25, steps);
    }

    [Fact]
    public void PartOne_OnUsingInputFile_ReturnFewestSteps()
    {
        var fileInput = ReadInput(12);

        var result = DayTwelve.PartOne(fileInput);

        result.Should().Be(440);
    }

    [Fact]
    public void PartTwo_OnExampleTestCase_ReturnFewestStepsRequired()
    {
        var result = DayTwelve.PartTwo(this.unitTestInput);

        result.Should().Be(29);
    }

    [Fact]
    public void PartTwo_OnUsingInputFile_ReturnFewestSteps()
    {
        var fileInput = ReadInput(12);

        var result = DayTwelve.PartTwo(fileInput);

        result.Should().Be(439);
    }

    private static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber:00}/input.txt");
    }

    private string[] GenerateTopToBottomArray()
    {
        var list = new List<string>();
        for (var i = 0; i < 26; i++)
        {
            var chr = (char)(i + 97);
            list.Add(string.Concat(chr, chr, chr));
        }

        string[] returnArray = list.ToArray();

        returnArray[0] = "aSa";
        returnArray[25] = "zEz";

        return returnArray;
    }

    private string[] GenerateLeftToRightArray()
    {
        var list = new List<string>();

        for (var row = 0; row < 5; row++)
        {
            var line = new StringBuilder();
            for (var column = 0; column < 26; column++)
            {
                var chr = (char)(column + 97);
                if (column == 0 && row == 2)
                {
                    chr = 'S';
                }

                if (column == 25 && row == 2)
                {
                    chr = 'E';
                }

                line.Append(chr);
            }

            list.Add(line.ToString());
        }

        string[] returnArray = list.ToArray();

        return returnArray;
    }

    private string[] GenerateBottomToTopArray()
    {
        return this.GenerateTopToBottomArray().Reverse()
                                         .ToArray();
    }

    private string[] GenerateRightToLeftArray()
    {
        return this.GenerateLeftToRightArray().Reverse()
                                         .ToArray();
    }
}