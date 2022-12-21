
/// <summary>
/// https://adventofcode.com/2022/day/4.
/// </summary>
public sealed class DayFour : IDay<int>
{
    public static int PartOne(string[] lines)
    {
        var total = 0;

        foreach (var line in lines)
        {
            var elvesAssignments = line.Split(',');
            var firstElfAssignment = RangeToSequence(elvesAssignments[0]);
            var secondElfAssignment = RangeToSequence(elvesAssignments[1]);

            if (!firstElfAssignment.Except(secondElfAssignment).Any()
             || !secondElfAssignment.Except(firstElfAssignment).Any())
            {
                total++;
                continue;
            }
        }

        return total;
    }

    public static int PartTwo(string[] lines)
    {
        var total = 0;

        foreach (var line in lines)
        {
            var elvesAssignments = line.Split(',');
            var firstElfAssignment = RangeToSequence(elvesAssignments[0]);
            var secondElfAssignment = RangeToSequence(elvesAssignments[1]);

            if (firstElfAssignment.Intersect(secondElfAssignment).Any())
            {
                total++;
            }
        }

        return total;
    }

    private static IEnumerable<int> RangeToSequence(string range)
    {
        var assignment = range.Split('-').Select(int.Parse);
        return Enumerable.Range(assignment.First(), assignment.Last() - assignment.First() + 1);
    }
}