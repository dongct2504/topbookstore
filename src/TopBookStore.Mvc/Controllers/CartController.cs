using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Constants;
using TopBookStore.Domain.Entities;
using TopBookStore.Infrastructure.Identity;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Mvc.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ICartItemService _cartItemService;
    private readonly TopBookStoreContext _context;
    private readonly IMapper _mapper;

    public CartController(ICartService service, ICartItemService cartItemService,
        TopBookStoreContext context, IMapper mapper)
    {
        _cartService = service;
        _cartItemService = cartItemService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        IdentityTopBookStoreUser user = await _context.Users.FindAsync(claim?.Value)
            ?? throw new Exception("User not found.");

        HttpContext.Session.SetInt32(SessionCookieConstants.CartItemQuantityKey,
            await _cartService.GetTotalCartItemsCountAsync(user.CustomerId));

        Cart cart = await _cartService.GetCartByCustomerAsync(user.CustomerId) ?? new Cart();

        cart.CartItems = (await _cartItemService.GetAllCartItemsByCartIdAsync(cart.CartId)).ToList();

        return View(cart);
    }

    // only for add cart item from cart when hit repair.
    [HttpGet]
    public async Task<IActionResult> AddCartItem(CartItem cartItem)
    {
        ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        IdentityTopBookStoreUser? user = await _context.Users.FindAsync(claim?.Value)
            ?? throw new Exception("User not found.");

        await _cartService.AddCartItemAsync(user.CustomerId, cartItem);

        return RedirectToAction(nameof(Index));
    }

    // only for add cart item from book details
    [HttpGet]
    public async Task<IActionResult> AddCartItemForBookDetails(CartItemDto cartItemDto)
    {
        ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        IdentityTopBookStoreUser? user = await _context.Users.FindAsync(claim?.Value)
            ?? throw new Exception("User not found.");

        await _cartService.AddCartItemAsync(user.CustomerId, _mapper.Map<CartItem>(cartItemDto));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> RemoveCartItem(int id)
    {
        ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        IdentityTopBookStoreUser? user = await _context.Users.FindAsync(claim?.Value)
            ?? throw new Exception("User not found.");

        CartItem? cartItem = await _cartItemService.GetCartItemByIdAsync(id);
        if (cartItem is null)
        {
            return NotFound();
        }

        await _cartService.RemoveCartItemAsync(user.CustomerId, cartItem);

        return RedirectToAction(nameof(Index));
    }
}