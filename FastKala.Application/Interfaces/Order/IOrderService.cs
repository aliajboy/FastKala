using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Orders;
using FastKala.Domain.Enums.Orders;
using FastKala.Domain.Models.Order;

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
    public Task<List<IranCities>?> GetIranStatesAndCities();
    public Task<List<IranCities>?> GetIranStates();
    public Task<IranCities?> GetIranStateById(int stateId);
    public Task<List<IranCities>?> GetIranCities(int stateId);
    public Task<List<Orders>?> GetOrders(int count, int page = 1);
    public Task<List<OrderItem>?> GetOrderItems(int orderId);
    public Task<List<int>?> GetOrderItemsIds(int orderId);
    public Task<Orders?> GetOrder(int orderId);
}