namespace FastKala.Domain.Models;

public record ProductFeature
{
    public int Id { get; set; }
    public required string TitleName { get; set; }
    public required string Value { get; set; }

    // Relations
    public Product Product { get; set; }
}