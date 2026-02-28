using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;
using System.Linq.Expressions;

namespace MotoLube.Persistence.Repositories;

internal class Repository<TEntity, TId>(AppDbContext context) : IRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : IEquatable<TId>
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    protected AppDbContext Context => context;

    public virtual Task<int> CountAsync() => _dbSet.CountAsync();

    public void Delete(TEntity entity) => _dbSet.Remove(entity);

    public virtual ValueTask<TEntity?> FindByIdAsync(TId id) => _dbSet.FindAsync([id]);

    public async Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(
        int page,
        int size,
        CancellationToken cancellationToken = default) => 
        await GetPagedListAsync(page, size, e => e, cancellationToken);

    public async Task<IReadOnlyCollection<TResult>> GetPagedListAsync<TResult>(
        int page,
        int size,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default) => 
        await _dbSet
            .AsNoTracking()
            .Select(selector)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }
}