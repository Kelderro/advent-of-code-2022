
using System.Numerics;
/// <summary>
/// Day 21: Monkey Math
/// https://adventofcode.com/2022/day/21
/// </summary>
namespace Aoc.Year2022.Day21;

public sealed class DayTwentyOne : IDay<long>
{
    public static long PartOne(string[] lines)
    {
        var monkeys = ParseMonkeys(lines);
        return Process(monkeys);
    }

    public static long PartTwo(string[] lines)
    {
        throw new NotImplementedException();
    }

    private static long Process(IDictionary<string, Monkey> monkeys)
    {
        while (monkeys["root"] is not NumberMonkey)
        {
            foreach (var monkey in monkeys.Values)
            {
                var mathMonkey = monkey as MathMonkey;
                if (mathMonkey == null)
                {
                    // Number monkey
                    continue;
                }

                Console.WriteLine($"Working on monkey: {monkey.Name}");

                var monkeyA = monkeys[mathMonkey.MonkeyValueA] as NumberMonkey;
                var monkeyB = monkeys[mathMonkey.MonkeyValueB] as NumberMonkey;

                if (monkeyA != null && monkeyB != null)
                {
                    monkeys[monkey.Name] = new NumberMonkey
                    {
                        Name = monkey.Name,
                        Value = mathMonkey.Operator(monkeyA.Value, monkeyB.Value),
                    };
                }
            }
        }

        var rootMonkey = monkeys["root"] as NumberMonkey;

        if (rootMonkey == null)
        {
            throw new NotSupportedException("How did the code end up in this state?");
        }

        return rootMonkey.Value;
    }

    private static readonly Dictionary<char, Func<long, long, long>> Operators = new() {
        { '+', (long a, long b) => a + b },
        { '-', (long a, long b) => a - b },
        { '*', (long a, long b) => a * b },
        { '/', (long a, long b) => a / b }
    };

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
            MonkeyValueA = instructionSplit[0],
            Operator = Operators[instructionSplit[1][0]],
            MonkeyValueB = instructionSplit[2],
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
        required public string MonkeyValueA { get; init; }

        required public Func<long, long, long> Operator { get; init; }

        required public string MonkeyValueB { get; init; }
    }
}