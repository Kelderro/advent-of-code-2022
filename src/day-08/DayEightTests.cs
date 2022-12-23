namespace Aoc.Year2022.Day08;

using FluentAssertions;
using Xunit;

public class DayEightTests
{
    private readonly string[] exampleTestInput = """
        30373
        25512
        65332
        33549
        35390
        """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnExampleTestCase_ReturnAmountOfVisibleTrees()
    {
        var result = DayEight.PartOne(this.exampleTestInput);

        result.Should().Be(21);
    }

    [Fact]
    public void PartOne_OnRunningSolution_ReturnAmountOfVisibleTrees()
    {
        var input = this.ReadInput(8);

        var result = DayEight.PartOne(input);

        result.Should().Be(1827);
    }

    [Fact]
    public void PartTwo_OnExampleTestCase_ReturnHighestScenicScore()
    {
        var result = DayEight.PartTwo(this.exampleTestInput);

        result.Should().Be(8);
    }

    [Fact]
    public void PartTwo_OnRunningSolution_ReturnHighestScenicScore()
    {
        var result = DayEight.PartTwo(this.ReadInput(8));

        result.Should().Be(335580);
    }

    private string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber:00}/input.txt");
    }
}