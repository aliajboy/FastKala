using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastKala.Data;
using FastKala.Domain.Models;

namespace FastKala.Pages.Admin.ProductsAttributes.Attributes
{
    public class EditModel : PageModel
    {
        private readonly FastKala.Data.FsContext _context;

        public EditModel(FastKala.Data.FsContext context)
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

            var productattributevalues =  await _context.ProductAttributeValues.FirstOrDefaultAsync(m => m.Id == id);
            if (productattributevalues == null)
            {
                return NotFound();
            }
            ProductAttributeValues = productattributevalues;
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

            _context.Attach(ProductAttributeValues).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAttributeValuesExists(ProductAttributeValues.Id))
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

        private bool ProductAttributeValuesExists(int id)
        {
          return (_context.ProductAttributeValues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
