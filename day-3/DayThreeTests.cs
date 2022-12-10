using Xunit;
using FluentAssertions;

public class DayThreeTests
{
    [Fact]
    public void PartOne_On_Return()
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