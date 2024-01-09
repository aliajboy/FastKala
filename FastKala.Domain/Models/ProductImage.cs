namespace FastKala.Domain.Models;
public record ProductImage
{
    public int Id { get; set; }
    public required string ImageName { get; set; }
    public int ProductId { get; set; }
}