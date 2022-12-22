namespace Aoc.Year2022.Day05;

using FluentAssertions;
using Xunit;

public class DayFiveTests
{
    [Fact]
    public void PartOne_On_Return()
    {
        var input = """
            [D]    
        [N] [C]    
        [Z] [M] [P]
        1   2   3

        move 1 from 2 to 1
        move 3 from 1 to 3
        move 2 from 2 to 1
        move 1 from 1 to 2
        """.Split(Environment.NewLine);

        var result = DayFive.PartOne(input);

        result.Should().Be("CMZ");
    }

    [Fact]
    public void PartTwo_On_Return()
    {
        var input = """
            [D]    
        [N] [C]    
        [Z] [M] [P]
        1   2   3

        move 1 from 2 to 1
        move 3 from 1 to 3
        move 2 from 2 to 1
        move 1 from 1 to 2
        """.Split(Environment.NewLine);

        var result = DayFive.PartTwo(input);

        result.Should().Be("MCD");
    }
}