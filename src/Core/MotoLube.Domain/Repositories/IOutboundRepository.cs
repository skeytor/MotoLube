using MotoLube.Domain.Entities;
using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Domain.Repositories;

public interface IOutboundRepository : IRepository<Guid, Outbound>
{
    Task<IReadOnlyList<TResult>> GetByDateRangeAsync<TResult>(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        Expression<Func<Outbound, TResult>> selector,
        PaginationOptions pagingOptions,
        CancellationToken cancellationToken = default);
}
