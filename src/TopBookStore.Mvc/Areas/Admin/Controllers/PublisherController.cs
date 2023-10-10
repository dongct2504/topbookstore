using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.Services;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
public class PublisherController : Controller
{
    private readonly IPublisherService _service;

    public PublisherController(IPublisherService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Upsert(int? id)
    {
        Publisher? publisher = new();
        if (id is null)
        {
            ViewBag.Action = "Add";
            return View(publisher);
        }

        ViewBag.Action = "Update";
        publisher = await _service.GetPublisherByIdAsync(id.GetValueOrDefault());
        return View(publisher);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(Publisher publisher)
    {
        if (ModelState.IsValid)
        {
            if (publisher.PublisherId == 0)
            {
                await _service.AddPublisherAsync(publisher);
            }
            else
            {
                await _service.UpdatePublisherAsync(publisher);
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = publisher.PublisherId == 0 ? "Add" : "Update";
        return View(publisher);
    }

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAllPublishers()
    {
        return Json(new { data = await _service.GetAllPublishersAsync() });
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        Publisher? publisher = await _service.GetPublisherByIdAsync(id);
        if (publisher is null)
        {
            return Json(new { success = false, message = "Không tìm thấy tác giả bạn muốn xóa." });
        }

        await _service.DeletePublisherAsync(publisher);
        return Json(new { success = true, message = "Đã xóa thành công." });
    }

    #endregion
}