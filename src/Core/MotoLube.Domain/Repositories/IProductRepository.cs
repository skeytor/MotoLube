using MotoLube.Domain.Entities;
using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Domain.Repositories;

public interface IProductRepository : IRepository<Product, Guid>
{
    Task<Product?> FindBySkuAsync(string sku);

    Task<bool> IsSkuUniqueAsync(string sku);
    
    Task<bool> IsSkuUniqueAsync(string sku, Guid excludeId);
    
    /// <summary>
    /// Asynchronously retrieves a paged list of entities projected to the specified result type.
    /// </summary>
    /// <typeparam name="TResult">The type to which each entity is projected in the result set.</typeparam>
    /// <param name="pagingParams">The pagination options that specify the page size, page number, and sorting criteria for the query.</param>
    /// <param name="pagingFilters">The filters to apply when querying entities. Only entities matching these filters are included in the result.</param>
    /// <param name="selector">An expression that defines how to project each entity to the result type.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of projected
    /// results for the requested page. The list is empty if no entities match the filters.</returns>
    Task<IReadOnlyCollection<TResult>> GetPagedListAsync<TResult>(
        int page,
        int size,
        PagingFilters pagingFilters,
        Expression<Func<Product, TResult>> selector,
        CancellationToken cancellationToken = default);
}