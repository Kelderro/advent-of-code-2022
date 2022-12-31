namespace Aoc.Year2022.Day20;

using FluentAssertions;
using Xunit;

public class DayTwentyTests
{
    private readonly string[] aocExampleInput = """
    1
    2
    -3
    3
    -2
    0
    4
    """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnAocExampleTestCase_ReturnGroveCoordinates()
    {
        Console.WriteLine(-1 % 3);
        Console.WriteLine(-2 % 3);
        Console.WriteLine(-3 % 3);
        Console.WriteLine(-4 % 3);
        Console.WriteLine(-5 % 3);
        Console.WriteLine(-6 % 3);

        var result = DayTwenty.PartOne(this.aocExampleInput);

        result.Should().Be(3);
    }

    [Fact]
    public void PartOne_OnAocInputFile_ReturnGroveCoordinates()
    {
        var fileInput = ReadInput(20);

        var result = DayTwenty.PartOne(fileInput);

        result.Should().BeGreaterThan(3012);
    }

    private static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber}/input.txt");
    }
}