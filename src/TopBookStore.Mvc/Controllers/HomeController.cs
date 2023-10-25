using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Constants;
using TopBookStore.Domain.Entities;
using TopBookStore.Infrastructure.Identity;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Mvc.Grid;
using TopBookStore.Mvc.Models;

namespace TopBookStore.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IBookService _bookService;
    private readonly ICartService _cartService;
    private readonly TopBookStoreContext _context;

    public HomeController(IBookService service, ICartService cartService, TopBookStoreContext context)
    {
        _bookService = service;
        _cartService = cartService;
        _context = context;
    }

    public async Task<IActionResult> Index(GridDto values)
    {
        ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        if (claim is not null) // user have login, retrieve the cart item quantity
        {
            IdentityTopBookStoreUser? user = await _context.Users.FindAsync(claim.Value) ??
                throw new Exception("User not found.");

            HttpContext.Session.SetInt32(SessionCookieConstants.CartItemQuantityKey,
                await _cartService.GetQuantityAsync(user.CustomerId) ?? 0);
        }

        GridBuilder gridBuilder = new(HttpContext.Session, values);

        IEnumerable<Book> books;
        if (values.Id is not null)
        {
            // get by category id
            books = await _bookService.GetBooksByCategoryAsync(values);
        }
        else
        {
            // get all
            books = await _bookService.GetAllBooksAsync(values);
        }

        HomeIndexViewModel vm = new()
        {
            Books = books,
            PageNumber = values.PageNumber,
            PageSize = values.PageSize,
            CurrentRoute = gridBuilder.CurrentRoute,
            TotalPages = gridBuilder.GetTotalPages(await _bookService.GetBookCountAsync())
        };

        return View(vm);
    }

    public ViewResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
