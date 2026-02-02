using MotoLube.Domain.Entities;
using MotoLube.Persistence.Repositories;
using MotoLube.Persistence.Test.DataInitializers;
using MotoLube.Persistence.Test.Fixtures;
using Xunit.Abstractions;

namespace MotoLube.Persistence.Test.TestCases.Repositories;

public class CustomerRepositoryTests(DatabaseFixture fixture, ITestOutputHelper outputHelper)
    : TestBase(fixture, outputHelper)
{
    private readonly CustomerRepository _repository = new(fixture.Context);

    [Fact]
    public async Task FindByPhoneNumber_Should_Return_Customer_When_PhoneExists()
    {
        Customer customer = SampleData.Customers[0];

        var result = await _repository.FindByPhoneNumber(customer.PhoneNumber!);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customer.Id, result.Id);
        Assert.Equal(customer.PhoneNumber, result.PhoneNumber);
    }

    [Theory]
    [InlineData("+1-123-456-789")]
    [InlineData("+44-20-7946-0958")]
    public async Task FindByPhoneNumber_Should_Return_Null_When_PhoneDoesNotExist(string phoneNumber)
    {
        // Act
        var result = await _repository.FindByPhoneNumber(phoneNumber);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task IsPhoneNumberUnique_Should_Return_False_When_PhoneExists()
    {
        string existingPhone = SampleData.Customers[1].PhoneNumber!;

        var result = await _repository.IsPhoneNumberUnique(existingPhone);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("+59174553220")]
    [InlineData("+59174553221")]
    public async Task IsPhoneNumberUnique_Should_Return_True_When_PhoneDoesNotExist(string phone)
    {
        // Act
        var result = await _repository.IsPhoneNumberUnique(phone);

        // Assert
        Assert.True(result);
    }
}