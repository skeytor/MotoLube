using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Domain.Repositories;

public interface IRepository<in TId, TEntity> 
    where TId : notnull, IEquatable<TId>
    where TEntity : class
{
    Task<TEntity> InsertAsync(TEntity entity);
    ValueTask<TEntity?> FindByIdAsync(TId id);
    Task<IReadOnlyList<TResult>> GetPagedAsync<TResult>(
        PaginationOptions pagingOptions,
        PaginationQueryFilters filters,
        Expression<Func<TEntity, TResult>> selector);
    Task<IReadOnlyList<TResult>> GetPagedAsync<TResult>(
        PaginationOptions pagingOptions,
        Expression<Func<TEntity, TResult>> selector);
    Task<bool> ExistsByIdAsync(TId id);
    Task<int> CountAsync();
    void Update(TEntity entity);
}