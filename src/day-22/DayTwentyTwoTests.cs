namespace Aoc.Year2022.Day22;

using FluentAssertions;
using Xunit;

public class DayTwentyTwoTests
{
    private readonly string[] aocExampleInput = """
            ...#
            .#..
            #...
            ....
    ...#.......#
    ........#...
    ..#....#....
    ..........#.
            ...#....
            .....#..
            .#......
            ......#.

    10R5L5R10L4R5L5
    """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnAocExampleTestCase_ReturnPassword()
    {
        var result = DayTwentyTwo.PartOne(this.aocExampleInput);

        result.Should().Be(6032);
    }

    [Fact]
    public void PartOne_OnAocInputFile_ReturnPassword()
    {
        var fileInput = ReadInput(22);

        var result = DayTwentyTwo.PartOne(fileInput);

        result.Should().Be(1484);
    }

    private static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber}/input.txt");
    }
}