using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Queries;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Infrastructure.UnitOfWork;
using TopBookStore.Mvc.Grid;
using TopBookStore.Domain.Extensions;
using TopBookStore.Mvc.Models;

namespace TopBookStore.Mvc.Controllers;

public class BookController : Controller
{
    private readonly TopBookStoreUnitOfWork _data;

    public BookController(TopBookStoreContext context)
    {
        _data = new TopBookStoreUnitOfWork(context);
    }

    public RedirectToActionResult Index() => RedirectToAction("List");

    public async Task<ViewResult> List(GridDTO values)
    {
        // I will make sort, filter in this
        GridBuilder builder = new(HttpContext.Session, values);

        QueryOptions<Book> options = new()
        {
            Includes = "Authors, Category",
            OrderByDirection = builder.CurrentRoute.SortDirection,
            PageNumber = builder.CurrentRoute.PageNumber,
            PageSize = builder.CurrentRoute.PageSize
        };

        // filter
        if (builder.IsFilterByCategory)
        {
            options.Where = b => b.CategoryId == builder.CurrentRoute.CategoryFilter;
        }
        if (builder.IsFilterByPrice)
        {
            if (builder.CurrentRoute.PriceFilter.EqualsNoCase("under50"))
            {
                options.Where = b => b.Price < 50_000;
            }
            else if (builder.CurrentRoute.PriceFilter.EqualsNoCase("50to150"))
            {
                options.Where = b => b.Price >= 50_000 && b.Price <= 150_000;
            }
            else if (builder.CurrentRoute.PriceFilter.EqualsNoCase("150to500"))
            {
                options.Where = b => b.Price > 150_000 && b.Price <= 500_000;
            }
            else if (builder.CurrentRoute.PriceFilter.EqualsNoCase("500to1000"))
            {
                options.Where = b => b.Price > 500_000 && b.Price <= 1_000_000;
            }
            else if (builder.CurrentRoute.PriceFilter.EqualsNoCase("over1000"))
            {
                options.Where = b => b.Price > 1_000_000;
            }
        }
        if (builder.IsFilterByNumberOfPages)
        {
            if (builder.CurrentRoute.NumberOfPagesFilter.EqualsNoCase("under100"))
            {
                options.Where = b => b.NumberOfPages < 100;
            }
            else if (builder.CurrentRoute.NumberOfPagesFilter.EqualsNoCase("100to500"))
            {
                options.Where = b => b.NumberOfPages >= 100 && b.NumberOfPages <= 500;
            }
            else if (builder.CurrentRoute.NumberOfPagesFilter.EqualsNoCase("over500"))
            {
                options.Where = b => b.NumberOfPages > 500;
            }
        }
        if (builder.IsFilterByAuthor)
        {
            _ = int.TryParse(builder.CurrentRoute.AuthorFilter, out int authorId) ? authorId : 0;
            if (authorId > 0)
            {
                // to filter the books by author, use the LINQ Any() method. 
                options.Where = b => b.Authors.Any(a => a.AuthorId == authorId);
            }
        }

        // sort
        if (builder.IsSortByCategory)
        {
            options.OrderBy = b => b.Category.Name;
        }
        else if (builder.IsSortByPrice)
        {
            options.OrderBy = b => b.Price;
        }

        BookListViewModel vm = new()
        {
            Books = await _data.Books.ListAllAsync(options),
            Categories = await _data.Categories.ListAllAsync(new QueryOptions<Category>
            {
                OrderBy = c => c.Name
            }),
            Authors = await _data.Authors.ListAllAsync(new QueryOptions<Author>
            {
                OrderBy = a => a.FirstName
            }),
            CurrentRoute = builder.CurrentRoute,
            TotalPages = builder.GetTotalPages(_data.Books.Count)
        };

        return View(vm);
    }

    public RedirectToActionResult FilterBooks(string[] filter, bool clear = false)
    {
        GridBuilder builder = new(HttpContext.Session);

        if (clear)
        {
            builder.ClearFilterSegments();
        }
        else
        {
            builder.CurrentRoute.PageNumber = 1;
            builder.LoadFilterSegments(filter);
        }
        builder.SaveRouteDirection();

        return RedirectToAction("List", builder.CurrentRoute);
    }

    public async Task<ViewResult> Details(int id)
    {
        Book book = await _data.Books.GetAsync(new QueryOptions<Book>
        {
            Where = b => b.BookId == id,
            Includes = "Authors, Category"
        }) ?? new Book();

        return View(book);
    }
}