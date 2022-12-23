namespace Aoc.Year2022.Day09;

using FluentAssertions;
using Xunit;

public class DayNineTests
{
    private readonly string[] unitTestInput = """
    R 4
    U 4
    L 3
    D 1
    R 4
    D 1
    L 5
    R 2
    """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnExampleTestCase_ReturnPositionsVisitByTail()
    {
        var result = DayNine.PartOne(this.unitTestInput);

        result.Should().Be(13);
    }

    [Fact]
    public void PartOne_OnRunningSolution_ReturnPositionsVisitByTail()
    {
        var input = this.ReadInput(9);

        var result = DayNine.PartOne(input);

        result.Should().Be(6384);
    }

    private string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber:00}/input.txt");
    }
}