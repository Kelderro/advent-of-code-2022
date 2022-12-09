string[] ReadInput(int dayNumber)
{
    return File.ReadAllLines($"./day-{dayNumber}/input.txt");
}
