using MotoLube.Domain.Entities;

namespace MotoLube.Domain.Repositories;

public interface IOutboundRepository : IRepository<Guid, Outbound>
{
    Task<IReadOnlyList<Outbound>> GetByDateRangeAsync(
        DateTimeOffset startDate, 
        DateTimeOffset endDate,
        CancellationToken cancellationToken = default);
}
