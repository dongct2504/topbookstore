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

    public ViewResult Index() => View();

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
            return RedirectToAction("Index");
        }

        ViewBag.Action = category.CategoryId == 0 ? "Add" : "Update";
        return View(category);
    }

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        IEnumerable<Category> categories = await _service.GetAllCategoriesAsync();
        return Json(new { data = categories });
    }

    // public async Task<IActionResult> AddCategory(int? id)
    // {

    // }

    #endregion
}
