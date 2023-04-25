using FastKala.Data;
using FastKala.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastKala.Pages
{
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
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            return Page();
        }
    }
}
