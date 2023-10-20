using TopBookStore.Domain.Entities;

namespace TopBookStore.Domain.Interfaces;

public interface ICartItemRepository : IRepository<CartItem>
{
    void Update(CartItem item);
}