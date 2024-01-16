using Microsoft.AspNetCore.Mvc;

namespace FastAdmin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Logout()
    {
        return View();
    }
}
