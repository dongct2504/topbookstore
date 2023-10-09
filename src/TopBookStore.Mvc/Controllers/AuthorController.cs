using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Mvc.Grid;
using TopBookStore.Mvc.Models;

namespace TopBookStore.Mvc.Controllers;

public class AuthorController : Controller
{
    private readonly IAuthorService _service;

    public AuthorController(IAuthorService service)
    {
        _service = service;
    }

    public RedirectToActionResult Index() => RedirectToAction("List");

    public async Task<ViewResult> List(GridDTO values)
    {
        GridBuilder builder = new(HttpContext.Session, values);

        AuthorListViewModel vm = new()
        {
            Authors = await _service.GetAllAuthorsAsync(),
            CurrentRoute = builder.CurrentRoute
        };

        return View(vm);
    }

    public async Task<IActionResult> Details(int id)
    {
        Author? author = await _service.GetAuthorByIdAsync(id);
        if (author is null)
        {
            return NotFound();
        }

        return View(author);
    }
}