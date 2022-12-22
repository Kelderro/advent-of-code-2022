namespace Aoc.Year2022;

using FluentAssertions;
using Xunit;

public class IntegrationTests
{
    [Fact]
    public void TestProgram()
    {
        Action act = () =>
        {
            var entryPoint = typeof(Program).Assembly.EntryPoint!;
            entryPoint.Invoke(null, new object[] { Array.Empty<string>() });
        };

        act.Should().NotThrow();
    }
}