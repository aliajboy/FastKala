using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Orders;
using FastKala.Domain.Enums.Orders;
using FastKala.Domain.Models.Orders;

namespace FastKala.Application.Interfaces.Order;

public interface IOrderService
{
    public Task<OperationResult> AddToCard(int productId, int quantity, string userId);
    public Task<List<CartItemsViewModel>> GetCartItems(string userId);
    public Task<OperationResult> ChangeCartValue(int productId, int quantity, string userId);
    public Task<OperationResult> RemoveCartItem(int productId, string userId);
    public Task<OperationResult> RemoveAllCartItems(string userId);
    public Task<long> GetTotalOrderPrice(string userId);
    public Task<OperationResult> SubmitOrder(CheckoutViewModel checkout, string userId, long shippingPrice);
    public Task<long> GetShippingPrice(ShippingMethods shipping, long orderPrice, int state, int city);
    public Task<List<IranCities>?> GetIranStates();
    public Task<List<IranCities>?> GetStateCities(int stateId);
}