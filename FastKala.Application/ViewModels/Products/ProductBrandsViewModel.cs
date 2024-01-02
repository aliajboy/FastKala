using FastKala.Domain.Models;

namespace FastKala.Application.ViewModels.Products;
public class ProductBrandsViewModel
{
    public List<ProductBrand> Brands { get; set; } = new();
    public ProductBrand Brand { get; set; } = new();
}