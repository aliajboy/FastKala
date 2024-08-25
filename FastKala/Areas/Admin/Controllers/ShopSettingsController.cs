using FastKala.Application.Interfaces.ShopSettings;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.ShopSettings;
using FastKala.Domain.Models.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Areas.Admin.Controllers;

[Area("Admin")]
public class ShopSettingsController(IShopSettings shopSettings) : Controller
{
    private readonly IShopSettings _shopSettings = shopSettings;

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Delivery()
    {
        var shippings = await _shopSettings.GetAllShippingTypes();

        return View(new DeliverySettingsViewModel() { Shippings = shippings });
    }

    public IActionResult Payment()
    {
        return View();
    }

    [HttpPost]
    public async Task<OperationResult> UpdateShipping(UpdateDeliveryViewModel viewModel)
    {
        return await _shopSettings.UpdateShipping(viewModel);
    }

    [HttpPost]
    public async Task<ShippingSettings?> GetShippingData(int id)
    {
        return await _shopSettings.GetShippingById(id);
    }

    [HttpPost]
    public async Task<OperationResult> ToggleShipping(int id)
    {
        return await _shopSettings.ToggleShipping(id);
    }
}
