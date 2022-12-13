using Xunit;

public class ProgramTests
{
    [Fact]
    public void ProgramT()
    {
        var entryPoint = typeof(Program).Assembly.EntryPoint!;
        entryPoint.Invoke(null, new object[] { Array.Empty<string>() });
    }
}