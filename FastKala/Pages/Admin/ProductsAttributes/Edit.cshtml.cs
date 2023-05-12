using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastKala.Data;
using FastKala.Models;

namespace FastKala.Pages.Admin.PoductsAttributes
{
    public class EditModel : PageModel
    {
        private readonly FastKala.Data.FsContext _context;

        public EditModel(FastKala.Data.FsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductAttribute ProductAttribute { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ProductAttributes == null)
            {
                return NotFound();
            }

            var productattribute =  await _context.ProductAttributes.FirstOrDefaultAsync(m => m.Id == id);
            if (productattribute == null)
            {
                return NotFound();
            }
            ProductAttribute = productattribute;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProductAttribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAttributeExists(ProductAttribute.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductAttributeExists(int id)
        {
          return (_context.ProductAttributes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
