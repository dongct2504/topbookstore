using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;

namespace TopBookStore.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ITopBookStoreUnitOfWork _data;

    public CustomerService(ITopBookStoreUnitOfWork data)
    {
        _data = data;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await _data.Customers.ListAllAsync(new QueryOptions<Customer>());
    }

    public Task<Customer?> GetCustomerById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetCustomerByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task AddCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}