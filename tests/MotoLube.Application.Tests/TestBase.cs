namespace MotoLube.Application.Tests;

public abstract class TestBase(ITestOutputHelper outputHelper)
{
    protected ITestOutputHelper OutputHelper { get; } = outputHelper;
}