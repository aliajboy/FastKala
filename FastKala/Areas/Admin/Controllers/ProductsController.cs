﻿using FastKala.Application.Interfaces.Global;
using FastKala.Application.Interfaces.Product;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Products;
using FastKala.Domain.Enums.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastAdmin.Controllers;

[Area("Admin")]
[Authorize]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IUploadService _uploadService;

    public ProductsController(IProductService productService, IUploadService uploadService)
    {
        _productService = productService;
        _uploadService = uploadService;
    }

    #region Products

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
            var categories = await _productService.GetProductCategories();
            var brands = await _productService.GetProductBrands();
            ProductViewModel model = new ProductViewModel() { ProductAttributes = attr.ProductAttributes, Brands = brands.Brands, Categories = categories.Categories, Product = new() { Name = "" } };
            return View(model);
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> NewProduct(ProductViewModel productView)
    {
        ModelState.Remove("MainCategory.Name");
        ModelState.Remove("MainCategory.Link");
        if (!ModelState.IsValid)
        {
            return View(productView);
        }

        var res = await _productService.AddProduct(productView);

        if (res.OperationStatus == OperationStatus.Success)
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
        var result = await _productService.RemoveProductById(id);
        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Attributes

    public async Task<IActionResult> Attributes()
    {
        return View(await _productService.GetAllProductAttributes());
    }

    [HttpPost]
    public async Task<ProductAttributeValueViewModel> GetAttribute(int id)
    {
        return await _productService.GetProductAttributeById(id);
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

    [HttpPost]
    public async Task<IActionResult> UpdateAttribute(int id, string name, string link, byte type)
    {
        var result = await _productService.UpdateAttribute(id, name, link, type);
        return PartialView("_ProductAttributePartial", await _productService.GetAllProductAttributes());
    }

    #endregion

    #region Attribute Values

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

        if (res.OperationStatus != OperationStatus.Success)
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
        if (res.OperationStatus != OperationStatus.Success)
        {
            return View();
        }

        return PartialView("_ProductAttributeValuePartial", await _productService.GetProductAttributeById(attributeId));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveAttributeValue(int attributeValueId, int attributeId)
    {
        var result = await _productService.RemoveAttributeValue(attributeValueId);
        if (result.OperationStatus != OperationStatus.Success)
        {
            return View();
        }
        return PartialView("_ProductAttributeValuePartial", await _productService.GetProductAttributeById(attributeId));
    }

    #endregion

    #region Categories

    public async Task<IActionResult> Categories()
    {
        return View(await _productService.GetProductCategories());
    }

    [HttpGet]
    public async Task<ProductCategoriesViewModel> GetCategories()
    {
        return await _productService.GetProductCategories();
    }

    [HttpPost]
    public async Task<ProductCategoriesViewModel> GetCategory(int categoryId)
    {
        return await _productService.GetProductCategories(categoryId);
    }

    [HttpPost]
    public async Task<PartialViewResult> CreateCategory(string name, string link, string description, int parentId)
    {
        ProductCategoriesViewModel viewModel = new ProductCategoriesViewModel()
        {
            Category = new FastKala.Domain.Models.Product.ProductCategory()
            {
                Name = name,
                Link = link,
                Description = description,
                ParentId = parentId
            }
        };
        await _productService.AddProductCategory(viewModel);
        return PartialView("_CategoriesPartial", await _productService.GetProductCategories());
    }

    [HttpPost]
    public async Task<PartialViewResult> UpdateCategory(int id, string name, string link, string description, int parentId)
    {
        ProductCategoriesViewModel viewModel = new ProductCategoriesViewModel()
        {
            Category = new FastKala.Domain.Models.Product.ProductCategory()
            {
                Id = id,
                Name = name,
                Link = link,
                Description = description,
                ParentId = parentId
            }
        };
        await _productService.UpdateProductCategory(viewModel);
        return PartialView("_CategoriesPartial", await _productService.GetProductCategories());
    }

    [HttpPost]
    public async Task<IActionResult> RemoveCategory(int id)
    {
        await _productService.RemoveProductCategory(id);
        return RedirectToAction(nameof(Categories));
    }

    #endregion

    #region Tags

    public async Task<IActionResult> Tags()
    {
        return View(await _productService.GetProductTags());
    }

    [HttpGet]
    public async Task<ProductTagsViewModel> GetTags()
    {
        return await _productService.GetProductTags();
    }

    [HttpPost]
    public async Task<ProductTagsViewModel> GetTag(int tagId)
    {
        return await _productService.GetProductTags(tagId);
    }

    [HttpPost]
    public async Task<PartialViewResult> CreateTag(string name, string link, string description)
    {
        await _productService.AddProductTag(name, link, description);
        return PartialView("_TagsPartial", await _productService.GetProductTags());
    }

    [HttpPost]
    public async Task<PartialViewResult> UpdateTag(int id, string name, string link, string description)
    {
        await _productService.UpdateProductTag(id, name, link, description);
        return PartialView("_TagsPartial", await _productService.GetProductTags());
    }

    [HttpPost]
    public async Task<IActionResult> RemoveTag(int id)
    {
        await _productService.RemoveProductTag(id);
        return RedirectToAction(nameof(Tags));
    }

    #endregion

    #region Brands

    public async Task<IActionResult> Brands()
    {
        return View(await _productService.GetProductBrands());
    }

    [HttpGet]
    public async Task<ProductBrandsViewModel> GetBrands()
    {
        return await _productService.GetProductBrands();
    }

    [HttpPost]
    public async Task<ProductBrandsViewModel> GetBrand(int brandId)
    {
        return await _productService.GetProductBrands(brandId);
    }

    [HttpPost]
    public async Task<PartialViewResult> CreateBrand(string name, string link, string description)
    {
        await _productService.AddProductBrand(name, link, description);
        return PartialView("_BrandsPartial", await _productService.GetProductBrands());
    }

    [HttpPost]
    public async Task<PartialViewResult> UpdateBrand(int id, string name, string link, string description)
    {
        await _productService.UpdateProductBrand(id, name, link, description);
        return PartialView("_BrandsPartial", await _productService.GetProductBrands());
    }

    [HttpPost]
    public async Task<IActionResult> RemoveBrand(int id)
    {
        await _productService.RemoveProductBrand(id);
        return RedirectToAction(nameof(Tags));
    }

    #endregion

    #region Comments

    public async Task<IActionResult> Comments()
    {
        return View(await _productService.GetAllComments());
    }

    [HttpPost]
    public async Task<OperationResult> RemoveComment(int commentId)
    {
        return await _productService.RemoveProductComment(commentId);
    }

    [HttpPost]
    public async Task<OperationResult> VerifyComment(int id)
    {
        return await _productService.VerifyComment(id);
    }

    [HttpPost]
    public async Task<ProductCommentViewModel> GetComment(int id)
    {
        ProductCommentViewModel viewModel = new ProductCommentViewModel();
        var res = await _productService.GetProductComment(id);
        viewModel.ProductComment = res.ProductComments.First();
        return viewModel;
    }

    [HttpPost]
    public async Task<OperationResult> EditComment(int id, string comment)
    {
        return await _productService.UpdateProductComment(id, comment);
    }

    #endregion
}