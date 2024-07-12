using Microsoft.AspNetCore.Mvc;

namespace FastKala.Areas.Admin.Controllers;

[Area("Admin")]
public class SettingsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Users()
    {
        return View();
    }

    public IActionResult MainPage()
    {
        return View();
    }
}
