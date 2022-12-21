namespace Aoc.Year2022;

using Xunit;

public class IntegrationTests
{
    [Fact]
    public void TestProgram()
    {
        var entryPoint = typeof(Program).Assembly.EntryPoint!;
        entryPoint.Invoke(null, new object[] { Array.Empty<string>() });
    }
}