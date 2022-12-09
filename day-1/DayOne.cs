public static class DayOne
{
    public static int PartOne(string[] lines)
    {
        var calc = TotalPerElf(lines);

        return calc.Max();
    }

    public static int PartTwo(string[] lines)
    {
        var totals = TotalPerElf(lines);

        return totals.OrderByDescending(x => x)
                   .Take(3)
                   .Sum();
    }

    private static List<int> TotalPerElf(string[] lines)
    {
        var elves = new List<int>();
        var count = 0;
        var amount = 0;

        foreach (var line in lines)
        {
            if (line == string.Empty)
            {
                elves.Add(amount);
                amount = 0;
                count++;
                continue;
            }
            amount += int.Parse(line);
        }
        elves.Add(amount);
        return elves;
    }
}
