using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();

    Task<Order?> GetOrderByIdAsync(int id);
    Task<Order?> GetOrderByCustomerIdAsync(int id);

    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task RemoveOrderAsync(Order order);
}
