using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;
using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Persistence.Repositories;

internal sealed class OutboundRepository(AppDbContext context)
    : Repository<Outbound, Guid>(context), IOutboundRepository
{
    public async Task<IReadOnlyList<Outbound>> GetByDateRangeAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationOptions pagingOptions,
        CancellationToken cancellationToken = default) =>
            await GetByDateRangeAsync(
                startDate,
                endDate,
                o => o,
                pagingOptions,
                cancellationToken);

    public async Task<IReadOnlyList<TResult>> GetByDateRangeAsync<TResult>(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        Expression<Func<Outbound, TResult>> selector,
        PaginationOptions pagingOptions,
        CancellationToken cancellationToken = default) =>
            await Context.Outbounds
                    .AsNoTracking()
                    .Include(o => o.Items)
                    .Where(o => o.OutboundDate >= startDate &&
                                o.OutboundDate <= endDate)
                    .OrderBy(o => o.OutboundDate)
                    .Select(selector)
                    .Skip((pagingOptions.Page - 1) * pagingOptions.Size)
                    .Take(pagingOptions.Size)
                    .ToListAsync(cancellationToken);
}