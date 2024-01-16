using FastKala.Application.Interfaces.Product;
using FastKala.Application.ViewModels.Products;
using FastKala.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastKala.Pages;

public class ProductModel : PageModel
{
    private readonly IProductService _productService;
    public ProductModel(IProductService productService)
    {
        _productService = productService;
    }

    public ProductViewModel ProductView { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        ProductView = await _productService.GetProductById(id);
        if (ProductView.Categories.Any())
        {
            ProductView.MainCategory = ProductView.Categories.First(x => x.Id == ProductView.Product.MainCategoryId);
            ProductView.CategoryOrder.Add(ProductView.MainCategory);
            int parentId = ProductView.MainCategory.ParentId;
            while (parentId != 0)
            {
                var pCategory = ProductView.Categories.First(x => x.Id == parentId);
                ProductView.CategoryOrder.Add(pCategory);
                parentId = pCategory.ParentId;
            }
        }
        if (ProductView.Product.Name == "پیش فرض" || ProductView.Product.Status != Domain.Enums.ProductStatus.Published)
        {
            return NotFound();
        }

        return Page();
    }
}