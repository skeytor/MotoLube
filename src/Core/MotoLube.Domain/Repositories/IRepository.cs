using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Domain.Repositories;

/// <summary>
/// Defines a generic contract for a repository that provides asynchronous and synchronous operations for managing
/// entities by their identifiers.
/// </summary>
/// <remarks>This interface abstracts common data access operations such as insertion, retrieval, update, and
/// existence checks for entities. It is intended to be implemented by classes that interact with a data store,
/// providing a consistent API for working with entities regardless of the underlying storage mechanism. Implementations
/// are expected to handle thread safety and data consistency according to their specific requirements.</remarks>
/// <typeparam name="TId">The type of the unique identifier for entities managed by the repository. Must be non-null and support equality
/// comparison.</typeparam>
/// <typeparam name="TEntity">The type of the entity managed by the repository. Must be a reference type.</typeparam>
public interface IRepository<in TId, TEntity> 
    where TId : notnull, IEquatable<TId>
    where TEntity : class
{
    Task<TEntity> InsertAsync(TEntity entity);
    ValueTask<TEntity?> FindByIdAsync(TId id);

    /// <summary>
    /// Asynchronously retrieves a paged list of entities projected to the specified result type.
    /// </summary>
    /// <typeparam name="TResult">The type to which each entity is projected in the result set.</typeparam>
    /// <param name="pagingOptions">The pagination options that specify the page size, page number, and sorting criteria for the query.</param>
    /// <param name="filters">The filters to apply when querying entities. Only entities matching these filters are included in the result.</param>
    /// <param name="selector">An expression that defines how to project each entity to the result type.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of projected
    /// results for the requested page. The list is empty if no entities match the filters.</returns>
    Task<IReadOnlyList<TResult>> GetPagedAsync<TResult>(
        PaginationOptions pagingOptions,
        PaginationQueryFilters filters,
        Expression<Func<TEntity, TResult>> selector);
    
    /// <summary>
    /// Asynchronously retrieves a paged subset of entities projected to a specified result type.
    /// </summary>
    /// <typeparam name="TResult">The type to which each entity is projected in the result set.</typeparam>
    /// <param name="pagingOptions">The pagination options that specify the page size, page number, and related settings for the query. Cannot be
    /// null.</param>
    /// <param name="selector">An expression that defines how to project each entity to the result type. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of projected
    /// results for the specified page. The list will be empty if no entities match the paging criteria.</returns>
    Task<IReadOnlyList<TResult>> GetPagedAsync<TResult>(
        PaginationOptions pagingOptions,
        Expression<Func<TEntity, TResult>> selector);
    Task<bool> ExistsByIdAsync(TId id);
    Task<int> CountAsync();
    void Update(TEntity entity);
}