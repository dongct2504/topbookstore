using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Infrastructure.Repositories;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(TopBookStoreContext context) : base(context)
    {
    }

    public void Update(Cart cart)
    {
        _context.Update(cart);
    }
}