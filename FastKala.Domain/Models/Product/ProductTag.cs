namespace FastKala.Domain.Models.Product;

public record ProductTag
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Link { get; set; } = null!;
    public string Description { get; set; } = "";
}