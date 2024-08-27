using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.ShopSetting;
using FastKala.Domain.Models.Orders;
using FastKala.Domain.Models.Payment;
using FastKala.Domain.Models.Settings;

namespace FastKala.Application.Interfaces.ShopSetting;

public interface IShopSettingsService
{
    public Task<List<ShippingSettings>> GetAllShippingTypes();

    public Task<List<ShippingSettings>> GetActiveShippingTypes();

    public Task<OperationResult> ToggleShipping(int id);

    public Task<ShippingSettings?> GetShippingById(int id);

    public Task<OperationResult> UpdateShipping(UpdateDeliveryViewModel updateDelivery);

    public Task<List<PaymentSettings>?> GetAllPayments();

    public Task<OperationResult> UpdateGateway(UpdatePaymentViewModel updatePayment);

    public Task<OperationResult> AddPayment(PaymentSettings payment);

    public Task<OperationResult> RemovePayment(int paymentId);

    public Task<ShopSettings?> GetShopSettings();
}