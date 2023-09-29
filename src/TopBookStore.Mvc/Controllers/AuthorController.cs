using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Queries;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Infrastructure.Repositories;
using TopBookStore.Mvc.Grid;
using TopBookStore.Mvc.Models;

namespace TopBookStore.Mvc.Controllers;

public class AuthorController : Controller
{
    private readonly Repository<Author> _data;

    public AuthorController(TopBookStoreContext context)
    {
        _data = new Repository<Author>(context);
    }

    [Route("[controller]")]
    public async Task<ViewResult> List(GridDTO values)
    {
        GridBuilder builder = new(HttpContext.Session, values);

        QueryOptions<Author> options = new()
        {
            Includes = "Books",
            OrderByDirection = builder.CurrentRoute.SortDirection,
            PageNumber = builder.CurrentRoute.PageNumber,
            PageSize = builder.CurrentRoute.PageSize,
        };

        AuthorListViewModel vm = new()
        {
            Authors = await _data.ListAllAsync(options),
            CurrentRoute = builder.CurrentRoute,
            TotalPages = builder.GetTotalPages(_data.Count)
        };

        return View(vm);
    }
}