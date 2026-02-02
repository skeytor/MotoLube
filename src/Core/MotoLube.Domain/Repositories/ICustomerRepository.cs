using MotoLube.Domain.Entities;

namespace MotoLube.Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer, Guid>
{
    Task<Customer?> FindByPhoneNumber(string name);
    Task<bool> IsPhoneNumberUnique(string email);
}