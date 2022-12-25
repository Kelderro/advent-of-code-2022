namespace Aoc.Year2022.Day14;

using FluentAssertions;
using Xunit;

public class DayFourteenTests
{
    private readonly string[] unitTestInput = """
    498,4 -> 498,6 -> 496,6
    503,4 -> 502,4 -> 502,9 -> 494,9
    """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnExampleTestCase_ReturnNumberOfSand()
    {
        var result = DayFourteen.PartOne(this.unitTestInput);

        result.Should().Be(1);
    }

    [Fact]
    public void PartOne_OnUsingInputFile_ReturnNumberOfSand()
    {
        var fileInput = ReadInput(14);

        var result = DayFourteen.PartOne(fileInput);

        result.Should().Be(1);
    }

    [Fact]
    public void PartTwo_OnExampleTestCase_ReturnNumberOfSand()
    {
        var result = DayFourteen.PartTwo(this.unitTestInput);

        result.Should().Be(1);
    }

    [Fact]
    public void PartTwo_OnUsingInputFile_ReturnNumberOfSand()
    {
        var fileInput = ReadInput(14);

        var result = DayFourteen.PartTwo(fileInput);

        result.Should().Be(1);
    }

    private static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber:00}/input.txt");
    }
}
