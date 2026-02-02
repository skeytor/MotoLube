using MotoLube.Domain.Entities;
using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Domain.Repositories;

public interface IOutboundRepository : IRepository<Outbound, Guid>
{
    Task<IReadOnlyList<Outbound>> GetByDateRangeAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationOptions pagingOptions,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TResult>> GetByDateRangeAsync<TResult>(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        Expression<Func<Outbound, TResult>> selector,
        PaginationOptions pagingOptions,
        CancellationToken cancellationToken = default);
}