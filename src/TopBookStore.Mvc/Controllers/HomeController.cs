using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Mvc.Grid;
using TopBookStore.Mvc.Models;

namespace TopBookStore.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IBookService _bookService;

    public HomeController(IBookService bookService)
    {
        _bookService = bookService;
    }

    public RedirectToActionResult Index(string? id) => RedirectToAction("List", new { id });

    public async Task<ViewResult> List(GridDTO values, int? id)
    {
        GridBuilder builber = new(HttpContext.Session, values);

        BookListDTO bookListDTO = await _bookService.GetBooksByCategoryAsync(values, id);

        BookListViewModel vm = new()
        {
            Books = bookListDTO.Books.ToList(),
            CurrentRoute = builber.CurrentRoute,
            TotalPages = builber.GetTotalPages(bookListDTO.TotalCount),
            Id = id ?? 0
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
