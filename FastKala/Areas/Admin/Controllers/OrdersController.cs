using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class OrdersController : Controller
{
    public OrdersController()
    {
        
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddOrder()
    {
        return View();
    }
}