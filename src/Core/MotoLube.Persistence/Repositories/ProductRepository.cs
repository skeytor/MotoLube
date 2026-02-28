using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;
using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Persistence.Repositories;

internal sealed class ProductRepository(AppDbContext context) : Repository<Product, Guid>(context), IProductRepository
{
    private static readonly Dictionary<string, Expression<Func<Product, object>>> _sortableColumns =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "name", p => p.Name },
            { "sku", p => p.Sku },
            { "price", p => p.Price },
        };

    public Task<Product?> FindBySkuAsync(string sku) =>
        Context.Products.FirstOrDefaultAsync(p => p.Sku == sku);

    public async Task<bool> IsSkuUniqueAsync(string sku) =>
        !await Context.Products.AnyAsync(p => p.Sku == sku);

    public async Task<bool> IsSkuUniqueAsync(string sku, Guid excludeId) => 
        !await Context.Products.AnyAsync(p => p.Sku == sku && p.Id != excludeId);

    public async Task<IReadOnlyCollection<TResult>> GetPagedListAsync<TResult>(
        int page,
        int size,
        PagingFilters pagingFilters,
        Expression<Func<Product, TResult>> selector,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query = Context.Products.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(pagingFilters.SearchTerm))
        {
            query = query.Where(p =>
                p.Name.Contains(pagingFilters.SearchTerm) ||
                p.Sku.Contains(pagingFilters.SearchTerm));
        }

        Expression<Func<Product, object>> keySelector = _sortableColumns.GetValueOrDefault(
            pagingFilters.SortColumn!,
            defaultValue: p => p.Name);

        query = pagingFilters.SortOrder is SortOrder.Desc
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector);

        return await query
            .Select(selector)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }
}