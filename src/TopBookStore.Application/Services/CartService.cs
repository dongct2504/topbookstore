using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;

namespace TopBookStore.Application.Services;

public class CartService : ICartService
{
    private readonly ITopBookStoreUnitOfWork _data;

    public CartService(ITopBookStoreUnitOfWork data)
    {
        _data = data;
    }

    public Task<IEnumerable<Cart>> GetAllCartAsync()
    {
        return _data.Carts.ListAllAsync(new QueryOptions<Cart>());
    }

    public async Task AddCartItemAsync(int customerId, CartItem cartItem)
    {
        Customer? customer = await _data.Customers.GetAsync(customerId) ??
            throw new Exception("Customer not found.");

        // Get customer's cart
        // Cart? cart = customer.Carts.FirstOrDefault(c => c.CustomerId == customerId);
        Cart? cart = await _data.Carts.GetAsync(new QueryOptions<Cart>
        {
            Where = c => c.CustomerId == customerId
        });

        // If the customer doesn't have a cart, create a new one and associate it with the customer
        if (cart is null)
        {
            cart = new Cart()
            {
                CustomerId = customerId
            };
            // _data.Carts.Add(cart);
            // customer.Carts.Add(cart); // no need, it automatically add it
            await AddCartAsync(cart); // need to add and save to update CartId
        }

        // Check if the book is already in the cart
        CartItem? cartItemFromDb = await _data.CartItems.GetAsync(new QueryOptions<CartItem>
        {
            Includes = "Book",
            Where = ci => ci.BookId == cartItem.BookId
        });

        if (cartItemFromDb is null)
        {
            cartItem.CartId = cart.CartId;
            _data.CartItems.Add(cartItem);
            // cart.CartItems.Add(cartItem); // also no need
        }
        else
        {
            cartItemFromDb.Quantity += cartItem.Quantity;
        }

        await _data.SaveAsync();
    }

    public async Task<int?> GetQuantityAsync(int customerId)
    {
        Cart cart = await _data.Carts.GetAsync(new QueryOptions<Cart>
        {
            Where = c => c.CustomerId == customerId
        }) ?? new Cart();

        IEnumerable<CartItem> cartItems =  await _data.CartItems.ListAllAsync(new QueryOptions<CartItem>
        {
            Where = ci => ci.CartId == cart.CartId
        });

        return cartItems.Count();
    }

    public async Task<int?> GetTotalCartItemsCountAsync(int customerId)
    {
        IEnumerable<Cart> carts = await _data.Carts.ListAllAsync(new QueryOptions<Cart>
        {
            Where = c => c.CustomerId == customerId
        });

        int count = 0;
        foreach (Cart cart in carts)
        {
            count += cart.CartItems.Count;
        }

        return count;
    }

    public async Task AddCartAsync(Cart cart)
    {
        _data.Carts.Add(cart);
        await _data.SaveAsync();
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        _data.Carts.Update(cart);
        await _data.SaveAsync();
    }

    public async Task RemoveCartAsync(Cart cart)
    {
        _data.Carts.Remove(cart);
        await _data.SaveAsync();
    }
}