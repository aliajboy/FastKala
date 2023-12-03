using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FastKala.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Pages.Admin.ProductsAttributes.Attributes
{
    public class IndexModel : PageModel
    {
        private readonly FastKala.Data.FsContext _context;

        public IndexModel(FastKala.Data.FsContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ProductAttributeValues ProductAttributeValue { get; set; } = default!;
        public IList<ProductAttributeValues> ProductAttributeValues { get; set; } = default!;
        public int Id { get; set; }
        public string Title { get; set; }
        public async Task OnGetAsync(int id)
        {
            if (_context.ProductAttributeValues != null)
            {
                ProductAttributeValues = await _context.ProductAttributeValues.Where(x => x.ProductAttributeId == id).ToListAsync();
            }
            Id = id;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null || _context.ProductAttributeValues == null)
            {
                return NotFound();
            }
            var productattributevalue = await _context.ProductAttributeValues.FindAsync(id);
            
            if (productattributevalue != null)
            {
                Id = productattributevalue.ProductAttributeId;
                ProductAttributeValue = productattributevalue;
                _context.ProductAttributeValues.Remove(ProductAttributeValue);
                await _context.SaveChangesAsync();
            }
            return Redirect("/Admin/ProductsAttributes/Attributes/?id=" + Id);
        }

        public async Task<IActionResult> OnPostAddAsync(int id)
        {
            ProductAttributeValue.ProductAttributeId = id;
            if (!ModelState.IsValid || _context.ProductAttributeValues == null || ProductAttributeValue == null)
            {
                return Page();
            }

            _context.ProductAttributeValues.Add(ProductAttributeValue);
            await _context.SaveChangesAsync();

            return Redirect("/Admin/ProductsAttributes/Attributes/?id=" + id);
        }
    }
}