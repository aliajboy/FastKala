using Microsoft.AspNetCore.Mvc;

namespace FastKala.Controllers;
public class AccountController : Controller
{
    public AccountController()
    {
        
    }

    [Route("Login")]
    public IActionResult Login()
    {
        return View();
    }

    [Route("Register")]
    public IActionResult Register()
    {
        return View();
    }
}
