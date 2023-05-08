using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            // Add Product Pros
            n = 1;
            while (Request.Form["pros" + n].FirstOrDefault() != null)
            {
                Product.ProductPros.Add(new ProductPros()
                {
                    Text = Request.Form["pros" + n].FirstOrDefault()
                });
                n++;
            }
            // Add Product Cons
            n = 1;
            while (Request.Form["cons" + n].FirstOrDefault() != null)
            {
                Product.ProductCons.Add(new ProductCons()
                {
                    Text = Request.Form["cons" + n].FirstOrDefault()
                });
                n++;
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
