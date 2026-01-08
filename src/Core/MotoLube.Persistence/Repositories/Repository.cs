using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Repositories;
using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Persistence.Repositories;

internal abstract class Repository<TId, TEntity>(AppDbContext context) : IRepository<TId, TEntity>
    where TEntity : class
    where TId : notnull, IEquatable<TId>
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    protected AppDbContext Context { get; } = context;

    public virtual Task<int> CountAsync() => _dbSet.CountAsync();

    public virtual Task<bool> ExistsByIdAsync(TId id) => _dbSet.AnyAsync(e => EF.Property<TId>(e, "Id").Equals(id));

    public virtual ValueTask<TEntity?> FindByIdAsync(TId id) => _dbSet.FindAsync(id);

    public virtual async Task<IReadOnlyList<TResult>> GetPagedAsync<TResult>(
        PaginationFilter filter,
        Expression<Func<TEntity, TResult>> selector) =>
        await _dbSet
            .AsNoTracking()
            .Select(selector)
            .Skip((filter.Page - 1) * filter.Size)
            .Take(filter.Size)
            .ToListAsync();

    public virtual async Task<IReadOnlyList<TEntity>> GetPagedAsync(PaginationFilter filter) =>
        await _dbSet
            .AsNoTracking()
            .Skip((filter.Page - 1) * filter.Size)
            .Take(filter.Size)
            .ToListAsync();

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public void Update(TEntity entity) => _dbSet.Update(entity);
}