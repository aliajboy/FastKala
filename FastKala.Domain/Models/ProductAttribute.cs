using FastKala.Domain.Enums;
using System.ComponentModel;

namespace FastKala.Domain.Models;

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
    public List<ProductAttributeValues> AttributeValues { get; set; } = new List<ProductAttributeValues>();
}

public record ProductAttributeValues
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? value { get; set; }

    // Relations
    public ProductAttribute ProductAttribute { get; set; } = new();
    public int ProductAttributeId { get; set; }
}