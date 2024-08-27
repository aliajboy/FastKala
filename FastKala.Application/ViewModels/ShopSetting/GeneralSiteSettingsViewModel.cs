using FastKala.Domain.Models.Orders;
using FastKala.Domain.Models.Settings;

namespace FastKala.Application.ViewModels.ShopSetting;

public class GeneralSiteSettingsViewModel
{
    public ShopSettings ShopSettings { get; set; } = new ShopSettings();
    public List<IranCities> IranCities { get; set; } = new List<IranCities>();
}