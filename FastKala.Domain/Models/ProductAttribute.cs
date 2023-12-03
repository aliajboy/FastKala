using System.ComponentModel;

namespace FastKala.Domain.Models;

public class ProductAttribute
{
    public int Id { get; set; }
    [DisplayName("نام ویژگی")]
    public string Name { get; set; }
    [DisplayName("لینک")]
    public string? Link { get; set; }
    [DisplayName("نوع ویژگی")]
    public string? Type { get; set; }

    // Relations
    public List<ProductAttributeValues> AttributeValues { get; set; } = new List<ProductAttributeValues>();
}

public class ProductAttributeValues
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? value { get; set; }

    // Relations
    public ProductAttribute? ProductAttribute { get; set; }
    public int ProductAttributeId { get; set; }
}