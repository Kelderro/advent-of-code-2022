namespace Aoc.Year2022.Day04;

using FluentAssertions;
using Xunit;

public class DayFourTests
{
    [Fact]
    public void PartOne_OnExampleTestCase_ReturnNumberOfOverlappingOccurences()
    {
        var input = """
        2-4,6-8
        2-3,4-5
        5-7,7-9
        2-8,3-7
        6-6,4-6
        2-6,4-8
        """.Split(Environment.NewLine);

        var result = DayFour.PartOne(input);

        result.Should().Be(2);
    }

    [Fact]
    public void PartTwo_OnExampleTestCase_ReturnNumberOfOverlappingSections()
    {
        var input = """
        2-4,6-8
        2-3,4-5
        5-7,7-9
        2-8,3-7
        6-6,4-6
        2-6,4-8
        """.Split(Environment.NewLine);

        var result = DayFour.PartTwo(input);

        result.Should().Be(4);
    }
}