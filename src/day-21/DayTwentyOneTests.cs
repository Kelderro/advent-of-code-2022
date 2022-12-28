using FluentAssertions;
using Xunit;

namespace Aoc.Year2022.Day21;

public class DayTwentyOneTests
{
    private readonly string[] unitTestInput = """
    root: pppw + sjmn
    dbpl: 5
    cczh: sllz + lgvd
    zczc: 2
    ptdq: humn - dvpt
    dvpt: 3
    lfqf: 4
    humn: 5
    ljgn: 2
    sjmn: drzm * dbpl
    sllz: 4
    pppw: cczh / lfqf
    lgvd: ljgn * ptdq
    drzm: hmdt - zczc
    hmdt: 32
    """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnExampleTestCase_ReturnNumberYelledByRootMonkey()
    {
        var result = DayTwentyOne.PartOne(this.unitTestInput);

        result.Should().Be(152);
    }

    [Fact]
    public void PartOne_OnUsingInputFile_ReturnNumberYelledByRootMonkey()
    {
        var fileInput = ReadInput(21);

        var result = DayTwentyOne.PartOne(fileInput);

        result.Should().Be(56490240862410);
    }

    [Fact]
    public void PartTwo_OnExampleTestCase_ReturnNumberToYell()
    {
        var result = DayTwentyOne.PartTwo(this.unitTestInput);

        result.Should().Be(301);
    }

    [Fact]
    public void PartTwo_OnAdditionNumberHumnRight_ReturnHumnNumber()
    {
        var input = """
        root: aaaa + humn
        aaaa: 3
        humn: 5
        """.Split(Environment.NewLine);

        var result = DayTwentyOne.PartTwo(input);

        result.Should().Be(3);
    }

    [Theory]
    [InlineData("""
    root: AAAA + BBBB
    BBBB: 10
    AAAA: humn + CCCC
    CCCC: 3
    humn: 0
    """, 7)]
    [InlineData("""
    root: AAAA + BBBB
    BBBB: 10
    AAAA: CCCC + humn
    CCCC: 3
    humn: 0
    """, 7)]
    public void PartTwo_OnAdditionNumber_ReturnHumnNumber(
        string input,
        int expectedHumn)
    {
        var inputLines = input.Split(Environment.NewLine);

        var result = DayTwentyOne.PartTwo(inputLines);

        result.Should().Be(7);
    }

    [Theory]
    [InlineData("""
    root: AAAA + BBBB
    BBBB: 10
    AAAA: humn - CCCC
    CCCC: 2
    humn: 0
    """, 12)]
    [InlineData("""
    root: AAAA + BBBB
    BBBB: 10
    AAAA: CCCC - humn
    CCCC: 2
    humn: 0
    """, -8)]
    public void PartTwo_OnSubtractionNumber_ReturnHumnNumber(
        string input,
        int expectedHumn)
    {
        var inputLines = input.Split(Environment.NewLine);

        var result = DayTwentyOne.PartTwo(inputLines);

        result.Should().Be(expectedHumn);
    }

    [Theory]
    [InlineData("""
    root: AAAA + BBBB
    BBBB: 10
    AAAA: humn / CCCC
    CCCC: 2
    humn: 0
    """, 20L)]
    [InlineData("""
    root: AAAA + BBBB
    BBBB: 10
    AAAA: CCCC / humn
    CCCC: 20
    humn: 0
    """, 2L)]
    public void PartTwo_OnDivisionNumber_ReturnHumnNumber(
    string input,
    long expectedHumn)
    {
        var inputLines = input.Split(Environment.NewLine);

        var result = DayTwentyOne.PartTwo(inputLines);

        result.Should().Be(expectedHumn);
    }

    [Theory]
    [InlineData("""
    root: AAAA + BBBB
    BBBB: 10
    AAAA: humn * CCCC
    CCCC: 2
    humn: 0
    """, 5)]
    [InlineData("""
    root: AAAA + BBBB
    BBBB: 10
    AAAA: CCCC * humn
    CCCC: 2
    humn: 0
    """, 5)]
    public void PartTwo_OnMultiplicationNumber_ReturnHumnNumber(
        string input,
        int expectedHumn)
    {
        var inputLines = input.Split(Environment.NewLine);

        var result = DayTwentyOne.PartTwo(inputLines);

        result.Should().Be(expectedHumn);
    }

    [Fact]
    public void PartTwo_OnUsingInputFile_ReturnNumberToYell()
    {
        var fileInput = ReadInput(21);

        var result = DayTwentyOne.PartTwo(fileInput);

        result.Should().Be(3403989691757);
    }

    private static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber}/input.txt");
    }
}