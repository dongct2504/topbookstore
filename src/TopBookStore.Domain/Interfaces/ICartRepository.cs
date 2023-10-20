using TopBookStore.Domain.Entities;

namespace TopBookStore.Domain.Interfaces;

public interface ICartRepository : IRepository<Cart>
{
    void Update(Cart cart);
}