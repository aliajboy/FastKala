using FastKala.Application.Interfaces;
using FastKala.Application.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace FastAdmin.Controllers;
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProducts();
        return View(products);
    }

    [HttpGet]
    public IActionResult NewProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> NewProduct(ProductViewModel productView)
    {
        return View();
    }

    public IActionResult EditProduct()
    {
        return View();
    }

    public IActionResult RemoveProduct()
    {
        return View();
    }
}
