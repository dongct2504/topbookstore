using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly ICategoryService _service;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public ViewResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Upsert(int? id)
    {
        CategoryDto? categoryDto = new();

        // for add
        if (id is null)
        {
            ViewBag.Action = "Add";
            return View(categoryDto);
        }

        // for update
        ViewBag.Action = "Update";
        categoryDto = _mapper.Map<CategoryDto>(await _service.GetCategoryByIdAsync(id.GetValueOrDefault()));
        if (categoryDto is null)
        {
            return NotFound();
        }
        return View(categoryDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(CategoryDto categoryDto)
    {
        if (ModelState.IsValid)
        {
            Category category = _mapper.Map<Category>(categoryDto);

            if (categoryDto.CategoryId == 0)
            {
                await _service.AddCategoryAsync(category);
            }
            else
            {
                await _service.UpdateCategoryAsync(category);
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = categoryDto.CategoryId == 0 ? "Add" : "Update";
        return View(categoryDto);
    }

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        return Json(new { data = await _service.GetAllCategoriesAsync() });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        Category? category = await _service.GetCategoryByIdAsync(id);
        if (category is null)
        {
            return Json(new { success = false, message = "Không tìm thấy thể loại bạn muốn xóa." });
        }

        await _service.RemoveCategoryAsync(category);
        return Json(new { success = true, message = "Đã Xóa thành công." });
    }

    #endregion
}
