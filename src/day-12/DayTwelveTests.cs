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
}