using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;
using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Persistence.Repositories;

internal sealed class OutboundRepository(AppDbContext context)
    : Repository<Guid, Outbound>(context), IOutboundRepository
{

    public async Task<IReadOnlyList<TResult>> GetByDateRangeAsync<TResult>(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        Expression<Func<Outbound, TResult>> selector,
        PaginationOptions pagingOptions,
        CancellationToken cancellationToken = default) =>
            await Context.Outbounds
                         .AsNoTracking()
                         .Where(o => o.OutboundDate >= startDate &&
                                     o.OutboundDate <= endDate)
                         .OrderBy(o => o.OutboundDate)
                         .Select(selector)
                         .Skip((pagingOptions.Page - 1) * pagingOptions.Size)
                         .Take(pagingOptions.Size)
                         .ToListAsync(cancellationToken);
}