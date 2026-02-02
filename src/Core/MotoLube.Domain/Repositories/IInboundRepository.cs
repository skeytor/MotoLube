using MotoLube.Domain.Entities;
using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Domain.Repositories;

/// <summary>
/// Defines a repository interface for retrieving inbound entities.
/// </summary>
/// <remarks>This interface extends the generic repository pattern for inbound entities, providing asynchronous
/// methods.
/// </remarks>
public interface IInboundRepository : IRepository<Inbound, Guid>
{
    /// <summary>
    /// Asynchronously retrieves a read-only list of inbound records that fall within the specified date range.
    /// </summary>
    /// <param name="startDate">The start of the date range. Only inbound records with a date greater than or equal to this value are included.</param>
    /// <param name="endDate">The end of the date range. Only inbound records with a date less than or equal to this value are included.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of inbound records
    /// within the specified date range. The list is empty if no records are found.</returns>
    Task<IReadOnlyList<Inbound>> GetByDateRangeAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationOptions paginationOptions,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a paginated list of inbound records within the specified date range, projecting each
    /// record to a result of type TResult.
    /// </summary>
    /// <param name="startDate">The start of the date range to filter inbound records. Only records with a date greater than or equal to this
    /// value are included.</param>
    /// <param name="endDate">The end of the date range to filter inbound records. Only records with a date less than or equal to this value
    /// are included.</param>
    /// <param name="paginationOptions">The pagination settings that control the number of records returned and the page to retrieve. Must not be null.</param>
    /// <param name="selector">An expression that defines how to project each inbound record to the TResult type. Cannot be null.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is None.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of projected
    /// results of type TResult that match the specified date range and pagination options. The list is empty if no
    /// records are found.</returns>
    Task<IReadOnlyList<TResult>> GetByDateRangeAsync<TResult>(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationOptions paginationOptions,
        Expression<Func<Inbound, TResult>> selector,
        CancellationToken cancellationToken = default);
}