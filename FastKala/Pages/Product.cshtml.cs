using FastKala.Data;
using FastKala.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FastKala.Pages;

public class ProductModel : PageModel
{
    private readonly FsContext _context;
    public ProductModel(FsContext context)
    {
        _context = context;
    }
    [BindProperty]
    public Product Product { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Products == null)
        {
            return NotFound();
        }
        var product = await _context.Products.Include(x => x.ProductFeatures).Include(y => y.ProductPros).Include(e => e.ProductCons).FirstAsync(x => x.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        Product = product;
        return Page();
    }
}