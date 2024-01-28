using FastKala.Application.Interfaces.Product;
using FastKala.Application.ViewModels.Products;
using FastKala.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastKala.Pages.Products;

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

        if (ProductView.Product.Name == "پیش فرض" || ProductView.Product.Status != Domain.Enums.ProductStatus.Published)
        {
            return NotFound();
        }

        return Page();
    }
}