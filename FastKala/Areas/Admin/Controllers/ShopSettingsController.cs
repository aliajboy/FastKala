using Microsoft.AspNetCore.Mvc;

namespace FastKala.Areas.Admin.Controllers;

[Area("Admin")]
public class ShopSettingsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Delivery()
    {
        return View();
    }

    public IActionResult Payment()
    {
        return View();
    }
}
