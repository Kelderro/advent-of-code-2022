using System.Text;
/// <summary>
/// https://adventofcode.com/2022/day/11
/// </summary>
public abstract class DayElevent : IDay<int>
{
    #region Part One
    /// <summary>
    /// Figure out which monkeys to chase by counting how many items they
    /// inspect over 20 rounds. What is the level of monkey business after
    /// 20 rounds of stuff-slinging simian shenanigans?
    /// </summary>
    /// <param name="lines">Instructions</param>
    /// <returns>Level of monkey business after 20 rounds</returns>
    public static int PartOne(string[] lines)
    {
        var monkeys = new List<Monkey>();

        for (var i = 1; i < lines.Length; i = i + 7)
        {
            var part = lines[i..(i + 5)];
            monkeys.Add(new Monkey(part));
        }

        var round = 1;
        while (round < 21)
        {
            for (var i = 0; i < monkeys.Count(); i++)
            {
                var monkey = monkeys[i];

                while (monkey.Items.Any())
                {
                    monkey.Inspections++;
                    var itemWorryLevel = monkey.Items.Dequeue();

                    itemWorryLevel = monkey.Operation(itemWorryLevel);

                    // Reduce worry level
                    itemWorryLevel = (int)Math.Floor(itemWorryLevel / 3d);

                    var throwToMonkey = monkey.ThrowToNext(itemWorryLevel);
                    monkeys[throwToMonkey].Items.Enqueue(itemWorryLevel);
                }
            }
            round++;
        }

        var monkeyBusiness = CalculateMonkeyBusiness(monkeys);

        return monkeyBusiness;
    }

    /// <summary>
    /// Calculate the monkey business by taking the most two active monkeys
    /// and multiplying their number of inspections.
    /// </summary>
    /// <param name="monkeys">Applicable monkeys</param>
    /// <returns>Monkey business amount</returns>
    private static int CalculateMonkeyBusiness(List<Monkey> monkeys)
    {
        var mostActiveMonkeys = monkeys.OrderByDescending(x => x.Inspections)
                                       .Take(2)
                                       .Select(x => x.Inspections)
                                       .Aggregate(1, (a, b) => a * b);

        return mostActiveMonkeys;
    }
    #endregion

    public static int PartTwo(string[] lines)
    {
        return 0;
    }
}