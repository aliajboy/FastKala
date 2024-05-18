using FastKala.Domain.Models.Product;

namespace FastKala.Application.ViewModels.Products;
public class ProductAttributeValueViewModel
{
    public List<ProductAttributeValue> AttributeValues { get; set; } = new();
    public ProductAttributeValue? AttributeValue { get; set; }
    public ProductAttribute Attribute { get; set; } = new();
}