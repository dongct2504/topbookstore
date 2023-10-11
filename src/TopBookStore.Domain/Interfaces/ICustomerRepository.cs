using TopBookStore.Domain.Entities;

namespace TopBookStore.Domain.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    void Update(Customer customer);
}