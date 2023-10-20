using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Infrastructure.Repositories;

public class CartItemRepository : Repository<CartItem>, ICartItemRepository
{
    public CartItemRepository(TopBookStoreContext context) : base(context)
    {
    }

    public void Update(CartItem item)
    {
        _context.Update(item);
    }
}