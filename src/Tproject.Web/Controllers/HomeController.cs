using Microsoft.AspNetCore.Mvc;

namespace Tproject.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Projects");
    }
}
