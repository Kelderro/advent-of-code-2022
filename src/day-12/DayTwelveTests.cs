namespace Aoc.Year2022.Day12;

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
    public void PartOne_OnUsingInputFile_ReturnFewestSteps()
    {
        var fileInput = ReadInput(12);

        var result = DayTwelve.PartOne(fileInput);

        result.Should().BeGreaterThan(319);
    }

    private static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber:00}/input.txt");
    }
}