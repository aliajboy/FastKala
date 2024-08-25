using FastKala.Domain.Models.Orders;

namespace FastKala.Application.ViewModels.ShopSettings;

public class DeliverySettingsViewModel
{
    public List<ShippingSettings> Shippings { get; set; } = new List<ShippingSettings>();
}