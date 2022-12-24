namespace Aoc.Year2022.Day09;

using FluentAssertions;
using Xunit;

public class DayNineTests
{
    [Fact]
    public void PartOne_OnExampleTestCase_ReturnPositionsVisitByTail()
    {
        var unitTestInput = """
        R 4
        U 4
        L 3
        D 1
        R 4
        D 1
        L 5
        R 2
        """.Split(Environment.NewLine);

        var result = DayNine.PartOne(unitTestInput);

        result.Should().Be(13);
    }

    [Fact]
    public void PartOne_OnRunningSolution_ReturnPositionsVisitByTail()
    {
        var input = this.ReadInput(9);

        var result = DayNine.PartOne(input);

        result.Should().Be(6384);
    }

    [Fact]
    public void PartTwo_OnRopeWith10Knots2_ReturnPositionsVisitByTail()
    {
        var unitTestInput = """
        R 4
        U 4
        L 3
        D 1
        R 4
        D 1
        L 5
        R 2
        """.Split(Environment.NewLine);

        var result = DayNine.PartTwo(unitTestInput);

        result.Should().Be(1);
    }

    [Fact]
    public void PartTwo_OnRopeWith10Knots_ReturnPositionsVisitByTail()
    {
        var unitTestInput = """
        R 5
        U 8
        L 8
        D 3
        R 17
        D 10
        L 25
        U 20
        """.Split(Environment.NewLine);

        var result = DayNine.PartTwo(unitTestInput);

        result.Should().Be(36);
    }

    [Fact]
    public void PartTwo_OnRunningSolution_ReturnPositionsVisitByTail()
    {
        var input = this.ReadInput(9);

        var result = DayNine.PartTwo(input);

        result.Should().Be(2734);
    }

    private string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber:00}/input.txt");
    }
}