using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Orders;

namespace FastKala.Application.Interfaces.Order;

public interface IOrderService
{
    public Task<OperationResult> AddToCard(int productId, int quantity, string userId);
    public Task<List<CartItemsViewModel>> GetCartItems(string userId);
    public Task<OperationResult> ChangeCartValue(int productId, int quantity, string userId);
    public Task<OperationResult> RemoveCartItem(int productId, string userId);
    public Task<OperationResult> RemoveAllCartItems(string userId);
    public Task<long> GetTotalOrderPrice(string userId);
}