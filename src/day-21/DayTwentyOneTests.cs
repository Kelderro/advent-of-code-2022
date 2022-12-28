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
    public void PartOne_OnExampleTestCase_ReturnFewestStepsRequired()
    {
        var result = DayTwentyOne.PartOne(this.unitTestInput);

        result.Should().Be(152);
    }

    [Fact]
    public void PartOne_OnUsingInputFile_ReturnMonkeyBusinessAmount()
    {
        var fileInput = ReadInput(21);

        var result = DayTwentyOne.PartOne(fileInput);

        result.Should().Be(56490240862410);
    }

    private static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines($"./src/day-{dayNumber}/input.txt");
    }
}