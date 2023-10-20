using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Infrastructure.Repositories;

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(TopBookStoreContext context) : base(context)
    {
    }

    public void Update(OrderDetail orderDetail)
    {
        _context.Update(orderDetail);
    }
}