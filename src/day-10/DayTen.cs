/// <summary>
/// https://adventofcode.com/2022/day/10.
/// </summary>

namespace Aoc.Year2022.Day10;

using System.Text;

public sealed class DayTen : IDay<string>
{
    /// <summary>
    /// Find the signal strength during the 20th, 60th, 100th, 140th, 180th, and 220th cycles.
    /// </summary>
    /// <param name="lines">Instructions.</param>
    /// <returns>Sum of the six signal strengths.</returns>
    public static string PartOne(string[] lines)
    {
        var totalSignalStrength = 0;

        var register = 1;
        var cycle = 0;

        foreach (var line in lines)
        {
            cycle++;

            totalSignalStrength += CalculateCycleSignalStrength(register, cycle);

            if (line.StartsWith("addx "))
            {
                // Increase the cycle number as the addx takes a second cycle to execute
                cycle++;

                // Check if we need to calculate the signal strength as we increased the cycle
                totalSignalStrength += CalculateCycleSignalStrength(register, cycle);

                var lineValue = int.Parse(line.Split(' ')[1]);

                register += lineValue;
            }
        }

        // Add all the signal together and return
        return totalSignalStrength.ToString();
    }

    public static string PartTwo(string[] lines)
    {
        // The sprite is three pixels wide
        // X is set in the middle of the sprite
        // CRT width is 40 pixels with a maximum of 6 rows
        var register = 1;
        var cycle = 1;

        var crtScreen = new bool[6, 40];

        WriteToLog($"Sprite position: {ToSprite(register)}");
        WriteToLog(string.Empty);

        foreach (var line in lines)
        {
            WriteToLog($"Start cycle   {cycle}: begin executing {line}");
            WriteToLog($"During cycle  {cycle}: CRT draws pixel in position {cycle - 1}");

            UpdateScreen(crtScreen, cycle, register);
            WriteToLog($"Current CRT Row: {CrtToString(crtScreen)}");

            if (line.StartsWith("addx "))
            {
                // Increase the cycle number as we shouldn't add the value directly to the register
                cycle++;

                WriteToLog($"During cycle  {cycle}: CRT draws pixel in position {cycle - 1}");
                UpdateScreen(crtScreen, cycle, register);
                WriteToLog($"Current CRT Row: {CrtToString(crtScreen)}");

                // Check if we need to calculate the signal strength as we increased the cycle
                var lineValue = int.Parse(line.Split(' ')[1]);

                register += lineValue;

                WriteToLog($"End of cycle  {cycle}: finish executing {line} (Register X is now {register})");
                WriteToLog($"Sprite position: {ToSprite(register)}");
            }
            else
            {
                WriteToLog($"End of cycle  {cycle}: finish executing noop");
            }

            WriteToLog(string.Empty);

            cycle++;
        }

        var result = CrtToString(crtScreen);

        return result;
    }

    /// <summary>
    /// Calculate the cycle signal strength only for the 20th, 60th, 100th,
    /// 140th, 180th, and 220th cycle.
    /// </summary>
    /// <param name="register">Current value of the register (X).</param>
    /// <param name="cycle">CPU cycle.</param>
    /// <returns>0 when the cycle is not the 20th, 60th, 100th, 140th, 180th
    /// or 220th cycle. Otherwise returns the product of cycle * register.
    /// </returns>
    private static int CalculateCycleSignalStrength(int register, int cycle)
    {
        if (cycle > 220 || (cycle != 20 && (cycle - 20) % 40 != 0))
        {
            return 0;
        }

        return cycle * register;
    }

    private static string ToSprite(int value)
    {
        return "###".PadLeft(value + 2, '.')
                    .PadRight(40, '.');
    }

    private static string CrtToString(bool[,] crtScreen)
    {
        var stringBuilder = new StringBuilder(Environment.NewLine);

        for (var scanLine = 0; scanLine <= crtScreen.GetUpperBound(0); scanLine++)
        {
            for (var i = 0; i <= crtScreen.GetUpperBound(1); i++)
            {
                stringBuilder.Append(crtScreen[scanLine, i] ? "#" : ".");
            }

            stringBuilder.Append(Environment.NewLine);
        }

        return stringBuilder.ToString();
    }

    private static void UpdateScreen(bool[,] crtScreen, int cycle, int register)
    {
        // Calculate the scan line
        var scanLine = (int)Math.Ceiling(cycle / 40d) - 1;

        // Handle pixel on the line
        if (cycle % 40 == 1)
        {
            crtScreen[scanLine, 0] = true;
            return;
        }

        var remainder = Math.Abs(((cycle % 40) - 1) - register);
        if (remainder == 0 || remainder == 1 || remainder == 39)
        {
            crtScreen[scanLine, (cycle - 1) % 40] = true;
        }
    }

    private static void WriteToLog(string logMsg)
    {
#if DEBUG
        Console.WriteLine(logMsg);
#endif
    }
}
