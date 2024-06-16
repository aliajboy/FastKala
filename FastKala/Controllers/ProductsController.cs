﻿using FastKala.Application.Interfaces.Product;
using FastKala.Application.ViewModels.Products;
using FastKala.Domain.Enums.Global;
using FastKala.Domain.Enums.Products;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Controllers;
public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProducts(ProductStatus.Published);
        return View(products);
    }

    [Route("/Product/{id}")]
    public async Task<IActionResult> Product(int id)
    {
        var productModel = await _productService.GetProductById(id);

        if (productModel.Product.Name == "پیش فرض" || productModel.Product.Status != ProductStatus.Published)
        {
            return NotFound();
        }

        return View(productModel);
    }

    public async Task<IActionResult> AddComment(int id)
    {
        ProductCommentViewModel commentViewModel = new ProductCommentViewModel() 
        { 
            Product = await _productService.GetProductById(id) 
        };

        return View(commentViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(ProductCommentViewModel commentViewModel)
    {
        var result = await _productService.AddProductComment(commentViewModel);
        if (result.OperationStatus == OperationStatus.Success)
        {
            return RedirectToAction("Product", "Products", new { id = commentViewModel.ProductComment.ProductId });
        }
        return RedirectToAction("AddComment", new { id = commentViewModel.ProductComment.ProductId });
    }
}
