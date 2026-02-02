using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;
using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Persistence.Repositories;

internal sealed class InboundRepository(AppDbContext context)
    : Repository<Inbound, Guid>(context), IInboundRepository
{
    public async Task<IReadOnlyList<Inbound>> GetByDateRangeAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationOptions paginationOptions,
        CancellationToken cancellationToken = default) =>
            await GetByDateRangeAsync(
                startDate,
                endDate,
                paginationOptions,
                i => i,
                cancellationToken);

    public async Task<IReadOnlyList<TResult>> GetByDateRangeAsync<TResult>(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationOptions paginationOptions,
        Expression<Func<Inbound, TResult>> selector,
        CancellationToken cancellationToken = default) =>
            await Context.Inbounds
                .AsNoTracking()
                .Include(i => i.Items)
                .Where(i => i.InboundDate >= startDate &&
                            i.InboundDate <= endDate)
                .OrderBy(i => i.InboundDate)
                .Select(selector)
                .Skip((paginationOptions.Page - 1) * paginationOptions.Size)
                .Take(paginationOptions.Size)
                .ToListAsync(cancellationToken);
}
