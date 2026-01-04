using SharedKernel.Pagination;

namespace MotoLube.Domain.Repositories;

public interface IRepository<in TId, TEntity> 
    where TId : notnull, IEquatable<TId>
    where TEntity : class
{
    Task<TEntity> InsertAsync(TEntity entity);
    ValueTask<TEntity?> FindByIdAsync(params TId[] ids);
    Task<IReadOnlyList<TEntity>> GetPagedAsync(PaginationFilter filter);
    Task<bool> ExistsByIdAsync(TId id);
    Task<int> CountAsync();
    void Update(TEntity entity); 
    void SoftDelete(TEntity entity);
}