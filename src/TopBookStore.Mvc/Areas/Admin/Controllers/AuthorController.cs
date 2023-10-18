using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Constants;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = RoleConstants.RoleAdmin + "," + RoleConstants.RoleLibrarian)]
public class AuthorController : Controller
{
    private readonly IAuthorService _service;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Upsert(int? id)
    {
        AuthorDto? authorDto = new();
        if (id is null)
        {
            ViewBag.Action = "Add";
            return View(authorDto);
        }

        ViewBag.Action = "Update";
        authorDto = _mapper.Map<AuthorDto>(await _service.GetAuthorByIdAsync(id.GetValueOrDefault()));
        if (authorDto is null)
        {
            return NotFound();
        }
        return View(authorDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(AuthorDto authorDto)
    {
        if (ModelState.IsValid)
        {
            Author author = _mapper.Map<Author>(authorDto);

            if (authorDto.AuthorId == 0) // add
            {
                await _service.AddAuthorAsync(author);
            }
            else // update
            {
                await _service.UpdateAuthorAsync(author);
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = authorDto.AuthorId == 0 ? "Add" : "Update";
        return View(authorDto);
    }

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        return Json(new { data = await _service.GetAllAuthorsAsync() });
    }

    [HttpGet]
    public async Task<IActionResult> SearchAuthors(string term)
    {
        IEnumerable<Author> authors = await _service.GetAuthorsByTermAsync(term);

        return Json(authors.Select(a => new { id = a.AuthorId, label = a.FullName }));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        Author? author = await _service.GetAuthorByIdAsync(id);
        if (author is null)
        {
            return Json(new { success = false, message = "Không tìm thấy tác giả bạn muốn xóa." });
        }

        await _service.RemoveAuthorAsync(author);
        return Json(new { success = true, message = "Đã xóa thành công." });
    }

    #endregion
}