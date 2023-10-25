using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface ICartItemService
{
    Task<CartItem?> GetCartItemByIdAsync(int id);
    Task<CartItem?> GetCartItemByCartIdAsync(int cartId);

    Task<int?> GetQuantityAsync(int cartId);

    Task AddCartItemAsync(CartItem cartItem);
    Task UpdateCartItemAsync(CartItem cartItem);
    Task RemoveCartItemAsync(CartItem cartItem);
}