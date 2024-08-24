using FastKala.Domain.Models.Product;

namespace FastKala.Application.ViewModels.Products;
public class AttributeValuesResponse
{
    public List<ProductAttributeValue> results { get; set; } = new List<ProductAttributeValue>();
}
