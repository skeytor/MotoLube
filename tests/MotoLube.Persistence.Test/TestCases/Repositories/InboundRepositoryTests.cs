using MotoLube.Domain.Entities;
using MotoLube.Persistence.Repositories;
using MotoLube.Persistence.Test.Fixtures;
using SharedKernel.Pagination;
using Xunit.Abstractions;

namespace MotoLube.Persistence.Test.TestCases.Repositories;

public class InboundRepositoryTests(DatabaseFixture fixture, ITestOutputHelper outputHelper) 
    : TestBase(fixture, outputHelper)
{
    private readonly InboundRepository _repository = new(fixture.Context);

    [Fact]
    public async Task GetByDateRangeAsync_Should_Return_InboundsWithinDateRange()
    {
        // Arrange
        DateTimeOffset startDate = DateTimeOffset.UtcNow.AddDays(-10);
        DateTimeOffset endDate = DateTimeOffset.UtcNow;
        PaginationOptions paginationOptions = new();

        // Act
        IReadOnlyList<Inbound> result = await _repository.GetByDateRangeAsync(startDate, endDate, paginationOptions);

        // Assert
        Assert.NotNull(result);
        Assert.All(result, inbound =>
        {
            Assert.InRange(inbound.InboundDate, startDate, endDate);
        });
    }

    [Fact]
    public async Task GetByDateRangeAsync_Should_Return_EmptyList_WhenNoInboundsFound()
    {
        // Arrange
        DateTimeOffset startDate = DateTimeOffset.UtcNow.AddYears(-5);
        DateTimeOffset endDate = DateTimeOffset.UtcNow.AddYears(-4);
        PaginationOptions paginationOptions = new();

        // Act
        IReadOnlyList<Inbound> result = await _repository.GetByDateRangeAsync(startDate, endDate, paginationOptions);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByDateRangeAsync_WithPagination_Should_Return_PaginatedResults()
    {
        // Arrange
        DateTimeOffset startDate = DateTimeOffset.UtcNow.AddDays(-50);
        DateTimeOffset endDate = DateTimeOffset.UtcNow;
        PaginationOptions paginationOptions = new(Page: 1, Size: 2);
        // Act
        var result = await _repository.GetByDateRangeAsync(
            startDate,
            endDate,
            paginationOptions,
            selector: x => new
            {
                x.Id,
                x.InboundDate,
                x.Items
            });

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count <= paginationOptions.Size);
    }
}
