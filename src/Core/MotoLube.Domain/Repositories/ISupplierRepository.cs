using MotoLube.Domain.Entities;

namespace MotoLube.Domain.Repositories;

public interface ISupplierRepository : IRepository<int, Supplier>
{
    Task<Supplier?> FindByNameAsync(string name);
    Task<bool> IsNameUniqueAsync(string name);
}
