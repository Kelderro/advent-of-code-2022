public sealed class DayTwo : IDay
{
    // First column
    // A: Rock
    // B: Paper
    // C: Scissors

    // Second column
    // X: Rock      - 1 point
    // Y: Paper     - 2 points
    // Z: Scissors  - 3 points

    // Lose: 0
    // Draw: 3
    // Win: 6

    private static Dictionary<char, int> CalcInputScore = new() {
        { 'X', 1 },
        { 'Y', 2 },
        { 'Z', 3 },
    };

    public static int PartOne(string[] lines)
    {
        var total = 0;
        foreach (var line in lines)
        {
            total += InputLine(line);
        }
        return total;
    }

    public static int PartTwo(string[] lines)
    {
        return 0;
    }

    private static int InputLine(string value)
    {
        return Score(value[0], value[2]);
    }

    private static int Score(char otherPlayerInput, char playerInput)
    {
        var score = CalcInputScore[playerInput];
        score += CalcOutcomeScore(otherPlayerInput, playerInput);

        return score;
    }

    private static int CalcOutcomeScore(char otherPlayerInput, char playerInput)
    {
        if ((int)otherPlayerInput == ((int)playerInput) - 23)
        {
            return 3;
        }

        var losePlaying = new Dictionary<char, char> {
            { 'A', 'Z' },
            { 'B', 'X' },
            { 'C', 'Y' },
        };

        var loseWhenPlaying = losePlaying[otherPlayerInput];

        if (loseWhenPlaying == playerInput)
        {
            return 0;
        }
        return 6;
    }
}