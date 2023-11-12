using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using TopBookStore.Application.Interfaces;
using TopBookStore.Application.Services;
using TopBookStore.Domain.Constants;
using TopBookStore.Mvc.Models.Chart;

namespace TopBookStore.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.RoleAdmin)]
    public class ChartController : Controller
    {
        private readonly IPublisherService _publisherService;
        private readonly IBookService _bookService;

        public ChartController
            (
                IBookService bookService,
                IPublisherService publisherService
            )
        {
            _bookService = bookService;
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dataWareHouseParent = new List<ParentDrillDownViewModel>();
            var dataWareHouseChild = new List<ChildDrillDownViewModel>();
            var dataSoldParent = new List<ParentDrillDownViewModel>();
            var dataSoldChild = new List<ChildDrillDownViewModel>();

            var publishers = await _publisherService.GetAllPublishersAsync();
            foreach (var publisher in publishers)
            {
                dataWareHouseParent.Add(new ParentDrillDownViewModel() { name = publisher.Name, y = 0, drilldown = publisher.PublisherId });
                dataSoldParent.Add(new ParentDrillDownViewModel() { name = publisher.Name, y = 0, drilldown = publisher.PublisherId });
            }

            var books = await _bookService.GetAllBooksAsync();
            foreach (var book in books)
            {
                if (dataWareHouseChild.ElementAtOrDefault(book.PublisherId) == null)
                {
                    dataWareHouseChild.Add(new ChildDrillDownViewModel()
                    {
                        id = book.PublisherId,
                        name = book.Publisher.Name
                    });
                }

                dataWareHouseChild.Where(x => x.id == book.PublisherId).First().data
                    = books.Where(x => x.PublisherId == book.PublisherId)
                        .Select(x => (new List<object> { x.Title, x.Inventory }).ToList());
                dataWareHouseParent.Where(x => x.drilldown == book.PublisherId).First().y += book.Inventory;

                if (dataSoldChild.ElementAtOrDefault(book.PublisherId) == null)
                {
                    dataSoldChild.Add(new ChildDrillDownViewModel()
                    {
                        id = book.PublisherId,
                        name = book.Publisher.Name
                    });
                }

                dataSoldChild.Where(x => x.id == book.PublisherId).First().data
                    = books.Where(x => x.PublisherId == book.PublisherId)
                        .Select(x => (new List<object> { x.Title, x.OrderDetails.Sum(x => x.Quantity) }).ToList());
                dataSoldParent.Where(x => x.drilldown == book.PublisherId).First().y += book.OrderDetails.Sum(x => x.Quantity);
            }

            ViewData["dataWareHouseParent"] = dataWareHouseParent.ToJson();
            ViewData["dataWareHouseChild"] = dataWareHouseChild.ToJson();
            ViewData["dataSoldParent"] = dataSoldParent.ToJson();
            ViewData["dataSoldChild"] = dataSoldChild.ToJson();

            return View();
        }
    }
}