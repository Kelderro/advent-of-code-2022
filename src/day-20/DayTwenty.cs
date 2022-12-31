/// <summary>
/// Day 20: Grove Positioning System
/// https://adventofcode.com/2022/day/20
/// </summary>
namespace Aoc.Year2022.Day20;

public sealed class DayTwenty : IDay<int>
{
    public static int PartOne(string[] lines)
    {
        var originalArray = Parse(lines);
        var movingArray = originalArray.ToList();

        for (var i = 0; i < originalArray.Length; i++)
        {
            try
            {
                if (i == 4992)
                {
                    Console.WriteLine();
                }

                var moveItem = movingArray.IndexOf(originalArray[i]);
                Move(movingArray, moveItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        var result = CalculateCoordinates(movingArray);

        return result;
    }

    public static int PartTwo(string[] lines)
    {
        throw new NotImplementedException();
    }

    private static int[] Parse(string[] lines)
    {
        return lines.Select(int.Parse)
                    .ToArray();
    }

    private static void Move(IList<int> arr, int fromIndex)
    {
        var objectToMove = arr[fromIndex];
        var moveBy = objectToMove;

        if (moveBy < 0)
            moveBy--;

        var toIndex = fromIndex + moveBy;

        if (toIndex >= arr.Count)
        {
            toIndex = (toIndex + 1) % arr.Count;
        }
        else if (toIndex < 0)
        {
            toIndex = arr.Count + (toIndex % arr.Count);
            if (toIndex == arr.Count)
            {
                toIndex = 0;
            }
        }

        // Console.WriteLine($"Moving number {objectToMove} between ? and ?:");

        arr.Remove(objectToMove);
        arr.Insert(toIndex, objectToMove);
    }

    /// <summary>
    /// Then, the grove coordinates can be found by looking at the 1000th, 2000th, and 3000th numbers after the value 0, wrapping around the list as necessary.
    /// </summary>
    /// <param name="arr"></param>
    /// <returns>Grove coordinates</returns>
    private static int CalculateCoordinates(IList<int> arr)
    {
        var position1000th = (1000 + arr.IndexOf(0)) % arr.Count;
        var position2000th = (2000 + arr.IndexOf(0)) % arr.Count;
        var position3000th = (3000 + arr.IndexOf(0)) % arr.Count;

        var number1000th = arr[position1000th];
        var number2000th = arr[position2000th];
        var number3000th = arr[position3000th];

        var result = number1000th + number2000th + number3000th;

        return result;
    }
}