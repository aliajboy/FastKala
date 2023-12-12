using FastKala.Domain.Models;

namespace FastKala.Application.ViewModels.Products;
public class ProductAttributeValueViewModel
{
    public List<ProductAttributeValues> AttributeValues { get; set; } = new();
    public ProductAttributeValues? AttributeValue { get; set; }
    public ProductAttribute Attribute { get; set; } = new();
}