using FastKala.Domain.Models.Product;

namespace FastKala.Application.ViewModels.Products;

public class ProductCommentListViewModel
{
    public List<ProductComment> ProductComments { get; set; } = new List<ProductComment>();
}