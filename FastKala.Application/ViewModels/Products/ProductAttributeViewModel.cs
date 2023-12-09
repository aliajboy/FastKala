using FastKala.Domain.Models;

namespace FastKala.Application.ViewModels.Products;
public class ProductAttributeViewModel
{
    public ProductAttribute ProductAttribute { get; set; } = new ProductAttribute();
    public ProductAttributeValues ProductAttributeValues { get; set; } = new ProductAttributeValues();
}