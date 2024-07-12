using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Orders;

namespace FastKala.Application.Interfaces.Order;

public interface IOrderService
{
    Task<OperationResult> AddToCard(int productId, int quantity, string userId);
    Task<List<CartItemsViewModel>> GetCartItems(string userId);
    Task<OperationResult> ChangeCartValue(int productId, int quantity, string userId);
    Task<OperationResult> RemoveCartItem(int productId, string userId);
    Task<OperationResult> RemoveAllCartItems(string userId);
}