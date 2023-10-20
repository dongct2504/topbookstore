using TopBookStore.Domain.Entities;

namespace TopBookStore.Domain.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    void Update(Order order);
}