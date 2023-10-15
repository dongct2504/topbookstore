using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
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
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _hostEnvironment;

    public BookController(IBookService service, IWebHostEnvironment hostEnvironment,
        IMapper mapper, ITopBookStoreUnitOfWork data)
    {
        _service = service;
        _data = data;
        _mapper = mapper;
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
        Book? book = await _service.GetBookByIdAsync(id.GetValueOrDefault());
        if (book is null)
        {
            return NotFound();
        }

        vm.BookDto = _mapper.Map<BookDto>(book);
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
                Book book = _mapper.Map<Book>(bookDto);

                await _data.Books.AddNewCategoriesAsync(book, bookDto.CategoryIds, _data.Categories);

                await _service.AddBookAsync(book);
            }
            else
            {
                QueryOptions<Book> options = new()
                {
                    Where = b => b.BookId == bookDto.BookId,
                    Includes = "Categories"
                };

                Book bookFromDb = await _data.Books.GetAsync(options) ?? new Book();
                bookFromDb.BookId = bookDto.BookId;
                bookFromDb.Title = bookDto.Title;
                bookFromDb.Description = bookDto.Description;
                bookFromDb.Isbn13 = bookDto.Isbn13;
                bookFromDb.Inventory = bookDto.Inventory;
                bookFromDb.Price = bookDto.Price;
                bookFromDb.DiscountPercent = bookDto.DiscountPercent;
                bookFromDb.NumberOfPages = bookDto.NumberOfPages;
                bookFromDb.PublicationDate = bookDto.PublicationDate;
                bookFromDb.ImageUrl = bookDto.ImageUrl;
                bookFromDb.AuthorId = bookDto.AuthorId;
                bookFromDb.PublisherId = bookDto.PublisherId;

                await _data.Books.AddNewCategoriesAsync(bookFromDb, bookDto.CategoryIds, _data.Categories);

                // don't need to call UpdateBookAsync() - db context is tracking changes 
                // because retrieved book with categories from db at the beginning
                await _data.SaveAsync();
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
        await _service.SaveAsync();

        return Json(new { success = true, message = "Xóa thành công!" });
    }

    #endregion
}