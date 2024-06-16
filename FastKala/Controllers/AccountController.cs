using Microsoft.AspNetCore.Mvc;

namespace FastKala.Controllers;
public class AccountController : Controller
{
    public AccountController()
    {
        
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
}
