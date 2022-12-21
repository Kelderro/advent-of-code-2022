public class Monkey
{
    /// <summary>
    /// Dictionary containing all the supported operators.
    /// </summary>
    private static readonly Dictionary<char, Func<long, long, long>> Operators = new()
    {
        { '+', (x, y) => x + y },
        { '*', (x, y) => x * y },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="Monkey"/> class.
    /// Initialize a monkey based on a string monkey description.
    /// </summary>
    /// <param name="monkeyDescription">Description on the monkey.</param>
    public Monkey(string[] monkeyDescription)
    {
        this.WorryLevelPerItem = ParseStartingItems(monkeyDescription[0]);
        this.WorryFunction = ParseOperation(monkeyDescription[1]);
        this.DivisibleBy = ParseDivisibleBy(monkeyDescription[2]);
        this.ThrowToNext = this.ParseThrowToNextTest();
        this.TrueThrowTo = ParseThrowTo(monkeyDescription[3]);
        this.FalseThrowTo = ParseThrowTo(monkeyDescription[4]);
    }

    /// <summary>
    /// Gets lists of worry level per item that the monkey is holding.
    /// </summary>
    public Queue<long> WorryLevelPerItem { get; init; }

    /// <summary>
    /// Gets or sets total number of times the monkey inspected an item.
    /// </summary>
    public int Inspections { get; set; }

    /// <summary>
    /// Gets the function that calculates the worry level change for each time
    /// a monkey inspect an item. (An operation like new = old * 5 means that
    /// your worry level after the monkey inspected the item is five times
    /// whatever your worry level was before inspection.)
    /// </summary>
    public Func<long, long> WorryFunction { get; init; }

    /// <summary>
    /// Gets the divisible by number that is used by the ThrowToNext function.
    /// </summary>
    public int DivisibleBy { get; init; }

    /// <summary>
    /// Gets the function that test if the worry level can be divided by the 
    /// DivisibleBy property. If true shows what happens with an item if the
    /// Test was true. If false shows what happens with an item if the Test
    /// was false.
    /// </summary>
    public Func<long, int> ThrowToNext { get; init; }

    /// <summary>
    /// Gets the monkey number when the ThrowToNext case is true.
    /// </summary>
    public int TrueThrowTo { get; init; }

    /// <summary>
    /// Gets the monkey number when the ThrowToNext case is false.
    /// </summary>
    public int FalseThrowTo { get; init; }

    /// <summary>
    /// Convert the items worry levels to an array.
    /// Sample input: Starting items: 54, 65, 75, 74.
    /// </summary>
    /// <param name="itemsWorryLevels">Description of the item worry levels.</param>
    /// <returns>Worry level per item.</returns>
    private static Queue<long> ParseStartingItems(string itemsWorryLevels)
    {
        var startingItems = itemsWorryLevels.Split(':')[1].Trim();
        return new Queue<long>(startingItems.Split(',')
                            .Select(long.Parse));
    }

    /// <summary>
    /// Convert the operation line to an worry function.
    /// Sample input: Operation: new = old * 19
    ///               Operation: new = old + old.
    /// </summary>
    /// <param name="operationLine">Description of the operation.</param>
    /// <returns>Worry function.</returns>
    private static Func<long, long> ParseOperation(string operationLine)
    {
        // Retrieve the operator from the operation line
        // Transform "Operation: new = old * 19" to '*'
        var operatorPart = operationLine.Substring(operationLine.IndexOf("old ") + 4, 1)[0];

        // Retrieve the operation part after the operator
        // Transform "Operation: new = old * 19" to "19"
        var operationPart = operationLine[(operationLine.LastIndexOf(' ') + 1)..];

        // The operation part can be numeric or can hold the value "old"
        if (int.TryParse(operationPart, out int amount))
        {
            return (worryLevel) => Operators[operatorPart](worryLevel, amount);
        }

        // Handle situation where the operation part is "old"
        return (worryLevel) => Operators[operatorPart](worryLevel, worryLevel);
    }

    /// <summary>
    /// Convert the divisible by line to the divisible by number.
    /// Sample input: Test: divisible by 19.
    /// </summary>
    /// <param name="divisibleByLine">Description of the divisible by.</param>
    /// <returns>Divisible by number.</returns>
    private static int ParseDivisibleBy(string divisibleByLine)
    {
        // Transform "Test: divisible by 19" to 19
        return int.Parse(divisibleByLine[(divisibleByLine.LastIndexOf(' ') + 1)..]);
    }

    /// <summary>
    /// Determine if the worry level can be divided by the DivisibleBy property.
    /// Based on the boolean outcome return the throw to value.
    /// </summary>
    /// <returns>Returns the monkey number to throw to.</returns>
    private Func<long, int> ParseThrowToNextTest()
    {
        return (worryLevel) => worryLevel % this.DivisibleBy == 0
            ? this.TrueThrowTo
            : this.FalseThrowTo;
    }

    /// <summary>
    /// Convert the throw to line to the monkey number.
    /// Sample input: If true: throw to monkey 2
    ///               If false: throw to monkey 0.
    /// </summary>
    /// <param name="throwToLine">Description of the throw to action.</param>
    /// <returns>The monkey number to throw to.</returns>
    private static int ParseThrowTo(string throwToLine)
    {
        // Transform "If true: throw to monkey 2" to 2
        return int.Parse(throwToLine[(throwToLine.LastIndexOf(' ') + 1)..]);
    }
}