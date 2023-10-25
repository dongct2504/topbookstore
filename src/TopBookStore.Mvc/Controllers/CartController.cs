using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Constants;
using TopBookStore.Domain.Entities;
using TopBookStore.Infrastructure.Identity;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Mvc.Controllers;

[Authorize(Roles = RoleConstants.RoleCustomer)]
public class CartController : Controller
{
    private readonly ICartService _service;
    private readonly TopBookStoreContext _context;

    public CartController(ICartService service, TopBookStoreContext context)
    {
        _service = service;
        _context = context;
    }

    public ViewResult Index()
    {
        return View();
    }

    public async Task<IActionResult> AddCartItem(CartItem cartItem)
    {
        ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        IdentityTopBookStoreUser user = await _context.Users.FindAsync(claim?.Value) ??
            throw new Exception("User not found.");

        await _service.AddCartItemAsync(user.CustomerId, cartItem);

        HttpContext.Session.SetInt32(SessionCookieConstants.CartItemQuantityKey,
            await _service.GetQuantityAsync(user.CustomerId) ?? 0);

        return RedirectToAction("Index", "Home");
    }
}