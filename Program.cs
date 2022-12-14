Resolve<DayOne, int>(1);
Resolve<DayTwo, int>(2);
Resolve<DayThree, int>(3);
Resolve<DayFour, int>(4);
Resolve<DayFive, string>(5);
Resolve<DaySix, int>(6);

void Resolve<DayType, ReturnType>(int dayNumber) where DayType : IDay<ReturnType>
{
    var lines = ReadInput(dayNumber);

    var partOneResult = DayType.PartOne(lines);
    var partTwoResult = DayType.PartTwo(lines);

    PrintToConsole(dayNumber, partOneResult, partTwoResult);
}

void PrintToConsole<ReturnType>(int dayNumber, ReturnType partOneResult, ReturnType partTwoResult)
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
    return File.ReadAllLines($"./src/day-{dayNumber}/input.txt");
}
