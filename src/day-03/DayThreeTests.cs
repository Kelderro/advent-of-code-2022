namespace Aoc.Year2022.Day03;

using FluentAssertions;
using Xunit;

public class DayThreeTests
{
    [Fact]
    public void PartOne_OnRuckSacksWithDuplicateItems_ReturnTheIntersectCharScore()
    {
        var input = """
        vJrwpWtwJgWrhcsFMMfFFhFp
        jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
        PmmdzqPrVvPwwTWBwg
        wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
        ttgJtRGJQctTZtZT
        CrZsJsPPZsGzwwsLwLmpwMDw
        """.Split(Environment.NewLine);

        var result = DayThree.PartOne(input);

        result.Should().Be(157);
    }

    [Fact]
    public void PartTwo_OnRuckSack_Return()
    {
        var input = """
        vJrwpWtwJgWrhcsFMMfFFhFp
        jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
        PmmdzqPrVvPwwTWBwg
        wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
        ttgJtRGJQctTZtZT
        CrZsJsPPZsGzwwsLwLmpwMDw
        """.Split(Environment.NewLine);

        var result = DayThree.PartTwo(input);

        result.Should().Be(70);
    }
}