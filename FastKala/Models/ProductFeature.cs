namespace FastKala.Models;

public class ProductFeature
{
    public int Id { get; set; }
    public string? TitleName { get; set; }
    public string? Value { get; set; }

    // Relations
    public Product Product { get; set; }
}