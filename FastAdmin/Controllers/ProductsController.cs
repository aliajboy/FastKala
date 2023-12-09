using FastKala.Application.Interfaces;
using FastKala.Application.ViewModels.Products;
using FastKala.Domain.Enums;
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
    public IActionResult NewProduct(int? id)
    {
        if (id == null)
        {
            return View();
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> NewProduct(ProductViewModel productView)
    {
        return View();
    }

    public async Task<IActionResult> Attributes()
    {
        return View(await _productService.GetAllProductAttributes());
    }

    [HttpPost]
    public async Task<PartialViewResult> AddNewAttribute(string attributeName, string attributeLink, int attributeType)
    {
        await _productService.AddProductAttribute(attributeName, attributeLink, attributeType);
        return PartialView("_ProductAttributePartial", await _productService.GetAllProductAttributes());
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
