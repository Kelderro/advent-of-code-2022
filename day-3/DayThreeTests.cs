using Xunit;
using FluentAssertions;

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
}