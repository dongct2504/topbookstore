using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;

namespace TopBookStore.Application.Services;

public class CartItemService : ICartItemService
{
    private readonly ITopBookStoreUnitOfWork _data;

    public CartItemService(ITopBookStoreUnitOfWork data)
    {
        _data = data;
    }

    public async Task<CartItem?> GetCartItemByIdAsync(int id)
    {
        QueryOptions<CartItem> options = new()
        {
            Where = ci => ci.CartItemId == id
        };

        return await _data.CartItems.GetAsync(options);
    }

    public async Task<CartItem?> GetCartItemByCartIdAsync(int cartId)
    {
        QueryOptions<CartItem> options = new()
        {
            Where = ci => ci.CartId == cartId
        };

        return await _data.CartItems.GetAsync(options);
    }

    public async Task<int?> GetQuantityAsync(int cartId)
    {
        IEnumerable<CartItem> cartItems = await _data.CartItems.ListAllAsync(new QueryOptions<CartItem>
        {
            Where = ci => ci.CartId == cartId
        });

        return cartItems.Count();
    }

    public async Task AddCartItemAsync(CartItem cartItem)
    {
        _data.CartItems.Add(cartItem);
        await _data.SaveAsync();
    }

    public async Task UpdateCartItemAsync(CartItem cartItem)
    {
        _data.CartItems.Update(cartItem);
        await _data.SaveAsync();
    }

    public async Task RemoveCartItemAsync(CartItem cartItem)
    {
        _data.CartItems.Remove(cartItem);
        await _data.SaveAsync();
    }
}