namespace FastKala.Domain.Models.Product;
public record ProductTag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public string Description { get; set; } = "";
}