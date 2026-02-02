using MotoLube.Domain.Entities;

namespace MotoLube.Domain.Repositories;

public interface IProductRepository : IRepository<Product, Guid>
{
    Task<Product?> FindBySkuAsync(string sku);
    Task<bool> IsSkuUniqueAsync(string sku);
}