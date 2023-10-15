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

    public async Task AddCustomerAsync(Customer customer)
    {
        _data.Customers.Add(customer);
        await _data.SaveAsync();
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        _data.Customers.Update(customer);
        await _data.SaveAsync();
    }

    public async Task RemoveCustomerAsync(Customer customer)
    {
        _data.Customers.Remove(customer);
        await _data.SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _data.SaveAsync();
    }
}