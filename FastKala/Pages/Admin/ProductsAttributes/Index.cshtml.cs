using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FastKala.Domain.Models;

namespace FastKala.Pages.Admin.PoductsAttributes
{
    public class IndexModel : PageModel
    {
        private readonly FastKala.Data.FsContext _context;

        public IndexModel(FastKala.Data.FsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductAttribute ProductAttribute { get; set; }
        public IList<ProductAttribute> ProductAttributes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ProductAttributes != null)
            {
                ProductAttributes = await _context.ProductAttributes.Include(x => x.AttributeValues).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAddAttributeAsync()
        {
            if (!ModelState.IsValid || _context.ProductAttributes == null || ProductAttribute == null)
            {
                return Page();
            }

            _context.ProductAttributes.Add(ProductAttribute);
            await _context.SaveChangesAsync();

            return RedirectToPage("/admin/productsattributes/index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null || _context.ProductAttributes == null)
            {
                return NotFound();
            }
            var productattribute = await _context.ProductAttributes.FindAsync(id);

            if (productattribute != null)
            {
                ProductAttribute = productattribute;
                _context.ProductAttributes.Remove(ProductAttribute);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/admin/productsattributes/index");
        }
    }
}
