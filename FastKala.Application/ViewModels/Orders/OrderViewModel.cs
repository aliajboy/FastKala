using FastKala.Domain.Models.Order;
using FastKala.Domain.Models.Product;

namespace FastKala.Application.ViewModels.Orders;

public class OrderViewModel
{
    public Domain.Models.Order.Orders Order { get; set; } = new Domain.Models.Order.Orders();
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public List<Product> Products { get; set; } = new List<Product>();
    public List<IranCities> IranCities { get; set; } = new List<IranCities>();
}