namespace FastKala.Domain.Models;
public class ProductCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public string Description { get; set; } = "";
    public int ParentId { get; set; } = 0;

    // Relations
    public List<Product> Products { get; set; } = new();
}
