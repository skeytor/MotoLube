using MotoLube.Domain.Entities;

namespace MotoLube.Domain.Repositories;

public interface IProductRepository : IRepository<Guid, Product>
{
    Task<Product?> FindBySkuAsync(string sku);
    Task<bool> IsSkuUniqueAsync(string sku);
}
