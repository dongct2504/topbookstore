using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    public ViewResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Upsert(int? id)
    {
        Category? category = new();

        // for add
        if (id is null)
        {
            ViewBag.Action = "Add";
            return View(category);
        }

        // for update
        ViewBag.Action = "Update";
        category = await _service.GetCategoryByIdAsync(id.GetValueOrDefault());
        if (category is null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Category category)
    {
        if (ModelState.IsValid)
        {
            if (category.CategoryId == 0)
            {
                await _service.AddCategoryAsync(category);
            }
            else
            {
                await _service.UpdateCategoryAsync(category);
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = category.CategoryId == 0 ? "Add" : "Update";
        return View(category);
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
