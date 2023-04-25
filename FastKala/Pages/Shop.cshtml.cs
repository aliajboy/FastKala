using FastKala.Data;
using FastKala.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FastKala.Pages
{
    public class ShopModel : PageModel
    {
        private FsContext _context;
        [BindProperty]
        public List<Product> Products { get; set; }
        public ShopModel()
        {
            _context = new FsContext();
        }
        public void OnGet()
        {
            Products = _context.Products.ToList();
        }
    }
}
