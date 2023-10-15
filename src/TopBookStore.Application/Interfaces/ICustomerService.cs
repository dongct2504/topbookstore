using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomers();
    Task<Customer?> GetCustomerById(int id);
    Task<Customer?> GetCustomerByEmail(string email);

    Task AddCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    Task RemoveCustomerAsync(Customer customer);
    Task SaveAsync();
}