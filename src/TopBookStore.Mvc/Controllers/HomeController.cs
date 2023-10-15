using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.Interfaces;
using TopBookStore.Mvc.Models;

namespace TopBookStore.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IBookService _service;

    public HomeController(IBookService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(int? id)
    {
        // get by category id
        if (id is not null)
        {
            return View(await _service.GetBooksByCategoryAsync(id.GetValueOrDefault()));
        }
        // get all
        return View(await _service.GetAllBooksAsync());
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
