using FastKala.Application.Data;
using FastKala.Application.Interfaces.Order;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Orders;
using FastKala.Domain.Enums.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Controllers;
public class OrderController : Controller
{
    private readonly UserManager<FastKalaUser> _userManager;
    private readonly SignInManager<FastKalaUser> _signinManager;
    private readonly IOrderService _orderService;
    public OrderController(UserManager<FastKalaUser> userManager, SignInManager<FastKalaUser> signInManager, IOrderService orderService)
    {
        _userManager = userManager;
        _signinManager = signInManager;
        _orderService = orderService;
    }

    [Route("Cart")]
    public async Task<IActionResult> Cart()
    {
        List<CartItemsViewModel> cartItems = new List<CartItemsViewModel>();
        if (_signinManager.IsSignedIn(User))
        {
            var user = await _userManager.GetUserAsync(User);
            cartItems = await _orderService.GetCartItems(user?.Id);
        }
        return View(cartItems);
    }

    [HttpPost]
    [Authorize]
    [Route("AddToCard")]
    public async Task<OperationResult> AddToCard(int productId, int quantity)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user != null && productId > 0 && quantity > 0)
        {
            return await _orderService.AddToCard(productId, quantity, user.Id);
        }

        return new OperationResult() { OperationStatus = OperationStatus.Fail, Message = "AddToCart Controller Error" };
    }
}
