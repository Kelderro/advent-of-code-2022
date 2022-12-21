public class Monkey
{
    /// <summary>
    /// Lists your worry level for each item the monkey is currently holding
    /// in the order they will be inspected.
    /// </summary>
    public Queue<long> WorryLevelPerItem { get; init; }

    /// <summary>
    /// Total number of times by the monkey.
    /// </summary>
    public int Inspections { get; set; }

    /// <summary>
    /// Operation shows how your worry level changes as that monkey inspects
    /// an item. (An operation like new = old * 5 means that your worry level
    /// after the monkey inspected the item is five times whatever your worry
    /// level was before inspection.)
    /// </summary>
    public Func<long, long> WorryFunction { get; init; }

    /// <summary>
    /// Used by ThrowToNext.
    /// </summary>
    public int DivisibleBy { get; init; }

    /// <summary>
    /// Test shows how the monkey uses your worry level to decide where to
    /// throw an item next. If true shows what happens with an item if the
    /// Test was true. If false shows what happens with an item if the Test
    /// was false.
    /// </summary>
    public Func<long, int> ThrowToNext { get; init; }

    /// <summary>
    /// If true shows what happens with an item if the Test was true.
    /// </summary>
    public int TrueThrowTo { get; init; }

    /// <summary>
    /// If false shows what happens with an item if the Test was false.
    /// </summary>
    public int FalseThrowTo { get; init; }

    /// <summary>
    /// Initialize a monkey based on a string monkey description.
    /// </summary>
    /// <param name="monkeyDescription">Description on the monkey.</param>
    public Monkey(string[] monkeyDescription)
    {
        this.WorryLevelPerItem = ParseStartingItems(monkeyDescription[0]);
        this.WorryFunction = ParseOperation(monkeyDescription[1]);
        this.DivisibleBy = ParseDivisibleBy(monkeyDescription[2]);
        this.ThrowToNext = ParseThrowToNextTest();
        this.TrueThrowTo = ParseThrowTo(monkeyDescription[3]);
        this.FalseThrowTo = ParseThrowTo(monkeyDescription[4]);
    }

    /// <summary>
    /// Convert the items worry levels to an array.
    /// Sample input: Starting items: 54, 65, 75, 74
    /// </summary>
    /// <param name="itemsWorryLevels">Description of the item worry levels.</param>
    /// <returns>Worry level per item.</returns>
    private Queue<long> ParseStartingItems(string itemsWorryLevels)
    {
        var startingItems = itemsWorryLevels.Split(':')[1].Trim();
        return new Queue<long>(startingItems.Split(',')
                            .Select(long.Parse));
    }

    /// <summary>
    /// Dictionary containing all the supported operators.
    /// </summary>
    private static Dictionary<char, Func<long, long, long>> operators = new()
    {
        { '+', (x, y) => x + y },
        { '*', (x, y) => x * y },
    };

    /// <summary>
    /// Convert the operation line to an worry function.
    /// Sample input: Operation: new = old * 19
    ///               Operation: new = old + old
    /// </summary>
    /// <param name="operationLine">Description of the operation.</param>
    /// <returns>Worry function.</returns>
    private Func<long, long> ParseOperation(string operationLine)
    {
        // Retrieve the operator from the operation line
        // Transform "Operation: new = old * 19" to '*'
        var operatorPart = operationLine.Substring(operationLine.IndexOf("old ") + 4, 1)[0];

        // Retrieve the operation part after the operator
        // Transform "Operation: new = old * 19" to "19"
        var operationPart = operationLine.Substring(operationLine.LastIndexOf(' ') + 1);

        int amount;
        // The operation part can be numeric or can hold the value "old"
        if (int.TryParse(operationPart, out amount))
        {
            return (worryLevel) => operators[operatorPart](worryLevel, amount);
        }

        // Handle situation where the operation part is "old"
        return (worryLevel) => operators[operatorPart](worryLevel, worryLevel);
    }

    /// <summary>
    /// Convert the divisible by line to the divisible by number.
    /// Sample input: Test: divisible by 19
    /// </summary>
    /// <param name="divisibleByLine">Description of the divisible by.</param>
    /// <returns>Divisible by number.</returns>
    private int ParseDivisibleBy(string divisibleByLine)
    {
        // Transform "Test: divisible by 19" to 19
        return int.Parse(divisibleByLine.Substring(divisibleByLine.LastIndexOf(' ') + 1));
    }

    /// <summary>
    /// Determine if the worry level can be divided by the DivisibleBy property.
    /// Based on the boolean outcome return the throw to value.
    /// </summary>
    /// <returns>Returns the monkey number to throw to</returns>
    private Func<long, int> ParseThrowToNextTest()
    {
        return (worryLevel) => worryLevel % this.DivisibleBy == 0
            ? this.TrueThrowTo
            : this.FalseThrowTo;
    }

    /// <summary>
    /// Convert the throw to line to the monkey number.
    /// Sample input: If true: throw to monkey 2
    ///               If false: throw to monkey 0
    /// </summary>
    /// <param name="throwToLine">Description of the throw to action</param>
    /// <returns>The monkey number to throw to.</returns>
    private int ParseThrowTo(string throwToLine)
    {
        // Transform "If true: throw to monkey 2" to 2
        return int.Parse(throwToLine.Substring(throwToLine.LastIndexOf(' ') + 1));
    }
}