using FluentAssertions;
using Xunit;

public class DayEleventTests
{
    private readonly string[] unitTestInput = """
    Monkey 0:
      Starting items: 79, 98
      Operation: new = old * 19
      Test: divisible by 23
        If true: throw to monkey 2
        If false: throw to monkey 3

    Monkey 1:
      Starting items: 54, 65, 75, 74
      Operation: new = old + 6
      Test: divisible by 19
        If true: throw to monkey 2
        If false: throw to monkey 0

    Monkey 2:
      Starting items: 79, 60, 97
      Operation: new = old * old
      Test: divisible by 13
        If true: throw to monkey 1
        If false: throw to monkey 3

    Monkey 3:
      Starting items: 74
      Operation: new = old + 3
      Test: divisible by 17
        If true: throw to monkey 0
        If false: throw to monkey 1
    """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnExampleTestCase_ReturnMonkeyBusinessAmount()
    {
        var result = DayElevent.PartOne(this.unitTestInput);

        result.Should().Be(10605);
    }

    [Fact]
    public void PartOne_OnUsingInputFile_ReturnMonkeyBusinessAmount()
    {
        var fileInput = ReadInput(11);

        var result = DayElevent.PartOne(fileInput);

        result.Should().Be(62491);
    }

    [Fact]
    public void PartTwo_OnExampleTestCase_ReturnMonkeyBusinessAmount()
    {
        var result = DayElevent.PartTwo(this.unitTestInput);

        result.Should().Be(2713310158);
    }

    [Fact]
    public void PartTwo_OnUsingInputFile_ReturnMonkeyBusinessAmount()
    {
        var fileInput = ReadInput(11);

        var result = DayElevent.PartTwo(fileInput);

        result.Should().Be(17408399184);
    }

    private static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber}/input.txt");
    }
}
