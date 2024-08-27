using FastKala.Application.Interfaces.Order;
using FastKala.Application.Interfaces.ShopSetting;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.ShopSetting;
using FastKala.Domain.Models.Orders;
using FastKala.Domain.Models.Payment;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Areas.Admin.Controllers;

[Area("Admin")]
public class ShopSettingsController(IShopSettingsService shopSettings, IOrderService orderService) : Controller
{
    private readonly IShopSettingsService _shopSettings = shopSettings;
    private readonly IOrderService _orderService = orderService;

    public async Task<IActionResult> Index()
    {
        var shopSettings = await _shopSettings.GetShopSettings();
        var iranCities = await _orderService.GetIranStatesAndCities();
        if (shopSettings == null && iranCities == null)
        {
            return StatusCode(500);
        }
        return View(new GeneralSiteSettingsViewModel() { ShopSettings = shopSettings, IranCities = iranCities });
    }

    public async Task<IActionResult> Delivery()
    {
        var shippings = await _shopSettings.GetAllShippingTypes();

        return View(new DeliverySettingsViewModel() { Shippings = shippings });
    }

    public async Task<IActionResult> Payment()
    {
        var payments = await _shopSettings.GetAllPayments();
        if (payments != null)
        {
            return View(new PaymentSettingsViewModel()
            {
                PaymentSettings = payments
            });
        }

        return View(new PaymentSettingsViewModel()
        {
            PaymentSettings = new List<PaymentSettings>()
        });
    }

    #region Shippings
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
    #endregion

    #region Payments
    [HttpPost]
    public async Task<OperationResult> UpdateGateway(UpdatePaymentViewModel viewModel)
    {
        return await _shopSettings.UpdateGateway(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddPayment(PaymentSettingsViewModel viewModel)
    {
        var result = await _shopSettings.AddPayment(viewModel.Payment);
        if (result.OperationStatus == Domain.Enums.Global.OperationStatus.Success)
        {
            return RedirectToAction("Payment");
        }
        return BadRequest();
    }

    [HttpDelete]
    public async Task<OperationResult> RemovePayment(int id)
    {
        return await _shopSettings.RemovePayment(id);
    }
    #endregion
}
