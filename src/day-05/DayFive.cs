
/// <summary>
/// https://adventofcode.com/2022/day/5
/// Code requires refactoring in a later stage.
/// </summary>
public sealed class DayFive : IDay<string>
{
    public static string PartOne(string[] lines)
    {
        // Locate stacks
        var (stackLines, movementLines) = Split(lines);

        var stacks = TransformToStacks(stackLines);

        stacks = RearrangePartOne(movementLines, stacks);

        var solution = GetSolution(stacks);
        return solution;
    }

    public static string PartTwo(string[] lines)
    {
        // Locate stacks
        var (stackLines, movementLines) = Split(lines);

        var stacks = TransformToStacks(stackLines);

        stacks = RearrangePartTwo(movementLines, stacks);

        var solution = GetSolution(stacks);
        return solution;
    }

    private static List<Stack<char>> RearrangePartTwo(string[] movementLines, List<Stack<char>> stacks)
    {
        var tempStack = new Stack<char>();
        foreach (var line in movementLines)
        {
            var splitLine = line.Split(' ');
            var move = int.Parse(splitLine[1]);
            var from = int.Parse(splitLine[3]);
            var to = int.Parse(splitLine[5]);

            for (var i = 1; i <= move; i++)
            {
                tempStack.Push(stacks[from - 1].Pop());
            }

            while (tempStack.Count > 0)
            {
                stacks[to - 1].Push(tempStack.Pop());
            }
        }

        return stacks;
    }

    private static string GetSolution(List<Stack<char>> stacks)
    {
        var result = string.Empty;
        foreach (var stack in stacks)
        {
            result += stack.Peek();
        }

        return result;
    }

    private static List<Stack<char>> RearrangePartOne(string[] movementLines, List<Stack<char>> stacks)
    {
        foreach (var line in movementLines)
        {
            var splitLine = line.Split(' ');
            var move = int.Parse(splitLine[1]);
            var from = int.Parse(splitLine[3]);
            var to = int.Parse(splitLine[5]);

            for (var i = 1; i <= move; i++)
            {
                stacks[to - 1].Push(stacks[from - 1].Pop());
            }
        }

        return stacks;
    }

    private static (string[] stacks, string[] movements) Split(string[] lines)
    {
        var splitBy = 0;
        while (lines[splitBy].Length != 0)
        {
            splitBy++;
        }

        return (lines[..splitBy], lines[(splitBy + 1)..]);
    }

    private static List<Stack<char>> TransformToStacks(string[] stackLines)
    {
        // Last line contains the amount of stacks
        var numberOfStacks = int.Parse(stackLines.Last()
                                                 .Trim()
                                                 .Split(' ')
                                                 .Last());

        var stacks = new List<Stack<char>>(numberOfStacks);

        for (var i = 0; i < numberOfStacks; i++)
        {
            stacks.Add(new Stack<char>());
        }

        for (var lineNumber = stackLines.Length - 2; lineNumber >= 0; lineNumber--)
        {
            var line = stackLines[lineNumber];
            var columnNumber = 1;
            var count = 0;

            while (columnNumber < line.Length)
            {
                var cellValue = line[columnNumber];

                if (!char.IsWhiteSpace(cellValue))
                {
                    stacks[count].Push(cellValue);
                }

                count++;
                columnNumber += 4;
            }
        }

        return stacks;
    }
}