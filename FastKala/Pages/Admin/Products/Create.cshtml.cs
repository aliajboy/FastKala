using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FastKala.Data;
using FastKala.Models;

namespace FastKala.Pages.Admin.Products
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
        public Product Product { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Products == null || Product == null)
            {
                return Page();
            }

            // Add Product Features
            int n = 1;
            while (Request.Form["title" + n].FirstOrDefault() != null)
            {
                Product.ProductFeatures.Add(new ProductFeature()
                {
                    TitleName = Request.Form["title" + n].FirstOrDefault(),
                    Value = Request.Form["value" + n].FirstOrDefault()
                });
                n++;
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
