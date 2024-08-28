using FastKala.Domain.Models.Order;

namespace FastKala.Application.ViewModels.Orders;

public class OrderListViewModel
{
    public List<Domain.Models.Order.Orders> Orders { get; set; } = new List<Domain.Models.Order.Orders>();
}