using Microsoft.AspNetCore.Mvc;

namespace FastAdmin.Controllers;
public class ProductsController : Controller
{
    public IActionResult ProductList()
    {
        return View();
    }

    public IActionResult NewProduct()
    {
        return View();
    }

    public IActionResult EditProduct()
    {
        return View();
    }
}
