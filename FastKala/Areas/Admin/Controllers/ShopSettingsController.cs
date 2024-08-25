using FastKala.Application.Interfaces.OnlinePayment;
using FastKala.Application.Interfaces.ShopSettings;
using FastKala.Application.Services.OnlinePayment;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.ShopSettings;
using FastKala.Domain.Models.Orders;
using FastKala.Domain.Models.Payment;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Areas.Admin.Controllers;

[Area("Admin")]
public class ShopSettingsController(IShopSettings shopSettings, IPaymentService paymentService) : Controller
{
    private readonly IShopSettings _shopSettings = shopSettings;
    private readonly IPaymentService _paymentService = paymentService;

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Delivery()
    {
        var shippings = await _shopSettings.GetAllShippingTypes();

        return View(new DeliverySettingsViewModel() { Shippings = shippings });
    }

    public async Task<IActionResult> Payment()
    {
        var payments = await _paymentService.GetAllPayments();
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
        return await _paymentService.UpdateGateway(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddPayment(PaymentSettingsViewModel viewModel)
    {
        var result = await _paymentService.AddPayment(viewModel.Payment);
        if (result.OperationStatus == Domain.Enums.Global.OperationStatus.Success)
        {
            return View("Payment");
        }
        return View(viewModel);
    }

    [HttpDelete]
    public async Task<OperationResult> RemovePayment(int id)
    {
        return await _paymentService.RemovePayment(id);
    }
    #endregion
}
