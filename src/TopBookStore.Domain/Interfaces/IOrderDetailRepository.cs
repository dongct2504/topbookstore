using TopBookStore.Domain.Entities;

namespace TopBookStore.Domain.Interfaces;

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    void Update(OrderDetail item);
}