using System.Text;

/// <summary>
/// https://adventofcode.com/2022/day/11
/// </summary>
public abstract class DayElevent : IDay<long>
{
    #region Part One
    /// <summary>
    /// Figure out which monkeys to chase by counting how many items they
    /// inspect over 20 rounds. What is the level of monkey business after
    /// 20 rounds of stuff-slinging simian shenanigans?
    /// </summary>
    /// <param name="lines">Instructions</param>
    /// <returns>Level of monkey business after 20 rounds</returns>
    public static long PartOne(string[] lines)
    {
        var monkeyBusiness = Calculate(lines, 20, 3);

        return monkeyBusiness;
    }

    private static long Calculate(string[] lines, int maxRounds, int inspectionWorryLevel)
    {
        var monkeys = CreateMonkeys(lines);

        var lcm = monkeys.Select(x => x.DivisibleBy)
                         .Aggregate(1, (a, b) => a * b);

        var round = 1;
        while (round <= maxRounds)
        {
            for (var i = 0; i < monkeys.Count(); i++)
            {
                var monkey = monkeys[i];

                while (monkey.Items.Any())
                {
                    monkey.Inspections++;
                    var itemWorryLevel = monkey.Items.Dequeue();

                    itemWorryLevel = monkey.WorryFunction(itemWorryLevel);

                    if (inspectionWorryLevel == 3)
                    {
                        itemWorryLevel = (int)Math.Floor(itemWorryLevel / (decimal)inspectionWorryLevel);
                    }
                    else
                    {
                        itemWorryLevel = itemWorryLevel % lcm;
                    }

                    var throwToMonkey = monkey.ThrowToNext(itemWorryLevel);
                    monkeys[throwToMonkey].Items.Enqueue(itemWorryLevel);
                }
            }

#if DEBUG
            if (round % 1000 == 0 || round == 1 || round == 20)
            {
                Console.WriteLine($"== After round {round}");
                Console.WriteLine(ReportInspectionRate(monkeys));
            }
#endif
            round++;
        }

        var monkeyBusiness = CalculateMonkeyBusiness(monkeys);
        return monkeyBusiness;
    }

    private static IList<Monkey> CreateMonkeys(string[] lines)
    {
        var monkeys = new List<Monkey>();

        for (var i = 1; i < lines.Length; i = i + 7)
        {
            var part = lines[i..(i + 5)];
            monkeys.Add(new Monkey(part));
        }

        return monkeys;
    }

    private static string ReportInspectionRate(IList<Monkey> monkeys)
    {
        var result = new StringBuilder();
        for (var i = 0; i < monkeys.Count; i++)
        {
            result.AppendLine($"Monkey {i} inspected items {monkeys[i].Inspections} times.");
        }
        return result.ToString();
    }

    /// <summary>
    /// Calculate the monkey business by taking the most two active monkeys
    /// and multiplying their number of inspections.
    /// </summary>
    /// <param name="monkeys">Applicable monkeys</param>
    /// <returns>Monkey business amount</returns>
    private static long CalculateMonkeyBusiness(IList<Monkey> monkeys)
    {
        var mostActiveMonkeys = monkeys.OrderByDescending(x => x.Inspections)
                                       .Take(2)
                                       .Select(x => x.Inspections)
                                       .Aggregate(1L, (a, b) => a * b);

        return mostActiveMonkeys;
    }
    #endregion

    public static long PartTwo(string[] lines)
    {
        var monkeyBusiness = Calculate(lines, 10000, 1);

        return monkeyBusiness;
    }
}
