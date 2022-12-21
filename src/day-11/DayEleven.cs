using System.Text;

/// <summary>
/// https://adventofcode.com/2022/day/11
/// </summary>
public abstract class DayElevent : IDay<long>
{
    /// <summary>
    /// Figure out which monkeys to chase by counting how many items they
    /// inspect over 20 rounds. What is the level of monkey business after
    /// 20 rounds of stuff-slinging simian shenanigans?
    /// </summary>
    /// <param name="lines">Instructions</param>
    /// <returns>Level of monkey business after 20 rounds.</returns>
    public static long PartOne(string[] lines)
    {
        var monkeyBusiness = Process(lines, 20, true);

        return monkeyBusiness;
    }

    /// <summary>
    /// What is the level of monkey business after 10000 rounds.
    /// </summary>
    /// <param name="lines">Instructions</param>
    /// <returns>Level of monkey business after 10000 rounds</returns>
    public static long PartTwo(string[] lines)
    {
        var monkeyBusiness = Process(lines, 10000);

        return monkeyBusiness;
    }

    /// <summary>
    /// Go over the instructions (lines) and run the process to calculate the monkey business.
    /// </summary>
    /// <param name="lines">Instructions</param>
    /// <param name="maxRounds">Maximum number of rounds.</param>
    /// <param name="inspectionReduceWorryLevel">Consider a reduction of worry
    /// level per item per inspection.</param>
    /// <returns>Monkey business amount.</returns>
    private static long Process(string[] lines, int maxRounds, bool inspectionReduceWorryLevel = false)
    {
        var monkeys = CreateMonkeys(lines);

        // Modular arithmetic
        // Required to prevent an overflow on the integer by using least common multiplier (LCM) as the modulo
        // This is not required for part one
        var lcm = monkeys.Select(x => x.DivisibleBy)
                         .Aggregate(1, (a, b) => a * b);

        for (var round = 1; round <= maxRounds; round++)
        {
            for (var i = 0; i < monkeys.Count(); i++)
            {
                var monkey = monkeys[i];

                while (monkey.WorryLevelPerItem.Any())
                {
                    monkey.Inspections++;
                    var itemWorryLevel = monkey.WorryLevelPerItem.Dequeue();

                    itemWorryLevel = monkey.WorryFunction(itemWorryLevel);

                    itemWorryLevel = UpdateInspectionWorryLevel(inspectionReduceWorryLevel, lcm, itemWorryLevel);

                    var throwToMonkey = monkey.ThrowToNext(itemWorryLevel);
                    monkeys[throwToMonkey].WorryLevelPerItem.Enqueue(itemWorryLevel);
                }
            }

#if DEBUG
            if (round % 1000 == 0 || round == 1 || round == 20)
            {
                Console.WriteLine(ReportInspectionRate(monkeys, round));
            }
#endif
        }

        var monkeyBusiness = CalculateMonkeyBusiness(monkeys);
        return monkeyBusiness;
    }

    /// <summary>
    /// Tranform the monkey description lines to monkey objects.
    /// </summary>
    /// <param name="monkeyDescriptions">Description of a monkey.</param>
    /// <returns>Monkeys based on the provided lines.</returns>
    private static IList<Monkey> CreateMonkeys(string[] monkeyDescriptions)
    {
        var monkeys = new List<Monkey>();
        // Only 5 lines of the monkey description are relevant
        var monkeyDescriptionLength = 7;

        for (var i = 1; i < monkeyDescriptions.Length; i = i + monkeyDescriptionLength)
        {
            // - 2: Skipping the monkey identifier line (first line) and white line at the end
            var singleMonkeyDescription = monkeyDescriptions[i..(i + monkeyDescriptionLength - 2)];
            monkeys.Add(new Monkey(singleMonkeyDescription));
        }

        return monkeys;
    }

    /// <summary>
    /// Calculate the item worry level per inspection.
    /// </summary>
    /// <param name="inspectionReduceWorryLevel">Reduce the worry level by 3</param>
    /// <param name="lcm">Least common multiplier to reduce the worry level number. Preventing
    /// overflowing the item worry levels.</param>
    /// <param name="itemWorryLevel">Current level of the item worry level.</param>
    /// <returns>New item worry level.</returns>
    private static long UpdateInspectionWorryLevel(
        bool inspectionReduceWorryLevel,
        int lcm,
        long itemWorryLevel)
    {
        if (inspectionReduceWorryLevel)
        {
            var inspectionReduceWorryLevelBy = 3d;
            itemWorryLevel = (int)Math.Floor(itemWorryLevel / inspectionReduceWorryLevelBy);
        }

        return itemWorryLevel % lcm;
    }

    /// <summary>
    /// Generate a report of the number of inspected items per monkey. Useful
    /// for debugging but not required to get to the answer.
    /// </summary>
    /// <param name="monkeys">Monkey to report on.</param>
    /// <returns>A report containing the number of inspected items per monkey</returns>
    private static string ReportInspectionRate(IList<Monkey> monkeys, int round)
    {
        Console.WriteLine($"== After round {round}");

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
    /// <param name="monkeys">Applicable monkeys.</param>
    /// <returns>Monkey business amount.</returns>
    private static long CalculateMonkeyBusiness(IList<Monkey> monkeys)
    {
        var mostActiveMonkeys = monkeys.OrderByDescending(x => x.Inspections)
                                       .Take(2)
                                       .Select(x => x.Inspections)
                                       .Aggregate(1L, (a, b) => a * b);

        return mostActiveMonkeys;
    }
}
