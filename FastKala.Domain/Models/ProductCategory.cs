namespace FastKala.Domain.Models;
public class ProductCategory
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Link { get; set; }
    public string Description { get; set; } = "";

    // Relations
    public List<Product> Products { get; set; } = new();
}
