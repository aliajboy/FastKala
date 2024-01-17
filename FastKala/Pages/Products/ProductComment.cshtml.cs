using FastKala.Application.Interfaces.Product;
using FastKala.Application.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastKala.Pages.Products;

public class ProductCommentModel : PageModel
{
    private readonly IProductService _productService;

    public ProductCommentModel(IProductService productService)
    {
        _productService = productService;
    }

    public ProductViewModel ProductView { get; set; }

    [BindProperty]
    public ProductCommentViewModel CommentView { get; set; }

    public async Task OnGetAsync(int id)
    {
        ProductView = await _productService.GetProductById(id);
    }

    public async Task<IActionResult> OnPost()
    {
        var a = await _productService.AddProductComment(CommentView.ProductComment);
        return RedirectToPage("/Products/Product", new { id = CommentView.ProductComment.ProductId });
    }
}