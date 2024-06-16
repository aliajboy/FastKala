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
    public async Task<IActionResult> ContactUs()
    {
        return View();
    }

    [Route("/About")]
    public async Task<IActionResult> About()
    {
        return View();
    }

    [Route("/FAQ")]
    public async Task<IActionResult> FAQ()
    {
        return View();
    }

    [Route("/Privacy")]
    public async Task<IActionResult> Privacy()
    {
        return View();
    }

    [Route("/Terms-Conditions")]
    public async Task<IActionResult> Terms()
    {
        return View();
    }
}
