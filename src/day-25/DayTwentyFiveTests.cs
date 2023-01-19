namespace Aoc.Year2022.Day25;

using FluentAssertions;
using Xunit;

public class DayTwentyFiveTests
{
    private readonly string[] exampleTestInput = """
    1=-0-2
    12111
    2=0=
    21
    2=01
    111
    20012
    112
    1=-1=
    1-12
    12
    1=
    122
    """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnExampleTestCase_ReturnSnafu()
    {
        var result = DayTwentyFive.PartOne(this.exampleTestInput);

        result.Should().Be("2=-1=0");
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    [InlineData(3, "1=")]
    [InlineData(4, "1-")]
    [InlineData(5, "10")]
    [InlineData(6, "11")]
    [InlineData(7, "12")]
    [InlineData(8, "2=")]
    [InlineData(9, "2-")]
    [InlineData(10, "20")]
    [InlineData(15, "1=0")]
    [InlineData(20, "1-0")]
    [InlineData(2022, "1=11-2")]
    [InlineData(12345, "1-0---0")]
    [InlineData(314159265, "1121-1110-1=0")]
    public void ParseNumberToSnafu_(int number, string snafu)
    {
        var result = DayTwentyFive.Parse(number);

        result.Should().Be(snafu);
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    [InlineData(3, "1=")]
    [InlineData(4, "1-")]
    [InlineData(5, "10")]
    [InlineData(6, "11")]
    [InlineData(7, "12")]
    [InlineData(8, "2=")]
    [InlineData(9, "2-")]
    [InlineData(10, "20")]
    [InlineData(15, "1=0")]
    [InlineData(20, "1-0")]
    [InlineData(2022, "1=11-2")]
    [InlineData(12345, "1-0---0")]
    [InlineData(314159265, "1121-1110-1=0")]
    public void ParseSnafuToNumber(int number, string snafu)
    {
        var result = DayTwentyFive.Parse(snafu);

        result.Should().Be(number);
    }

    [Fact]
    public void PartOne_OnAocInputFile_ReturnSumOfIndices()
    {
        var aocInput = ReadInput(25);

        var result = DayTwentyFive.PartOne(aocInput);

        result.Should().Be("2-0-01==0-1=2212=100");
    }

    private static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber:00}/input.txt");
    }
}