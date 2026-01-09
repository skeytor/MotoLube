using MotoLube.Domain.Entities;

namespace MotoLube.Domain.Repositories;

public interface ICustomerRepository : IRepository<Guid, Customer>
{
    Task<Customer?> FindByNameAsync(string name);
    Task<bool> IsEmailUniqueAsync(string email);
}
