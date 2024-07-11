namespace FastKala.Domain.Models.Product;
public class Cart
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int CustomerId { get; set; }

    // Relation
    public Product Product { get; set; } = new Product();
    // TODO: Customer Model Relation
}