using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Services;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
public class PublisherController : Controller
{
    private readonly IPublisherService _service;
    private readonly IMapper _mapper;

    public PublisherController(IPublisherService service, IMapper mapper)
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
        PublisherDto? publisherDto = new();
        if (id is null)
        {
            ViewBag.Action = "Add";
            return View(publisherDto);
        }

        ViewBag.Action = "Update";
        publisherDto = _mapper.Map<PublisherDto>(
            await _service.GetPublisherByIdAsync(id.GetValueOrDefault()));
        if (publisherDto is null)
        {
            return NotFound();
        }
        return View(publisherDto);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(PublisherDto publisherDto)
    {
        if (ModelState.IsValid)
        {
            Publisher publisher = _mapper.Map<Publisher>(publisherDto);

            if (publisherDto.PublisherId == 0)
            {
                await _service.AddPublisherAsync(publisher);
            }
            else
            {
                await _service.UpdatePublisherAsync(publisher);
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = publisherDto.PublisherId == 0 ? "Add" : "Update";
        return View(publisherDto);
    }

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAllPublishers()
    {
        return Json(new { data = await _service.GetAllPublishersAsync() });
    }

    [HttpGet]
    public async Task<IActionResult> SearchPublishers(string term)
    {
        IEnumerable<Publisher> publishers = await _service.GetPublishersByTermAsync(term);

        return Json(publishers.Select(p => new { id = p.PublisherId, label = p.Name }));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        Publisher? publisher = await _service.GetPublisherByIdAsync(id);
        if (publisher is null)
        {
            return Json(new { success = false, message = "Không tìm thấy tác giả bạn muốn xóa." });
        }

        await _service.RemovePublisherAsync(publisher);
        return Json(new { success = true, message = "Đã xóa thành công." });
    }

    #endregion
}