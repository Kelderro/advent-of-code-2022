using Aoc.Year2022;

Resolve<Aoc.Year2022.Day01.DayOne, int>(1);
Resolve<Aoc.Year2022.Day02.DayTwo, int>(2);
Resolve<Aoc.Year2022.Day03.DayThree, int>(3);
Resolve<Aoc.Year2022.Day04.DayFour, int>(4);
Resolve<Aoc.Year2022.Day05.DayFive, string>(5);
Resolve<Aoc.Year2022.Day06.DaySix, int>(6);
Resolve<Aoc.Year2022.Day07.DaySeven, int>(7);
Resolve<Aoc.Year2022.Day08.DayEight, int>(8);
Resolve<Aoc.Year2022.Day09.DayNine, int>(9);
Resolve<Aoc.Year2022.Day10.DayTen, string>(10);
Resolve<Aoc.Year2022.Day11.DayEleven, long>(11);
Resolve<Aoc.Year2022.Day14.DayFourteen, int>(14);

void Resolve<TDayType, TReturnType>(int dayNumber)
    where TDayType : IDay<TReturnType>
{
    var lines = ReadInput(dayNumber);

    var partOneResult = TDayType.PartOne(lines);
    var partTwoResult = TDayType.PartTwo(lines);

    PrintToConsole(dayNumber, partOneResult, partTwoResult);
}

void PrintToConsole<TReturnType>(int dayNumber, TReturnType partOneResult, TReturnType partTwoResult)
{
    Console.WriteLine($"""
    #########################################
    # Solution for day number: {dayNumber}
    # Solution for part one:   {partOneResult}
    # Solution for part two:   {partTwoResult}
    #########################################{Environment.NewLine}
    """);
}

string[] ReadInput(int dayNumber)
{
    return File.ReadAllLines($"./src/day-{dayNumber:00}/input.txt");
}
