using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Domain.Repositories;

public interface IRepository<in TId, TEntity> 
    where TId : notnull, IEquatable<TId>
    where TEntity : class
{
    Task<TEntity> InsertAsync(TEntity entity);
    ValueTask<TEntity?> FindByIdAsync(params TId[] ids);
    Task<IReadOnlyList<TEntity>> GetPagedAsync(PaginationFilter filter);
    Task<IReadOnlyList<TResult>> GetPagedAsync<TResult>(
        PaginationFilter filter,
        Expression<Func<TEntity, TResult>> selector);
    Task<bool> ExistsByIdAsync(TId id);
    Task<int> CountAsync();
    void Update(TEntity entity);
}