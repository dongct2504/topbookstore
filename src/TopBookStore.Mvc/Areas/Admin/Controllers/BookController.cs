using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Constants;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Mvc.Models;

namespace TopBookStore.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = RoleConstants.RoleAdmin + "," + RoleConstants.RoleLibrarian)]
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
            BookDto = new BookDto(),
            Categories = await _data.Categories.ListAllAsync(new QueryOptions<Category>()),
        };

        if (id is null)
        {
            ViewBag.Action = "Add";
            return View(vm);
        }

        ViewBag.Action = "Update";
        BookDto? bookDto = await _service.GetBookDtoByIdAsync(id.GetValueOrDefault());
        if (bookDto is null)
        {
            return NotFound();
        }

        vm.BookDto = bookDto;
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(BookDto bookDto)
    {
        if (ModelState.IsValid)
        {
            // handle image
            string webRootPath = _hostEnvironment.WebRootPath;

            IFormFileCollection files = HttpContext.Request.Form.Files;
            if (files.Count > 0) // has file
            {
                string fileName = Guid.NewGuid().ToString(); // make it unique
                string pathUploads = Path.Combine(webRootPath, @"imgs\books");
                string fileExtension = Path.GetExtension(files[0].FileName);

                if (bookDto.ImageUrl is not null)
                {
                    // this is an update
                    string imagePath = Path.Combine(webRootPath, bookDto.ImageUrl.TrimStart('\\'));
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

                bookDto.ImageUrl = @"\imgs\books\" + fileName + fileExtension;
            }

            if (bookDto.BookId == 0)
            {
                await _service.AddBookAsync(bookDto);
            }
            else
            {
                await _service.UpdateBookAsync(bookDto);
            }
            return RedirectToAction(nameof(Index));
        }

        BookListViewModel vm = new()
        {
            BookDto = bookDto,
            Categories = await _data.Categories.ListAllAsync(new QueryOptions<Category>()),
        };

        ViewBag.Action = bookDto.BookId == 0 ? "Add" : "Update";
        return View(vm);
    }

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
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

        if (book.ImageUrl is not null)
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            string imagePath = Path.Combine(webRootPath, book.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        await _service.RemoveBookAsync(book);
        return Json(new { success = true, message = "Xóa thành công!" });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteImage(int id)
    {
        Book? book = await _service.GetBookByIdAsync(id);
        if (book is null)
        {
            return Json(new { success = false, message = "không tìm thấy sách!" });
        }

        if (book.ImageUrl is null)
        {
            return Json(new { success = false, message = "Không có ảnh!" });
        }

        string webRootPath = _hostEnvironment.WebRootPath;
        string imagePath = Path.Combine(webRootPath, book.ImageUrl.TrimStart('\\'));
        if (!System.IO.File.Exists(imagePath))
        {
            return Json(new { success = false, message = "Ảnh url Không còn tồn tại!" });
        }

        System.IO.File.Delete(imagePath);
        book.ImageUrl = null;

        await _service.UpdateBookAsync(book);

        return Json(new { success = true, message = "Xóa thành công!" });
    }

    #endregion
}