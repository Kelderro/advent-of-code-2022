

public sealed class DayTen : IDay<int>
{
    private static List<int> signalStrengths = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lines"></param>
    /// <summary></summary>
    /// <returns></returns>


    public static int PartOne(string[] lines)
    {
        var register = 1;
        var cycle = 0;

        foreach (var line in lines)
        {
            cycle++;

            CalculateSignalStrength(register, cycle);

            if (line.StartsWith("addx "))
            {
                // Increase the cycle number as we shouldn't add the value directly to the register
                cycle++;

                // Check if we need to calculate the signal strength as we increased the cycle
                CalculateSignalStrength(register, cycle);

                var lineValue = int.Parse(line.Split(' ')[1]);

                register += lineValue;
            }
        }

        // Add all the signal together and return
        return signalStrengths.Sum();
    }

    private static void CalculateSignalStrength(int register, int cycle)
    {
        if (cycle > 220)
            return;

        if (cycle == 20 ||
            (cycle - 20) % 40 == 0)
        {
            signalStrengths.Add(cycle * register);
            Console.WriteLine($"Cycle: {cycle} | Register: {register} | SignalStrength: {signalStrengths.Last()}");
        }
    }

    public static int PartTwo(string[] lines)
    {
        return 0;
    }
}
