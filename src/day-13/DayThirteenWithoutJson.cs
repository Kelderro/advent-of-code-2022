/// <summary>
/// Day 13: Distress Signal
/// https://adventofcode.com/2022/day/13.
/// </summary>
namespace Aoc.Year2022.Day13;

public sealed class DayThirteenWithoutJson : IDay<int>
{
    public static int PartOne(string[] lines)
    {
        var currentIndexSum = 0;

        var packages = lines.Where(x => !string.IsNullOrWhiteSpace(x))
                            .Select(Packet.Parse)
                            .ToArray();

        for (var i = 0; i < packages.Length; i += 2)
        {
            var left = packages[i];
            var right = packages[i + 1];

            if (left.CompareTo(right) < 0)
            {
                currentIndexSum += (i / 2) + 1;
            }
        }

        return currentIndexSum;
    }

    public static int PartTwo(string[] lines)
    {
        var dividers = new[] { "[[2]]", "[[6]]" }.Select(Packet.Parse)
                                                 .ToArray();

        var packages = lines.Where(x => !string.IsNullOrWhiteSpace(x))
                            .Select(Packet.Parse)
                            .ToList();

        packages.AddRange(dividers);

        packages.Sort();

        var dividerOneIndex = packages.IndexOf(dividers[0]) + 1;
        var dividerTwoIndex = packages.IndexOf(dividers[1]) + 1;

        return dividerOneIndex * dividerTwoIndex;
    }

    private class Packet : IComparable<Packet>
    {
        public static Packet Parse(string input)
        {
            var start = 0;
            return ParseInner(input, ref start);
        }

        public int CompareTo(Packet? other)
        {
            ArgumentNullException.ThrowIfNull(other);

            if (this is ValuePacket a && other is ValuePacket b)
            {
                return a.Value.CompareTo(b.Value);
            }

            var thisAsList = this is ListPacket list ? list : new ListPacket() { Packets = { this } };
            var otherAsList = other is ListPacket otherList ? otherList : new ListPacket() { Packets = { other } };

            for (var i = 0; i < Math.Min(thisAsList.Packets.Count, otherAsList.Packets.Count); i++)
            {
                var comparison = thisAsList.Packets[i].CompareTo(otherAsList.Packets[i]);
                if (comparison != 0)
                {
                    return comparison;
                }
            }

            return thisAsList.Packets.Count.CompareTo(otherAsList.Packets.Count);
        }

        private static Packet ParseInner(string input, ref int currentPosition)
        {
            if (char.IsDigit(input[currentPosition]))
            {
                var start = currentPosition;
                while (char.IsDigit(input[currentPosition]))
                {
                    currentPosition++;
                }

                var number = int.Parse(input.Substring(start, currentPosition - start));
                return new ValuePacket(number);
            }

            var listPacket = new ListPacket();
            currentPosition++;
            while (input[currentPosition] != ']')
            {
                if (input[currentPosition] == ',')
                {
                    currentPosition++;
                }

                listPacket.Packets.Add(ParseInner(input, ref currentPosition));
            }

            currentPosition++;
            return listPacket;
        }
    }

    private class ListPacket : Packet
    {
        public List<Packet> Packets { get; init; } = new();
    }

    private class ValuePacket : Packet
    {
        public ValuePacket(int value) => this.Value = value;

        public int Value { get; init; }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
