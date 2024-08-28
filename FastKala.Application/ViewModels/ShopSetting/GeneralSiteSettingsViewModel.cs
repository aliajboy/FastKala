using FastKala.Domain.Models.Order;
using FastKala.Domain.Models.Settings;
using Microsoft.AspNetCore.Http;

namespace FastKala.Application.ViewModels.ShopSetting;

public class GeneralSiteSettingsViewModel
{
    public ShopSettings ShopSettings { get; set; } = new ShopSettings();
    public List<IranCities> IranCities { get; set; } = new List<IranCities>();
    public IFormFile? LogoImage { get; set; }
}