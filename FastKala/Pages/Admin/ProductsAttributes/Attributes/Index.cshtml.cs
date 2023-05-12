using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FastKala.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;

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
        public string Title { get; set; }
        [BindProperty]
        public int Id { get; set; }
        public async Task OnGetAsync(int id)
        {
            var title = await _context.ProductAttributes.FindAsync(id);
            Title = title.Name;
            Id = id;
            if (_context.ProductAttributeValues != null)
            {
                ProductAttributeValues = await _context.ProductAttributeValues.Where(x => x.ProductAttributeId == id).ToListAsync();
            }
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
                ProductAttributeValue = productattributevalue;
                _context.ProductAttributeValues.Remove(ProductAttributeValue);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Admin/ProductsAttributes/Attributes/index");
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            ProductAttributeValue.ProductAttributeId = Id;
            //if (!ModelState.IsValid || _context.ProductAttributeValues == null || ProductAttributeValue == null)
            //{
            //    return Page();
            //}

            _context.ProductAttributeValues.Add(ProductAttributeValue);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/ProductsAttributes/Attributes/index");
        }
    }
}