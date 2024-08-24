namespace FastKala.Domain.Models.Product;
public record ProductCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Link { get; set; } = null!;
    public string Description { get; set; } = "";
    public int ParentId { get; set; } = 0;

    // Relations
    public List<Product> Products { get; set; } = new();
}
