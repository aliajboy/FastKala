namespace FastKala.Domain.Models.Product;

public record ProductFeature
{
    public int Id { get; set; }
    public required string TitleName { get; set; }
    public required string Value { get; set; }
    public int ProductId { get; set; }
}