using FastKala.Application.Interfaces;
using FastKala.Application.ViewModels.Global;
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
    public async Task<IActionResult> NewProduct(int? id)
    {
        if (id == null)
        {
            var attr = await _productService.GetAllProductAttributes();
            ProductViewModel model = new ProductViewModel() { ProductAttributes = attr.ProductAttributes, Product = new() { Name = "" } };
            return View(model);
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> NewProduct(ProductViewModel productView)
    {
        if (!ModelState.IsValid)
        {
            return View(productView);
        }

        var res = await _productService.AddProduct(productView);

        if (res.OperationStatus == FastKala.Domain.Enums.OperationStatus.Success)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(productView);
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

    [HttpPost]
    public async Task<OperationResult> RemoveAttribute(int id)
    {
        return new OperationResult();
    }

    public IActionResult EditProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveProduct()
    {
        return View();
    }

    public async Task<IActionResult> AttributeValues(int id)
    {
        return View(await _productService.GetProductAttributeById(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddAttributeValue(string ValueName, string ValueLink, int Id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var res = await _productService.AddAttributeValue(ValueName, ValueLink, Id);

        if (res.OperationStatus != FastKala.Domain.Enums.OperationStatus.Success)
        {
            return View();
        }

        return PartialView("_ProductAttributeValuePartial", await _productService.GetProductAttributeById(Id));
    }

    [HttpGet]
    public async Task<AttributeValuesResponse> GetAttributeValues(int attributeId, string search)
    {
        var res = await _productService.GetAttributeValuesByIdAjax(attributeId, search);

        return res;
    }
}
