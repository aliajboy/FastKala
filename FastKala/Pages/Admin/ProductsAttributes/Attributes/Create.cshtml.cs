using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FastKala.Data;
using FastKala.Models;

namespace FastKala.Pages.Admin.ProductsAttributes.Attributes
{
    public class CreateModel : PageModel
    {
        private readonly FastKala.Data.FsContext _context;

        public CreateModel(FastKala.Data.FsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProductAttributeValues ProductAttributeValues { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ProductAttributeValues == null || ProductAttributeValues == null)
            {
                return Page();
            }

            _context.ProductAttributeValues.Add(ProductAttributeValues);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
