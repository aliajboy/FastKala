namespace FastKala.Domain.Models.Product;
public record ProductAttributeRelation
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AttributeValueId { get; set; }

    // relation
    public Product? Product { get; set; }
    public ProductAttributeValues? ProductAttributeValue { get; set; }
}