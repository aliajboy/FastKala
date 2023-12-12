using FastKala.Application.Interfaces;
using FastKala.Data;
using FastKala.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FastKala.Pages;

public class ProductModel : PageModel
{
    private readonly FsContext _context;
    private readonly IProductService _productService;
    public ProductModel(FsContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }

    public Product Product { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await _productService.GetProductById(id);
        if (product.Product.Name == "پیش فرض")
        {
            return NotFound();
        }
        Product = product.Product;

        return Page();
    }
}