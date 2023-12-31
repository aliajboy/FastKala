using FastKala.Application.Interfaces;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FastAdmin.Controllers;
public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // Products ---------------------------------------
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

    public IActionResult EditProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveProduct(int id)
    {
        await _productService.RemoveProductById(id);
        return RedirectToAction(nameof(Index));
    }

    // Attributes --------------------------------------

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
    public async Task<IActionResult> RemoveAttribute(int id)
    {
        var result = await _productService.RemoveAttributeById(id);
        return RedirectToAction(nameof(Attributes));
    }

    // Atribute Values ----------------------------------

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
        var res = await _productService.GetAttributeValues(attributeId, search);

        return res;
    }

    [HttpGet]
    public async Task<ProductAttributeValueViewModel> GetAttributeValue(int attributeId)
    {
        var res = await _productService.GetAttributeValue(attributeId);

        return res;
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAttributeValue(int id, int attributeId, string name, string value)
    {
        var res = await _productService.UpdateAttributeValue(id, name, value);
        if (res.OperationStatus != FastKala.Domain.Enums.OperationStatus.Success)
        {
            return View();
        }

        return PartialView("_ProductAttributeValuePartial", await _productService.GetProductAttributeById(attributeId));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveAttributeValue(int attributeValueId, int attributeId)
    {
        var result = await _productService.RemoveAttributeValue(attributeValueId);
        if (result.OperationStatus != FastKala.Domain.Enums.OperationStatus.Success)
        {
            return View();
        }
        return PartialView("_ProductAttributeValuePartial", await _productService.GetProductAttributeById(attributeId));
    }

    // Product Categoriess -------------------------------------

    public async Task<IActionResult> Categories()
    {
        return View();
    }
}