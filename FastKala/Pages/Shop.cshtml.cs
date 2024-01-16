using FastKala.Application.Interfaces.Product;
using FastKala.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastKala.Pages;

public class ShopModel : PageModel
{
    private readonly IProductService _productService;
    [BindProperty]
    public List<Product> Products { get; set; }
    public ShopModel(IProductService productService)
    {
        _productService = productService;
    }

    public async Task OnGetAsync()
    {
        var products = await _productService.GetAllProducts();
        Products = products.Products;
    }
}
