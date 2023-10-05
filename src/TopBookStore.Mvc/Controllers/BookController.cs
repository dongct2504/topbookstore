using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Queries;
using TopBookStore.Mvc.Grid;
using TopBookStore.Mvc.Models;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Interfaces;

namespace TopBookStore.Mvc.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Author> _authorRepository;

    public BookController(IBookService bookService, IRepository<Category> categoryRepo,
        IRepository<Author> authorRepo)
    {
        _bookService = bookService;
        _categoryRepository = categoryRepo;
        _authorRepository = authorRepo;
    }

    public RedirectToActionResult Index() => RedirectToAction("List");

    public async Task<ViewResult> List(GridDTO values)
    {
        GridBuilder builder = new(HttpContext.Session, values);

        BookListDTO bookListDTO = await _bookService.FilterBooksAsync(values);

        BookListViewModel vm = new()
        {
            Books = bookListDTO.Books.ToList(),
            Categories = await _categoryRepository.ListAllAsync(new QueryOptions<Category>
            {
                OrderBy = c => c.Name
            }),
            Authors = await _authorRepository.ListAllAsync(new QueryOptions<Author>
            {
                OrderBy = a => a.FirstName
            }),
            CurrentRoute = builder.CurrentRoute,
            TotalPages = builder.GetTotalPages(bookListDTO.TotalCount)
        };

        return View(vm);
    }

    public RedirectToActionResult Filter(string[] filter, bool clear = false)
    {
        // get current route segments from session
        GridBuilder builder = new(HttpContext.Session);

        // clear or update filter route segment values. If update, get author data
        // from database so can add author name slug to author filter value.
        if (clear)
        {
            builder.ClearFilterSegments();
        }
        else
        {
            builder.CurrentRoute.PageNumber = 1;
            builder.LoadFilterSegments(filter);
        }

        // save route data back to session and redirect to Book/List action method,
        // passing dictionary of route segment values to build URL
        builder.SaveRouteSegments();
        return RedirectToAction("List", builder.CurrentRoute);
    }

    public async Task<ViewResult> Details(int id)
    {
        Book book = await _bookService.GetBookByIdAsync(id);
        return View(book);
    }
}