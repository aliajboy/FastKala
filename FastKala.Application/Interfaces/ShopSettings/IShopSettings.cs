using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.ShopSettings;
using FastKala.Domain.Models.Orders;

namespace FastKala.Application.Interfaces.ShopSettings;

public interface IShopSettings
{
    public Task<List<ShippingSettings>> GetAllShippingTypes();

    public Task<List<ShippingSettings>> GetActiveShippingTypes();

    public Task<OperationResult> ToggleShipping(int id);

    public Task<ShippingSettings?> GetShippingById(int id);

    public Task<OperationResult> UpdateShipping(UpdateDeliveryViewModel updateDelivery);
}