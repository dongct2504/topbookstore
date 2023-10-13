using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Application.Mappers;
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
            BookDTO = new BookDTO(),
            Categories = await _data.Categories.ListAllAsync(new QueryOptions<Category>()),
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

        vm.BookDTO = BookMapper.MapToDTO(book);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(BookDTO bookDTO)
    {
        if (ModelState.IsValid)
        {
            // handle image
            string webRootPath = _hostEnvironment.WebRootPath;

            IFormFileCollection files = HttpContext.Request.Form.Files;
            if (files.Count > 0) // has file
            {
                string fileName = Guid.NewGuid().ToString(); // make it unique.
                string pathUploads = Path.Combine(webRootPath, @"imgs\books");
                string fileExtension = Path.GetExtension(files[0].FileName);

                if (bookDTO.ImageUrl is not null)
                {
                    // this is an update.
                    string imagePath = Path.Combine(webRootPath, bookDTO.ImageUrl.TrimStart('\\'));
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

                bookDTO.ImageUrl = @"\imgs\books\" + fileName + fileExtension;
            }

            await _service.UpsertBookAsync(bookDTO);

            return RedirectToAction(nameof(Index));
        }

        BookListViewModel vm = new()
        {
            BookDTO = bookDTO,
            Categories = await _data.Categories.ListAllAsync(new QueryOptions<Category>()),
        };

        ViewBag.Action = bookDTO.BookId == 0 ? "Add" : "Update";
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