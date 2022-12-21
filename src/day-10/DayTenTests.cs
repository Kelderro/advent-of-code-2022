namespace Aoc.Year2022.Day10;

using FluentAssertions;
using Xunit;

public class DayTenTests
{
    private readonly string[] input = """
    addx 15
    addx -11
    addx 6
    addx -3
    addx 5
    addx -1
    addx -8
    addx 13
    addx 4
    noop
    addx -1
    addx 5
    addx -1
    addx 5
    addx -1
    addx 5
    addx -1
    addx 5
    addx -1
    addx -35
    addx 1
    addx 24
    addx -19
    addx 1
    addx 16
    addx -11
    noop
    noop
    addx 21
    addx -15
    noop
    noop
    addx -3
    addx 9
    addx 1
    addx -3
    addx 8
    addx 1
    addx 5
    noop
    noop
    noop
    noop
    noop
    addx -36
    noop
    addx 1
    addx 7
    noop
    noop
    noop
    addx 2
    addx 6
    noop
    noop
    noop
    noop
    noop
    addx 1
    noop
    noop
    addx 7
    addx 1
    noop
    addx -13
    addx 13
    addx 7
    noop
    addx 1
    addx -33
    noop
    noop
    noop
    addx 2
    noop
    noop
    noop
    addx 8
    noop
    addx -1
    addx 2
    addx 1
    noop
    addx 17
    addx -9
    addx 1
    addx 1
    addx -3
    addx 11
    noop
    noop
    addx 1
    noop
    addx 1
    noop
    noop
    addx -13
    addx -19
    addx 1
    addx 3
    addx 26
    addx -30
    addx 12
    addx -1
    addx 3
    addx 1
    noop
    noop
    noop
    addx -9
    addx 18
    addx 1
    addx 2
    noop
    noop
    addx 9
    noop
    noop
    noop
    addx -1
    addx 2
    addx -37
    addx 1
    addx 3
    noop
    addx 15
    addx -21
    addx 22
    addx -6
    addx 1
    noop
    addx 2
    addx 1
    noop
    addx -10
    noop
    noop
    addx 20
    addx 1
    addx 2
    addx 2
    addx -6
    addx -11
    noop
    noop
    noop
    """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnExampleTestCase_ReturnExpectedSignalStrength()
    {
        var result = int.Parse(DayTen.PartOne(this.input));

        result.Should().Be(13140);
    }

    [Fact]
    public void PartOne_OnUsingInputFile_ReturnExpectedSignalStrengthSolution()
    {
        var input = ReadInput(10);

        var result = int.Parse(DayTen.PartOne(input));

        result.Should().Be(17020);
    }

    [Fact]
    public void PartTwo_OnSimpleExampleTestCase_ReturnCrtScreen()
    {
        var input = """
        addx 15
        addx -11
        addx 6
        addx -3
        addx 5
        addx -1
        addx -8
        addx 13
        addx 4
        noop
        addx -1
        """.Split(Environment.NewLine);

        var result = DayTen.PartTwo(input);

        result.Should().Be("""

        ##..##..##..##..##..#...................
        ........................................
        ........................................
        ........................................
        ........................................
        ........................................

        """);
    }

    [Fact]
    public void PartTwo_OnExampleTestCase_ReturnCrtScreen()
    {
        var result = DayTen.PartTwo(this.input);

        result.Should().Be("""

        ##..##..##..##..##..##..##..##..##..##..
        ###...###...###...###...###...###...###.
        ####....####....####....####....####....
        #####.....#####.....#####.....#####.....
        ######......######......######......####
        #######.......#######.......#######.....

        """);
    }

    [Fact]
    public void PartTwo_OnUsingInputFile_ReturnCrtScreen()
    {
        var input = ReadInput(10);

        var result = DayTen.PartTwo(input);

        result.Should().Be("""
        
        ###..#....####.####.####.#.....##..#####
        #..#.#....#.......#.#....#....#..#.#...#
        #..#.#....###....#..###..#....#....###..
        ###..#....#.....#...#....#....#.##.#..##
        #.#..#....#....#....#....#....#..#.#..##
        #..#.####.####.####.#....####..###.####.

        """);
    }

    private static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber}/input.txt");
    }
}