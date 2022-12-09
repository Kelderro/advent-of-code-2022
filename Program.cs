Resolve<DayOne>(1);

void Resolve<T>(int dayNumber) where T : IDay
{
    var lines = ReadInput(dayNumber);

    var partOneResult = T.PartOne(lines);
    var partTwoResult = T.PartTwo(lines);

    PrintToConsole(dayNumber, partOneResult, partTwoResult);
}

void PrintToConsole(int dayNumber, int partOneResult, int partTwoResult)
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
    return File.ReadAllLines($"./day-{dayNumber}/input.txt");
}
