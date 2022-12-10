using Xunit;
using FluentAssertions;

public class DayOneTests
{
    [Theory]
    [InlineData(20, "1", "2", "3", "", "20")]
    [InlineData(6, "1", "2", "3", "", "4")]
    public void PartOne_OnCapacityOfMultiElves_ReturnTotalCaloriesOfElfWithMostCapacity(int expected, params string[] lines)
    {
        var result = DayOne.PartOne(lines);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(36, "1", "2", "3", "", "5", "", "10", "", "20", "", "4")]
    [InlineData(36, "1", "2", "3", "", "5", "", "5", "5", "", "20", "", "4")]
    public void PartTwo_OnCapacityOfMultiElves_ReturnTotalCaloriesOfTopThreeElves(int expected, params string[] lines)
    {
        var result = DayOne.PartTwo(lines);
        result.Should().Be(expected);
    }
}