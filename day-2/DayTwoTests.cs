using Xunit;
using FluentAssertions;

public class DayTwoTests
{
    [Theory]
    [InlineData(1, "B X")]
    [InlineData(2, "C Y")]
    [InlineData(3, "A Z")]
    public void PartOne_OnLose_ReturnLoseScoreWithSymbolScore(int expected, params string[] lines)
    {
        var result = DayTwo.PartOne(lines);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(4, "A X")]
    [InlineData(5, "B Y")]
    [InlineData(6, "C Z")]
    public void PartOne_OnDraw_ReturnDrawScoreWithSymbolScore(int expected, params string[] lines)
    {
        var result = DayTwo.PartOne(lines);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(7, "C X")]
    [InlineData(8, "A Y")]
    [InlineData(9, "B Z")]
    public void PartOne_OnWin_ReturnWinScoreWithSymbolScore(int expected, params string[] lines)
    {
        var result = DayTwo.PartOne(lines);
        result.Should().Be(expected);
    }
}