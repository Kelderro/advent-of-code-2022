namespace Aoc.Year2022.Day02;

using FluentAssertions;
using Xunit;

public class DayTwoTests
{
    [Theory]
    [InlineData(1, "B X")] // Lose score
    [InlineData(2, "C Y")] // Lose score
    [InlineData(3, "A Z")] // Lose score
    [InlineData(4, "A X")] // Draw score
    [InlineData(5, "B Y")] // Draw score
    [InlineData(6, "C Z")] // Draw score
    [InlineData(7, "C X")] // Win score
    [InlineData(8, "A Y")] // Win score
    [InlineData(9, "B Z")] // Win score
    public void PartOne_OnWin_ReturnWithSymbolScore(int expected, params string[] lines)
    {
        var result = DayTwo.PartOne(lines);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(3, "A X")] // Losing score
    [InlineData(1, "B X")] // Losing score
    [InlineData(2, "C X")] // Losing score
    [InlineData(4, "A Y")] // Draw score
    [InlineData(5, "B Y")] // Draw score
    [InlineData(6, "C Y")] // Draw score
    [InlineData(8, "A Z")] // Win score
    [InlineData(9, "B Z")] // Win score
    [InlineData(7, "C Z")] // Win score
    public void PartTwo_OnSecondColumnValue_ReturnWithSymbolScore(int expected, params string[] lines)
    {
        var result = DayTwo.PartTwo(lines);
        result.Should().Be(expected);
    }
}