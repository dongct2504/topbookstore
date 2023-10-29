using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;

namespace TopBookStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly ITopBookStoreUnitOfWork _data;

    public OrderService(ITopBookStoreUnitOfWork data)
    {
        _data = data;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _data.Orders.ListAllAsync(new QueryOptions<Order>());
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        QueryOptions<Order> options = new()
        {
            Includes = "OrderDetails.Book",
            Where = c => c.OrderId == id
        };

        return await _data.Orders.GetAsync(options);
    }

    public async Task<Order?> GetOrderByCustomerIdAsync(int id)
    {
        QueryOptions<Order> options = new()
        {
            Includes = "OrderDetails.Book",
            Where = c => c.CustomerId == id
        };

        return await _data.Orders.GetAsync(options);
    }

    public async Task AddOrderAsync(Order order)
    {
        _data.Orders.Add(order);
        await _data.SaveAsync();
    }

    public async Task UpdateOrderAsync(Order category)
    {
        _data.Orders.Update(category);
        await _data.SaveAsync();
    }

    public async Task RemoveOrderAsync(Order category)
    {
        _data.Orders.Remove(category);
        await _data.SaveAsync();
    }
}
