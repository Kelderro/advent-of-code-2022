using Xunit;
using FluentAssertions;

public class DaySevenTests
{
    private string[] input = """
        $ cd /
        $ ls
        dir a
        14848514 b.txt
        8504156 c.dat
        dir d
        $ cd a
        $ ls
        dir e
        29116 f
        2557 g
        62596 h.lst
        $ cd e
        $ ls
        584 i
        $ cd ..
        $ cd ..
        $ cd d
        $ ls
        4060174 j
        8033020 d.log
        5626152 d.ext
        7214296 k
        """.Split(Environment.NewLine);

    [Fact]
    public void PartOne_OnExampleTestCase_ReturnTotalSizeOfAllDirectories()
    {
        var result = DaySeven.PartOne(this.input);

        result.Should().Be(95437);
    }

    [Fact]
    public void PartTwo_OnExampleTestCase_ReturnTotalSizeOfAllDirectories()
    {
        var result = DaySeven.PartTwo(this.input);

        result.Should().Be(24933642);
    }
}