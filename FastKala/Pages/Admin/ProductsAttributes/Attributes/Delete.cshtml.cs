using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FastKala.Data;
using FastKala.Models;

namespace FastKala.Pages.Admin.ProductsAttributes.Attributes
{
    public class DeleteModel : PageModel
    {
        private readonly FastKala.Data.FsContext _context;

        public DeleteModel(FastKala.Data.FsContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ProductAttributeValues ProductAttributeValues { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ProductAttributeValues == null)
            {
                return NotFound();
            }

            var productattributevalues = await _context.ProductAttributeValues.FirstOrDefaultAsync(m => m.Id == id);

            if (productattributevalues == null)
            {
                return NotFound();
            }
            else 
            {
                ProductAttributeValues = productattributevalues;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ProductAttributeValues == null)
            {
                return NotFound();
            }
            var productattributevalues = await _context.ProductAttributeValues.FindAsync(id);

            if (productattributevalues != null)
            {
                ProductAttributeValues = productattributevalues;
                _context.ProductAttributeValues.Remove(ProductAttributeValues);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
