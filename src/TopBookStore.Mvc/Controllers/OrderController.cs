using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Identity;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Mvc.Models;

namespace TopBookStore.Mvc.Controllers;

public class OrderController : Controller
{
    private readonly TopBookStoreContext _context;
    private readonly ITopBookStoreUnitOfWork _data;

    public OrderController(TopBookStoreContext context, ITopBookStoreUnitOfWork data)
    {
        _context = context;
        _data = data;
    }

    public async Task<IActionResult> Index(int id)
    {
        ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        IdentityTopBookStoreUser user = await _context.Users.FindAsync(claim?.Value) ??
            throw new Exception("User not found.");

        Customer customer = await _data.Customers.GetAsync(user.CustomerId) ??
            throw new Exception("Customer not found.");

        Cart? cart = await _data.Carts.GetAsync(id);
        if (cart is null)
        {
            return NotFound();
        }

        OrderDto orderDto = new()
        {
            Name = user.UserName,
            PhoneNumber = user?.PhoneNumber ?? string.Empty,
            TotalAmount = cart.TotalAmount,
            Address = customer.Address,
            Ward = customer.Ward,
            District = customer.District,
            City = customer.City,
            CustomerId = customer.CustomerId
        };

        OrderIndexViewModel vm = new()
        {
            OrderDto = orderDto,
            Cart = cart
        };

        return View(vm);
    }
}