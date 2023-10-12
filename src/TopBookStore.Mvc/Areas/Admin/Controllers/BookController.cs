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
            Authors = await _data.Authors.ListAllAsync(new QueryOptions<Author>()),
            Publishers = await _data.Publishers.ListAllAsync(new QueryOptions<Publisher>())
        };

        if (id is null)
        {
            ViewBag.Action = "Add";
            return View(vm);
        }

        ViewBag.Action = "Update";
        Book? book = await _service.GetBookByIdAsync(id.GetValueOrDefault());
        if (book is null)
        {
            return NotFound();
        }

        vm.Book = book;
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Book book)
    {
        if (ModelState.IsValid)
        {
            string webRootPath = _hostEnvironment.WebRootPath;

            IFormFileCollection files = HttpContext.Request.Form.Files;

            if (files.Count > 0) // has file.
            {
                string fileName = Guid.NewGuid().ToString(); // make it unique.
                string pathUploads = Path.Combine(webRootPath, @"imgs\books");
                string fileExtension = Path.GetExtension(files[0].FileName);

                if (book.ImageUrl is not null)
                {
                    // this is an update.
                    string imagePath = Path.Combine(webRootPath, book.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                using (FileStream fs = new(
                    Path.Combine(pathUploads, fileName + fileExtension), FileMode.Create))
                {
                    await files[0].CopyToAsync(fs);
                }

                book.ImageUrl = @"\imgs\books\" + fileName + fileExtension;
            }

            if (book.BookId == 0)
            {
                await _service.AddBookAsync(book);
            }
            else
            {
                await _service.UpdateBookAsync(book);
            }
            return RedirectToAction(nameof(Index));
        }

        BookListViewModel vm = new()
        {
            Book = book,
            Categories = await _data.Categories.ListAllAsync(new QueryOptions<Category>()),
        };

        ViewBag.Action = book.BookId == 0 ? "Add" : "Update";
        return View(vm);
    }

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAllBooks(int? id = null)
    {
        // get by category id
        if (id is not null)
        {
            return Json(new { data = await _service.GetBooksByCategoryAsync(id.GetValueOrDefault()) });
        }
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

        await _service.RemoveBookAsync(book);
        return Json(new { success = true, message = "Xóa thành công!" });
    }

    #endregion
}