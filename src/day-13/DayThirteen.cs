using System.Text.Json.Nodes;

/// <summary>
/// Day 13: Distress Signal
/// https://adventofcode.com/2022/day/13.
/// </summary>
namespace Aoc.Year2022.Day13;

public sealed class DayThirteen : IDay<int>
{
    public static int PartOne(string[] lines)
    {
        var pairIndex = 0;
        var correctPairs = 0;

        for (var i = 0; i < lines.Length; i = i + 3)
        {
            pairIndex++;

            var left = lines[i];
            var right = lines[i + 1];

            var jsonLeft = JsonNode.Parse(left);
            var jsonRight = JsonNode.Parse(right);

            var isCorrrect = Compare(jsonLeft, jsonRight);
            if (isCorrrect == true) correctPairs += pairIndex;
        }

        return correctPairs;
    }

    public static int PartTwo(string[] lines)
    {
        return 0;
    }

    private static bool? Compare(JsonNode left, JsonNode right)
    {
        // If both values are integers, the lower integer should come first.
        // If the left integer is lower than the right integer, the inputs are in the right order.
        // If the left integer is higher than the right integer, the inputs are not in the right order.
        // Otherwise, the inputs are the same integer; continue checking the next part of the input.
        if (left is JsonValue leftVal && right is JsonValue rightVal)
        {
            var leftInt = leftVal.GetValue<int>();
            var rightInt = rightVal.GetValue<int>();
            return leftInt == rightInt ? null : leftInt < rightInt;
        }

        // If exactly one value is an integer, convert the integer to a list which contains that integer as its only value
        if (left is not JsonArray leftArray) leftArray = new JsonArray(left.GetValue<int>());
        if (right is not JsonArray rightArray) rightArray = new JsonArray(right.GetValue<int>());

        // If both values are lists, compare the first value of each list, then the second value, and so on.
        for (var i = 0; i < Math.Min(leftArray.Count, rightArray.Count); i++)
        {
            var res = Compare(leftArray[i], rightArray[i]);

            // Check if comparison makes a decision about the order, continue checking the next part of the input if returned null.
            if (res.HasValue)
            {
                return res.Value;
            }
        }

        // If the left list runs out of items first, the inputs are in the right order.
        // If the right list runs out of items first, the inputs are not in the right order.
        if (leftArray.Count < rightArray.Count) { return true; }
        if (leftArray.Count > rightArray.Count) { return false; }

        return null;
    }

    public class Packet
    {
        public int Number { get; set; }

        public IList<Packet> List { get; set; } = new List<Packet>();
    }

}
