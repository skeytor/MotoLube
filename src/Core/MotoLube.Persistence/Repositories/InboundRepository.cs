using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;
using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Persistence.Repositories;

internal sealed class InboundRepository(AppDbContext context) 
    : Repository<Guid, Inbound>(context), IInboundRepository
{
    public Task<IReadOnlyList<Inbound>> GetByDateRangeAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        CancellationToken cancellationToken = default) =>
            GetByDateRangeAsync(startDate, endDate, new PaginationOptions(), i => i, cancellationToken);

    public async Task<IReadOnlyList<TResult>> GetByDateRangeAsync<TResult>(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationOptions paginationOptions,
        Expression<Func<Inbound, TResult>> selector,
        CancellationToken cancellationToken = default) =>
            await Context.Inbounds
                .AsNoTracking()
                .Where(i => i.InboundDate >= startDate &&
                            i.InboundDate <= endDate)
                .OrderBy(i => i.InboundDate)
                .Select(selector)
                .Skip((paginationOptions.Page - 1) * paginationOptions.Size)
                .Take(paginationOptions.Size)
                .ToListAsync(cancellationToken);
}
