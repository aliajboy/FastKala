namespace FastKala.Domain.Models;
public record ProductImage
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public required string ImageUrl { get; set; }
}