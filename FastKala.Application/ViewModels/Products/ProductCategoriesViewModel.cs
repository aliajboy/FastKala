using FastKala.Domain.Models;

namespace FastKala.Application.ViewModels.Products;
public class ProductCategoriesViewModel
{
    public List<ProductCategory> Categories { get; set; } = new();
    public ProductCategory Category { get; set; } = new();
}