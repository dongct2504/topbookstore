using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Mvc.Models;

namespace TopBookStore.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
public class BookController : Controller
{
    private readonly IBookService _service;
    private readonly ITopBookStoreUnitOfWork _data;
    private readonly IWebHostEnvironment _hostEnvironment;

    public BookController(IBookService service, IWebHostEnvironment hostEnvironment,
        ITopBookStoreUnitOfWork data)
    {
        _service = service;
        _data = data;
        _hostEnvironment = hostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Upsert(int? id)
    {
        BookListViewModel vm = new()
        {
            Book = new Book(),
            Categories = await _data.Categories.ListAllAsync(new QueryOptions<Category>()),
            Authors = await _data.Authors.ListAllAsync(new QueryOptions<Author>())
        };

        if (id is null)
        {
            ViewBag.Action = "Add";
            return View(vm);
        }

        ViewBag.Action = "Update";
        vm.Book = await _service.GetBookByIdAsync(id.GetValueOrDefault());
        if (vm.Book is null)
        {
            return NotFound();
        }
        return View(vm);
    }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Upsert(Book book)
    // {
    //     return View();
    // }

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        // // get by category id
        // if (id is not null)
        // {
        //     return Json(new { data = await _service.GetBooksByCategoryAsync(id.GetValueOrDefault()) });
        // }
        // get all
        return Json(new { data = await _service.GetAllBooksAsync() });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBook(int id)
    {
        Book? book = await _service.GetBookByIdAsync(id);
        if (book is null)
        {
            return Json(new { success = false, message = "Lỗi khi xóa!" });
        }

        await _service.DeleteBookAsync(book);
        return Json(new { success = true, message = "Xóa thành công!" });
    }

    #endregion
}