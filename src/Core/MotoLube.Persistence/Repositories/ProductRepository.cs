using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;
using SharedKernel.Pagination;
using System.Linq.Expressions;

namespace MotoLube.Persistence.Repositories;

internal sealed class ProductRepository(AppDbContext context)
    : Repository<Guid, Product>(context), IProductRepository
{
    private static readonly Dictionary<string, Expression<Func<Product, object>>> _sortableColumns =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "name", p => p.Name },
            { "sku", p => p.Sku },
            { "price", p => p.Price },
        };

    public Task<Product?> FindBySkuAsync(string sku) =>
        Context.Products
                .FirstOrDefaultAsync(p => p.Sku == sku);

    public async Task<bool> IsSkuUniqueAsync(string sku) =>
        !await Context.Products
                      .AnyAsync(p => p.Sku == sku);

    public override async Task<IReadOnlyList<TResult>> GetPagedAsync<TResult>(
        PaginationOptions pagingOptions,
        PaginationQueryFilters filters,
        Expression<Func<Product, TResult>> selector)
    {
        IQueryable<Product> query = Context.Products.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filters.SearchTerm))
        {
            query = query.Where(p =>
                p.Name.Contains(filters.SearchTerm) ||
                p.Sku.Contains(filters.SearchTerm));
        }

        Expression<Func<Product, object>> keySelector = _sortableColumns.GetValueOrDefault(
            filters.SortColumn ?? string.Empty,
            defaultValue: p => p.Name);

        query = filters.SortOrder is SortOrder.Descending
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector);

        return await query
            .Select(selector)
            .Skip((pagingOptions.Page - 1) * pagingOptions.Size)
            .Take(pagingOptions.Size)
            .ToListAsync();
    }
}