using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Queries;
using TopBookStore.Mvc.Grid;
using TopBookStore.Mvc.Models;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Interfaces;
using System.Security.Claims;
using TopBookStore.Infrastructure.Identity;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Mvc.Controllers;

public class BookController : Controller
{
    private readonly IBookService _service;
    private readonly ITopBookStoreUnitOfWork _data;
    private readonly TopBookStoreContext _context;

    public BookController(IBookService service, ITopBookStoreUnitOfWork data,
        TopBookStoreContext context)
    {
        _service = service;
        _data = data;
        _context = context;
    }

    public async Task<IActionResult> Index(BookGridDto values)
    {
        GridBuilder gridBuilder = new(HttpContext.Session, values);

        BookListViewModel vm = new()
        {
            Books = await _service.FilterBooksAsync(values),
            Categories = await _data.Categories.ListAllAsync(new QueryOptions<Category>
            {
                OrderBy = a => a.Name
            }),
            Authors = await _data.Authors.ListAllAsync(new QueryOptions<Author>
            {
                OrderBy = a => a.FirstName
            }),
            PageNumber = values.PageNumber,
            PageSize = values.PageSize,
            CurrentRoute = gridBuilder.CurrentRoute,
            TotalPages = gridBuilder.GetTotalPages(await _service.GetBookCountAsync())
        };

        return View(vm);
    }

    public async Task<IActionResult> Details(int id)
    {
        Book? book = await _service.GetBookByIdAsync(id);
        if (book is null)
        {
            return NotFound();
        }

        CartItemDto cartItemDto = new()
        {
            BookId = book.BookId,
            Quantity = 1
        };

        ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        if (claim is not null) // user have login, retrieve the cart item quantity
        {
            IdentityTopBookStoreUser? user = await _context.Users.FindAsync(claim.Value)
                ?? throw new Exception("User not found.");

            Cart? cart = await _data.Carts.GetAsync(new QueryOptions<Cart>
            {
                Where = c => c.CustomerId == user.CustomerId
            });

            if (cart is not null)
            {
                // first, it need to be exist in cart (belong to what cart)
                // second, does it match the book we need to find in the cart?
                CartItem? existingCartItem = await _data.CartItems.GetAsync(new QueryOptions<CartItem>
                {
                    Where = ci => ci.CartId == cart.CartId && ci.BookId == book.BookId
                });

                // Already have one in db
                if (existingCartItem is not null)
                {
                    cartItemDto.CartId = existingCartItem.CartId;
                    cartItemDto.CartItemId = existingCartItem.CartItemId;
                    cartItemDto.Quantity = existingCartItem.Quantity;
                }
            }
        }

        BookDetailsViewModel vm = new()
        {
            CartItemDto = cartItemDto,
            Book = book
        };

        return View(vm);
    }

    public RedirectToActionResult FilterBooks(string[] filter, bool clear = false)
    {
        // get current route segments from session
        GridBuilder gridBuilder = new(HttpContext.Session);

        // clear or update filter route segment values. If update, get author data
        // from database so can add author name slug to author filter value.
        if (clear)
        {
            gridBuilder.ClearFilterSegments();
        }
        else
        {
            gridBuilder.CurrentRoute.PageNumber = 1;
            gridBuilder.LoadFilterSegments(filter);
        }

        // save route data back to session and redirect to Book/List action method,
        // passing dictionary of route segment values to build URL
        gridBuilder.SaveRouteSegments();

        return RedirectToAction(nameof(Index), gridBuilder.CurrentRoute);
    }


    #region APIS CALL

    [HttpGet]
    public async Task<IActionResult> SearchBooks(string term)
    {
        IEnumerable<Book> books = await _service.GetBooksByTerm(term);

        return Json(books.Select(b => new { id = b.BookId, label = b.Title }));
    }

    #endregion
}