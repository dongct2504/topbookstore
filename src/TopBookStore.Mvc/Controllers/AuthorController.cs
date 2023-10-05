using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Queries;
using TopBookStore.Mvc.Grid;
using TopBookStore.Mvc.Models;

namespace TopBookStore.Mvc.Controllers;

public class AuthorController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public RedirectToActionResult Index() => RedirectToAction("List");

    public async Task<ViewResult> List(GridDTO values)
    {
        GridBuilder builder = new(HttpContext.Session, values);

        AuthorListDTO authorListDTO = await _authorService.GetAllAuthorsAsync(values);

        AuthorListViewModel vm = new()
        {
            Authors = authorListDTO.Authors.ToList(),
            CurrentRoute = builder.CurrentRoute,
            TotalPages = builder.GetTotalPages(authorListDTO.TotalCount)
        };

        return View(vm);
    }

    public async Task<ViewResult> Details(int id)
    {
        Author author = await _authorService.GetAuthorByIdAsync(id);

        return View(author);
    }
}