using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Orders;

namespace FastKala.Application.Interfaces.Order;

public interface IOrderService
{
    Task<OperationResult> AddToCard(int productId, int quantity, string userId);
    Task<List<CartItemsViewModel>> GetCartItems(string userId);
}