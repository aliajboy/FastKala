using FastKala.Application.Interfaces.Product;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Controllers;
public class HomeController : Controller
{
    private readonly IProductService _productService; 
    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var res = await _productService.GetAllProducts();
        return View(res);
    }

    [Route("/ContactUs")]
    public IActionResult ContactUs()
    {
        return View();
    }

    [Route("/About")]
    public IActionResult About()
    {
        return View();
    }

    [Route("/FAQ")]
    public IActionResult FAQ()
    {
        return View();
    }

    [Route("/Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [Route("/Terms-Conditions")]
    public IActionResult Terms()
    {
        return View();
    }
}
