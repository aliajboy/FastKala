using FastKala.Application.Interfaces.Product;
using FastKala.Domain.Enums.Products;
using FastKala.Domain.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastKala.Pages.Products;

public class ProductsModel : PageModel
{
    private readonly IProductService _productService;
    [BindProperty]
    public List<Product> Products { get; set; }
    public ProductsModel(IProductService productService)
    {
        _productService = productService;
    }

    public async Task OnGetAsync()
    {
        var products = await _productService.GetAllProducts(ProductStatus.Published);
        Products = products.Products;
    }
}
