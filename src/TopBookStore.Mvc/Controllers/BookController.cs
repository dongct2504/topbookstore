using Microsoft.AspNetCore.Mvc;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Queries;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Infrastructure.UnitOfWork;

namespace TopBookStore.Mvc.Controllers;

public class BookController : Controller
{
    private readonly TopBookStoreUnitOfWork _data;

    public BookController(TopBookStoreContext context)
    {
        _data = new TopBookStoreUnitOfWork(context);
    }

    public RedirectToActionResult Index() => RedirectToAction("List");

    public async Task<ViewResult> List()
    {
        return View();
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