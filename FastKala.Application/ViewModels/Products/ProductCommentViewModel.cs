using FastKala.Domain.Models.Product;

namespace FastKala.Application.ViewModels.Products;

public class ProductCommentViewModel
{
    public ProductComment ProductComment { get; set; } = new();

    public List<string> Advantages { get; set; } = new();
    public List<string> DisAdvantages { get; set; } = new();
}