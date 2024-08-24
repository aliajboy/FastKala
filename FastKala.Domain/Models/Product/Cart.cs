namespace FastKala.Domain.Models.Product;
public class Cart
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string CustomerId { get; set; } = null!;

    // Relation
    public Product Product { get; set; } = new Product();
}