using Microsoft.AspNetCore.Mvc;

namespace TopBookStore.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    public ViewResult Index() => View();
}
