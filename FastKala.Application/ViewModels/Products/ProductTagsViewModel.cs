using FastKala.Domain.Models.Product;

namespace FastKala.Application.ViewModels.Products;
public class ProductTagsViewModel
{
    public List<ProductTag> ProductTags { get; set; } = new();
    public ProductTag ProductTag { get; set; } = new();
}