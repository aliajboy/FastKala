using FastKala.Domain.Models.Product;

namespace FastKala.Application.ViewModels.Products;
public class ProductAtrributesListViewModel
{
    public List<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
    public List<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();
}