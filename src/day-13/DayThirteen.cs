/// <summary>
/// Day 13: Distress Signal
/// https://adventofcode.com/2022/day/13.
/// </summary>
namespace Aoc.Year2022.Day13;

using System.Text.Json.Nodes;

public sealed class DayThirteen : IDay<int>
{
    private enum SignalOrder
    {
        Ordered = -1,
        Unordered = 0,
        Undetermined = 1,
    }

    public static int PartOne(string[] lines)
    {
        var pairIndex = 0;
        var correctPairs = 0;

        var packets = GetPackets(lines);

        for (var i = 0; i < packets.Count; i = i + 2)
        {
            pairIndex++;

            var left = packets[i];
            var right = packets[i + 1];

            var signalOrder = Compare(left, right);
            if (signalOrder == SignalOrder.Ordered)
            {
                correctPairs += pairIndex;
            }
        }

        return correctPairs;
    }

    public static int PartTwo(string[] lines)
    {
        var dividers = GetPackets(new[] { "[[2]]", "[[6]]" });
        var packets = GetPackets(lines).Concat(dividers).ToList();

        packets.Sort((left, right) => Compare(left, right) == SignalOrder.Ordered ? -1 : 1);

        return (packets.IndexOf(dividers[0]) + 1) * (packets.IndexOf(dividers[1]) + 1);
    }

    private static IList<JsonNode> GetPackets(string[] input) => input.Where(x => !string.IsNullOrEmpty(x))
                                                                             .Select(x => JsonNode.Parse(x))
                                                                             .ToList();

    private static SignalOrder Compare(JsonNode left, JsonNode right)
    {
        // If both values are integers, the lower integer should come first.
        // If the left integer is lower than the right integer, the inputs are in the right order.
        // If the left integer is higher than the right integer, the inputs are not in the right order.
        // Otherwise, the inputs are the same integer; continue checking the next part of the input.
        if (left is JsonValue leftVal && right is JsonValue rightVal)
        {
            var leftInt = leftVal.GetValue<int>();
            var rightInt = rightVal.GetValue<int>();
            return leftInt == rightInt ? SignalOrder.Undetermined : leftInt < rightInt ? SignalOrder.Ordered : SignalOrder.Unordered;
        }

        // If exactly one value is an integer, convert the integer to a list which contains that integer as its only value
        if (left is not JsonArray leftArray)
        {
            leftArray = new JsonArray(left.GetValue<int>());
        }

        if (right is not JsonArray rightArray)
        {
            rightArray = new JsonArray(right.GetValue<int>());
        }

        // If both values are lists, compare the first value of each list, then the second value, and so on.
        for (var i = 0; i < Math.Min(leftArray.Count, rightArray.Count); i++)
        {
            var res = Compare(leftArray[i], rightArray[i]);

            // Check if comparison makes a decision about the order, continue checking the next part of the input is undetermined.
            if (res != SignalOrder.Undetermined)
            {
                return res;
            }
        }

        // If the left list runs out of items first, the inputs are in the right order.
        if (leftArray.Count < rightArray.Count)
        {
            return SignalOrder.Ordered;
        }

        // If the right list runs out of items first, the inputs are not in the right order.
        if (leftArray.Count > rightArray.Count)
        {
            return SignalOrder.Unordered;
        }

        return SignalOrder.Undetermined;
    }
}
