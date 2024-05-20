using FastKala.Application.Interfaces.Product;
using FastKala.Domain.Models.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastKala.Pages;
public class IndexModel : PageModel
{
    private readonly IProductService _productService;

    public List<Product> Products { get; set; }

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public async Task OnGetAsync()
    {
        var res = await _productService.GetAllProducts();
        Products = res.Products.OrderByDescending(p => p.LastChangeTime).ToList();
    }
}
