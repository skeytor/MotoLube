using MotoLube.Domain.Entities;
using MotoLube.Persistence.Repositories;
using MotoLube.Persistence.Test.DataInitializers;
using MotoLube.Persistence.Test.Fixtures;
using Xunit.Abstractions;

namespace MotoLube.Persistence.Test.TestCases.Repositories;

public class SupplierRepositoryTests(DatabaseFixture database, ITestOutputHelper outputHelper)
    : TestBase(database, outputHelper)
{
    private readonly SupplierRepository _repository = new(database.Context);

    [Fact]
    public async Task FindByNameAsync_Should_Return_Supplier_When_NameExists()
    {
        // Arrange
        Supplier existing = SampleData.Suppliers[0];

        // Act
        var result = await _repository.FindByNameAsync(existing.Name);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existing.Id, result.Id);
        Assert.Equal(existing.Name, result.Name);
    }

    [Theory]
    [InlineData("Non Existent Supplier")]
    [InlineData("Unknown Supplier Ltd.")]
    public async Task FindByNameAsync_Should_Return_Null_When_NameDoesNotExist(string name)
    {
        // Act
        var result = await _repository.FindByNameAsync(name);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task IsNameUniqueAsync_Should_Return_False_When_NameAlreadyExists()
    {
        // Arrange
        string existingName = SampleData.Suppliers[1].Name;

        // Act
        var result = await _repository.IsNameUniqueAsync(existingName);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("Brand New Supplier")]
    [InlineData("Local Parts Co.")]
    public async Task IsNameUniqueAsync_Should_Return_True_When_NameDoesNotExist(string name)
    {
        // Act
        var result = await _repository.IsNameUniqueAsync(name);

        // Assert
        Assert.True(result);
    }
}
