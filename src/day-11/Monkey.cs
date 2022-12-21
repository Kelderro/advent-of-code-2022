public class Monkey
{
    /// <summary>
    /// Lists your worry level for each item the monkey is currently holding
    /// in the order they will be inspected.
    /// </summary>
    public Queue<int> Items { get; init; }

    /// <summary>
    /// Total number of times by the monkey
    /// </summary>
    public int Inspections { get; set; }

    /// <summary>
    /// Operation shows how your worry level changes as that monkey inspects
    /// an item. (An operation like new = old * 5 means that your worry level
    /// after the monkey inspected the item is five times whatever your worry
    /// level was before inspection.)
    /// </summary>
    public Func<int, int> Operation { get; init; }

    /// <summary>
    /// Test shows how the monkey uses your worry level to decide where to
    /// throw an item next. If true shows what happens with an item if the
    /// Test was true. If false shows what happens with an item if the Test
    /// was false.
    /// </summary>
    public Func<int, int> ThrowToNext { get; init; }

    /// <summary>
    /// If true shows what happens with an item if the Test was true.
    /// </summary>
    public int TrueThrowTo { get; init; }

    /// <summary>
    /// If false shows what happens with an item if the Test was false.
    /// </summary>
    public int FalseThrowTo { get; init; }

    public Monkey(string[] monkeyDescription)
    {
        this.Items = ParseStartingItems(monkeyDescription[0]);
        this.Operation = ParseOperation(monkeyDescription[1]);
        this.ThrowToNext = ParseThrowToNextTest(monkeyDescription[2]);
        this.TrueThrowTo = ParseThrowTo(monkeyDescription[3]);
        this.FalseThrowTo = ParseThrowTo(monkeyDescription[4]);
    }

    private Queue<int> ParseStartingItems(string line)
    {
        var startingItems = line.Split(':')[1].Trim();
        return new Queue<int>(startingItems.Split(',')
                            .Select(int.Parse));
    }

    private Func<int, int> ParseOperation(string line)
    {
        var operatorPart = line.Substring(line.IndexOf("old ") + 4, 1)[0];

        var operationPart = line.Substring(line.LastIndexOf(' ') + 1);

        if (operationPart.Equals("old", StringComparison.InvariantCultureIgnoreCase))
        {
            switch (operatorPart)
            {
                case '+':
                    return (worryLevel) => worryLevel + worryLevel;
                case '*':
                    return (worryLevel) => worryLevel * worryLevel;
                default:
                    throw new NotSupportedException();
            }

        }

        var amount = int.Parse(operationPart);

        switch (operatorPart)
        {
            case '+':
                return (worryLevel) => worryLevel + amount;
            case '*':
                return (worryLevel) => worryLevel * amount;
            default:
                throw new NotSupportedException();
        }
    }

    private Func<int, int> ParseThrowToNextTest(string line)
    {
        var divisibleBy = int.Parse(line.Substring(line.LastIndexOf(' ') + 1));

        return (worryLevel) => worryLevel % divisibleBy == 0
            ? this.TrueThrowTo
            : this.FalseThrowTo;
    }

    private int ParseThrowTo(string line)
    {
        return int.Parse(line.Substring(line.LastIndexOf(' ') + 1));
    }
}