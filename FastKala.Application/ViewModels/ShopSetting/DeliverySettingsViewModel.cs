using FastKala.Domain.Models.Orders;

namespace FastKala.Application.ViewModels.ShopSetting;

public class DeliverySettingsViewModel
{
    public List<ShippingSettings> Shippings { get; set; } = new List<ShippingSettings>();
}