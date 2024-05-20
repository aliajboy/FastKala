using FastKala.Domain.Enums.Products;
using System.ComponentModel;

namespace FastKala.Domain.Models.Product;

public record ProductAttribute
{
    public int Id { get; set; }
    [DisplayName("نام ویژگی")]
    public string Name { get; set; }
    [DisplayName("لینک")]
    public string? Link { get; set; }
    [DisplayName("نوع ویژگی")]
    public AttributeType Type { get; set; }

    // Relations
    public List<ProductAttributeValue> AttributeValues { get; set; } = new List<ProductAttributeValue>();
}

public record ProductAttributeValue
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? value { get; set; }
    public int ProductAttributeId { get; set; }

    // Relations
    public ProductAttribute ProductAttribute { get; set; } = new();
    public List<Product> Product { get; set; } = new();
}