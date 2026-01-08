using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;

namespace MotoLube.Persistence.Repositories;

internal sealed class ProductRepository(AppDbContext context) 
    : Repository<Guid, Product>(context), IProductRepository
{
    public Task<Product?> FindBySkuAsync(string sku) => 
        Context.Products
        .       FirstOrDefaultAsync(p => p.Sku == sku);

    public async Task<bool> IsSkuUniqueAsync(string sku) =>
        !await Context.Products
                      .AnyAsync(p => p.Sku == sku);
}
