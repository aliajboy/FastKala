using FastKala.Domain.Models.Order;

namespace FastKala.Application.ViewModels.ShopSetting;

public class DeliverySettingsViewModel
{
    public List<ShippingSettings> Shippings { get; set; } = new List<ShippingSettings>();
}