var dayInput = ReadInput(1);
Console.WriteLine(DayOne.PartOne(dayInput));
Console.WriteLine(DayOne.PartTwo(dayInput));

string[] ReadInput(int dayNumber)
{
    return File.ReadAllLines($"./day-{dayNumber}/input.txt");
}
