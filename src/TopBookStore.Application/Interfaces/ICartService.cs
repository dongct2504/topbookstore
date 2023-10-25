using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface ICartService
{
    Task<IEnumerable<Cart>> GetAllCartAsync();

    Task<Cart?> GetCartByCustomerAsync(int customerId);

    Task AddCartItemAsync(int customerId, CartItem cartItem);

    Task<int?> GetQuantityAsync(int customerId);
    Task<int?> GetTotalCartItemsCountAsync(int customerId);

    Task AddCartAsync(Cart cart);
    Task UpdateCartAsync(Cart cart);
    Task RemoveCartAsync(Cart cart);
}