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
        PaginationOptions pagingOptions,
        PaginationQueryFilters filters,
        Expression<Func<TEntity, TResult>> selector) => 
            await _dbSet
                .AsNoTracking()
                .Select(selector)
                .Skip((pagingOptions.Page - 1) * pagingOptions.Size)
                .Take(pagingOptions.Size)
                .ToListAsync();

    public virtual async Task<IReadOnlyList<TResult>> GetPagedAsync<TResult>(
        PaginationOptions pagingOptions,
        Expression<Func<TEntity, TResult>> selector) =>
            await _dbSet
                .AsNoTracking()
                .Select(selector)
                .Skip((pagingOptions.Page - 1) * pagingOptions.Size)
                .Take(pagingOptions.Size)
                .ToListAsync();

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public void Update(TEntity entity) => _dbSet.Update(entity);
}