namespace Aoc.Year2022;

public interface IDay<T>
{
    static abstract T PartOne(string[] lines);

    static abstract T PartTwo(string[] lines);
}
