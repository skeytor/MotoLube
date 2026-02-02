using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;

namespace MotoLube.Persistence.Repositories;

internal sealed class CustomerRepository(AppDbContext context) 
    : Repository<Customer, Guid>(context), ICustomerRepository
{
    public Task<Customer?> FindByPhoneNumber(string phoneNumber) => 
        Context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);

    public async Task<bool> IsPhoneNumberUnique(string phoneNumber) => 
        !await Context.Customers.AnyAsync(c => c.PhoneNumber == phoneNumber);
}