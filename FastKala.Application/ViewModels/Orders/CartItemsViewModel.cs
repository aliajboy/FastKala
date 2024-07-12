namespace FastKala.Application.ViewModels.Orders;
public class CartItemsViewModel
{
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public string? ProductAttribute { get; set; }
    public int Quantity { get; set; }
    public long Price { get; set; }
    public long TotalPrice
    {
        get
        {
            return Price * Quantity;
        }
    }
}