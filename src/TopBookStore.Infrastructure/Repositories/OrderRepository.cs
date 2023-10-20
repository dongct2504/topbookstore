using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(TopBookStoreContext context) : base(context)
    {
    }

    public void Update(Order order)
    {
        _context.Update(order);
    }
}