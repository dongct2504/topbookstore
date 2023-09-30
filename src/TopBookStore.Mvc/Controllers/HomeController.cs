using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Infrastructure.UnitOfWork;
using TopBookStore.Mvc.Grid;
using TopBookStore.Mvc.Models;

namespace TopBookStore.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ITopBookStoreUnitOfWork _data;

    public HomeController(TopBookStoreContext context)
    {
        _data = new TopBookStoreUnitOfWork(context);
    }

    public RedirectToActionResult Index(string? id) => RedirectToAction("List", new { id });

    public async Task<ViewResult> List(GridDTO values, string? id)
    {
        GridBuilder builber = new(HttpContext.Session, values);

        QueryOptions<Book> options = new()
        {
            Includes = "Authors, Category",
            PageSize = builber.CurrentRoute.PageSize,
            PageNumber = builber.CurrentRoute.PageNumber
        };

        if (id is not null && id != string.Empty)
        {
            options.Where = b => b.CategoryId == id;
        }

        BookListViewModel vm = new()
        {
            Books = await _data.Books.ListAllAsync(options),
            CurrentRoute = builber.CurrentRoute,
            TotalPages = builber.GetTotalPages(_data.Books.Count),
            Id = id ?? string.Empty
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
