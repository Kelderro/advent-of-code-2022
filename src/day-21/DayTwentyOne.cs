/// <summary>
/// Day 21: Monkey Math
/// https://adventofcode.com/2022/day/21
/// </summary>
namespace Aoc.Year2022.Day21;

using System.Numerics;

public sealed class DayTwentyOne : IDay<long>
{
    private const string RootKeyName = "root";
    private const string HumanKeyName = "humn";

    private static readonly Dictionary<char, Func<long, long, bool, long>> Operators = new()
    {
        { '+', (long a, long b, bool reverse) => reverse ? a - b : a + b },
        { '-', (long a, long b, bool reverse) => reverse ? a + b : a - b },
        { '*', (long a, long b, bool reverse) => reverse ? a / b : a * b },
        { '/', (long a, long b, bool reverse) => reverse ? a * b : a / b },
        { '=', (long _, long b, bool _) => b },
    };

    public static long PartOne(string[] lines)
    {
        var monkeys = ParseMonkeys(lines);
        return Calculate(RootKeyName, monkeys);
    }

    public static long PartTwo(string[] lines)
    {
        var monkeys = ParseMonkeys(lines);

        var rootMonkey = monkeys[RootKeyName] as MathMonkey;

        if (rootMonkey == null)
        {
            throw new Exception("Something did go wrong!");
        }

        // Override the root operator for part two
        rootMonkey.OperatorChar = '=';

        return ResolveMissingChild(monkeys[RootKeyName], 0, monkeys);
    }

    private static long ResolveMissingChild(Monkey monkey, long parentValue, IDictionary<string, Monkey> monkeys)
    {
        var numberMonkey = monkey as NumberMonkey;

        if (numberMonkey != null)
        {
            if (numberMonkey.Name.Equals(HumanKeyName, StringComparison.InvariantCultureIgnoreCase))
            {
                return parentValue;
            }

            // Number monkey
            return numberMonkey.Value;
        }

        var mathMonkey = monkey as MathMonkey;

        if (mathMonkey == null)
        {
            throw new Exception("Who introduced a third monkey type?");
        }

        var leftMonkeyName = mathMonkey.MonkeyNameLeft;
        var rightMonkeyName = mathMonkey.MonkeyNameRight;

        if (MonkeyInPath(HumanKeyName, monkeys[leftMonkeyName], monkeys))
        {
            // Found human in the left part of the operation
            var rightMonkeyValue = Calculate(rightMonkeyName, monkeys);
            var leftMonkeyValue = mathMonkey.Operator(parentValue, rightMonkeyValue, true);

            return ResolveMissingChild(monkeys[leftMonkeyName], leftMonkeyValue, monkeys);
        }

        if (MonkeyInPath(HumanKeyName, monkeys[rightMonkeyName], monkeys))
        {
            // Found human in the right part of the operation
            var leftMonkeyValue = Calculate(leftMonkeyName, monkeys);
            var rightMonkeyValue = 0L;

            switch (mathMonkey.OperatorChar)
            {
                case '/':
                case '-':
                    rightMonkeyValue = mathMonkey.Operator(leftMonkeyValue, parentValue, false);
                    break;
                default:
                    rightMonkeyValue = mathMonkey.Operator(parentValue, leftMonkeyValue, true);
                    break;
            }

            return ResolveMissingChild(monkeys[rightMonkeyName], rightMonkeyValue, monkeys);
        }

        throw new NotSupportedException("WRONG!");
    }

    private static bool MonkeyInPath(string searchForMonkeyByName, Monkey startMonkey, IDictionary<string, Monkey> monkeys)
    {
        if (startMonkey.Name.Equals(searchForMonkeyByName))
        {
            return true;
        }

        var mathMonkey = startMonkey as MathMonkey;

        if (mathMonkey == null)
        {
            // Found a number monkey
            return false;
        }

        return MonkeyInPath(searchForMonkeyByName, monkeys[mathMonkey.MonkeyNameLeft], monkeys)
            || MonkeyInPath(searchForMonkeyByName, monkeys[mathMonkey.MonkeyNameRight], monkeys);
    }

    private static long Calculate(string resolveMonkey, IDictionary<string, Monkey> monkeys)
    {
        var monkey = monkeys[resolveMonkey];
        var numberMonkey = monkey as NumberMonkey;

        if (numberMonkey != null)
        {
            return numberMonkey.Value;
        }

        var mathMonkey = monkey as MathMonkey;

        if (mathMonkey == null)
        {
            throw new Exception("Who introduced a third monkey type?");
        }

        var left = Calculate(mathMonkey.MonkeyNameLeft, monkeys);
        var right = Calculate(mathMonkey.MonkeyNameRight, monkeys);

        var value = mathMonkey.Operator(left, right, false);

        monkeys[monkey.Name] = new NumberMonkey
        {
            Name = monkey.Name,
            Value = value,
        };

        return value;
    }

    private static IDictionary<string, Monkey> ParseMonkeys(string[] lines)
    {
        var monkeys = new List<Monkey>();

        foreach (var line in lines)
        {
            monkeys.Add(ParseMonkey(line));
        }

        return monkeys.ToDictionary(x => x.Name);
    }

    private static Monkey ParseMonkey(string line)
    {
        var monkeyName = line[..line.IndexOf(':')];
        var instruction = line[6..];

        if (instruction.All(c => char.IsDigit(c)))
        {
            return new NumberMonkey
            {
                Name = monkeyName,
                Value = int.Parse(instruction),
            };
        }

        var instructionSplit = instruction.Split(' ');

        return new MathMonkey
        {
            Name = monkeyName,
            MonkeyNameLeft = instructionSplit[0],
            OperatorChar = instructionSplit[1][0],
            MonkeyNameRight = instructionSplit[2],
        };
    }

    public abstract class Monkey
    {
        required public string Name { get; init; }
    }

    public class NumberMonkey : Monkey
    {
        required public long Value { get; init; }
    }

    public class MathMonkey : Monkey
    {
        required public string MonkeyNameLeft { get; init; }

        public Func<long, long, bool, long> Operator => Operators[this.OperatorChar];

        required public char OperatorChar { get; set; }

        required public string MonkeyNameRight { get; init; }
    }
}