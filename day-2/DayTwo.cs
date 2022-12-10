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

    private static readonly Dictionary<char, int> CalcInputScore = new() {
        { 'X', 1 },
        { 'Y', 2 },
        { 'Z', 3 },
    };

    public static int PartOne(string[] lines)
    {
        var total = 0;
        foreach (var line in lines)
        {
            total += Score(line[0], line[2]);
        }
        return total;
    }

    public static int PartTwo(string[] lines)
    {
        // A: 65 : Rock
        // B: 66 : Paper
        // C: 67 : Scissors

        // X: 88 : Rock
        // Y: 89 : Paper
        // Z: 90 : Scissors

        // X: Lose
        // Y: Draw
        // Z: Win

        var total = 0;
        foreach (var line in lines)
        {
            var otherPlayerInput = line[0];
            char playSymbol = PlayOccordingStateOfSecondColumn(line);

            total += Score(otherPlayerInput, playSymbol);
        }
        return total;
    }

    private static char PlayOccordingStateOfSecondColumn(string line)
    {
        var otherPlayerCharInt = (char)(int)line[0] + ('X' - 'A');

        if (line[2] == 'X') // Need to lose
        {
            var losingChar = otherPlayerCharInt - 1;
            return (losingChar == 87) ? 'Z' : (char)losingChar;
        }

        if (line[2] == 'Y') // Play a draw
        {
            return (char)otherPlayerCharInt;
        }

        // Need to win
        var winningChar = otherPlayerCharInt + 1;
        return (winningChar == 91) ? 'X' : (char)winningChar;
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