using Microsoft.EntityFrameworkCore;
using MotoLube.Persistence.Common;
using MotoLube.Persistence.Test.Fixtures;
using System.Reflection;
using Xunit.Abstractions;

namespace MotoLube.Persistence.Test.TestCases;

public class DatabaseInitializationTests(DatabaseFixture fixture, ITestOutputHelper outputHelper)
    : TestBase(fixture, outputHelper)
{
    [Fact]
    public void Database_Should_Initialize_Correctly()
    {
        // Arrange & Act done in the fixture
        // Assert
        Assert.True(_context.Database.CanConnect());
    }

    [Fact]
    public async Task Database_Should_Have_Expected_Tables()
    {
        var expectedTables = typeof(TableNames).GetFields(
                BindingFlags.NonPublic | 
                BindingFlags.Static)
            .Where(f => f.IsStatic && f.IsLiteral)
            .Select(f => f.GetRawConstantValue()!.ToString())
            .ToArray();

        var actualTables = _context.Model.GetEntityTypes()
            .Select(t => t.GetTableName())
            .Distinct()
            .ToArray();

        Assert.Equivalent(expectedTables, actualTables);
    }
}
