using MotoLube.Domain.Entities;

namespace MotoLube.Domain.Repositories;

public interface IInboundRepository : IRepository<Guid, Inbound>
{
    Task<IReadOnlyList<Inbound>> GetByDateRangeAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        CancellationToken cancellationToken = default);
}
