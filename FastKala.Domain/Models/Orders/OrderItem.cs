namespace FastKala.Domain.Models.Orders;

public record OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public long Fee { get; set; }
    public int OrderId { get; set; }
}