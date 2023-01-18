namespace Aoc.Year2022.Day13;

using FluentAssertions;
using Xunit;

public class DayThirteenTests
{
    private readonly string[] unitTestInput = """
    [1,1,3,1,1]
    [1,1,5,1,1]

    [[1],[2,3,4]]
    [[1],4]

    [9]
    [[8,7,6]]

    [[4,4],4,4]
    [[4,4],4,4,4]

    [7,7,7,7]
    [7,7,7]

    []
    [3]

    [[[]]]
    [[]]

    [1,[2,[3,[4,[5,6,7]]]],8,9]
    [1,[2,[3,[4,[5,6,0]]]],8,9]
    """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnExampleTestCase_ReturnFewestStepsRequired()
    {
        var result = DayThirteen.PartOne(this.unitTestInput);

        result.Should().Be(13);
    }

    [Fact]
    public void PartOne_OnAocInputFile_ReturnSumOfIndices()
    {
        var fileInput = ReadInput(13);

        var result = DayThirteen.PartOne(fileInput);

        result.Should().Be(5852);
    }

    [Fact]
    public void PartTwo_OnExampleTestCase_ReturnFewestStepsRequired()
    {
        var result = DayThirteen.PartTwo(this.unitTestInput);

        result.Should().Be(140);
    }

    [Fact]
    public void PartTwo_OnAocInputFile_ReturnAmountOfVisibleTrees()
    {
        var fileInput = ReadInput(13);

        var result = DayThirteen.PartTwo(fileInput);

        result.Should().Be(24190);
    }

    private static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber:00}/input.txt");
    }
}