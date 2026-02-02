using Microsoft.EntityFrameworkCore;
using MotoLube.Persistence.Common;
using MotoLube.Persistence.Test.DataInitializers;
using MotoLube.Persistence.Test.Fixtures;
using Xunit.Abstractions;

namespace MotoLube.Persistence.Test.TestCases;

public class DatabaseInitializationTests(DatabaseFixture database, ITestOutputHelper outputHelper)
    : TestBase(database, outputHelper)
{
    #region Database Connection Tests

    [Fact]
    public void Database_Should_Initialize_Correctly_When_ConnectionIsSuccessful()
    {
        // Arrange & Act done in the fixture
        // Assert
        Assert.True(Context.Database.CanConnect());
    }

    [Fact]
    public void DatabaseProvider_Should_Return_Npgsql_When_ContextIsInitialized()
    {
        // Arrange & Act
        string? providerName = Context.Database.ProviderName;

        // Assert
        Assert.NotNull(providerName);
        Assert.Contains("Npgsql", providerName);
    }

    #endregion

    #region Table Structure Tests

    [Theory]
    [InlineData(TableNames.Products)]
    [InlineData(TableNames.Categories)]
    [InlineData(TableNames.Brands)]
    [InlineData(TableNames.Suppliers)]
    [InlineData(TableNames.Customers)]
    [InlineData(TableNames.Inbounds)]
    [InlineData(TableNames.InboundItems)]
    [InlineData(TableNames.Outbounds)]
    [InlineData(TableNames.OutboundItems)]
    public void Database_Should_Return_True_When_TableExists(string tableName)
    {
        // Arrange & Act
        var tableExists = Context.Model.GetEntityTypes()
            .Any(t => t.GetTableName() == tableName);

        // Assert
        Assert.True(tableExists, $"Table '{tableName}' should exist in the database schema.");
    }

    #endregion

    #region Seed Data Tests

    [Fact]
    public async Task SeedData_Should_Return_Products_When_DatabaseIsSeeded()
    {
        // Arrange
        int expectedCount = SampleData.Products.Length;

        // Act
        int actualCount = await Context.Products.CountAsync();

        // Assert
        Assert.Equal(expectedCount, actualCount);
    }

    #endregion

    #region DbSet Availability Tests

    [Fact]
    public void DbSets_Should_Return_NotNull_When_ContextIsInitialized()
    {
        // Assert
        Assert.NotNull(Context.Products);
        Assert.NotNull(Context.Categories);
        Assert.NotNull(Context.Brands);
        Assert.NotNull(Context.Suppliers);
        Assert.NotNull(Context.Customers);
        Assert.NotNull(Context.Inbounds);
        Assert.NotNull(Context.InboundItems);
        Assert.NotNull(Context.Outbounds);
        Assert.NotNull(Context.OutboundItems);
    }

    #endregion
}
