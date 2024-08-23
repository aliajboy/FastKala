using FastKala.Application.Data;
using FastKala.Application.Interfaces.Order;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Orders;
using FastKala.Domain.Enums.Global;
using FastKala.Domain.Enums.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Controllers;

[Authorize]
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

        string? user = _userManager.GetUserId(User);
        cartItems = await _orderService.GetCartItems(user ?? "");

        return View(cartItems);
    }

    [Route("Checkout")]
    public async Task<IActionResult> Checkout()
    {
        string? user = _userManager.GetUserId(User);

        if (user != null)
        {
            long totalPrice = await _orderService.GetTotalOrderPrice(user);
            return View(new CheckoutViewModel() { TotalPrice = totalPrice });
        }
        return Unauthorized();
    }

    [Route("Checkout")]
    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutViewModel checkout)
    {
        if (!ModelState.IsValid)
        {
            return View(checkout);
        }
        var userid = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userid))
        {
            return Unauthorized();
        }

        long totalPrice = await _orderService.GetTotalOrderPrice(userid);
        long shippingPrice = await _orderService.GetShippingPrice(checkout.ShippingMethod, totalPrice);

        if ((totalPrice + shippingPrice) != checkout.TotalPrice)
        {
            return View(checkout);
        }

        return RedirectToAction("Index","Home");
    }

    [Route("ChangeCartValue")]
    [HttpPost]
    public async Task<OperationResult> ChangeCartValue(int quantity, int productId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            return await _orderService.ChangeCartValue(productId, quantity, user.Id);
        }

        return new OperationResult() { OperationStatus = OperationStatus.Fail };
    }

    [HttpPost]
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

    [Route("RemoveCartItem")]
    [HttpPost]
    public async Task<OperationResult> RemoveCartItem(int productId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            return await _orderService.RemoveCartItem(productId, user.Id);
        }

        return new OperationResult() { OperationStatus = OperationStatus.Fail };
    }

    [HttpPost]
    public async Task<long> GetShippingPrice(ShippingMethods shipping)
    {
        var userId = _userManager.GetUserId(User);
        long shippingPrice = await _orderService.GetShippingPrice(shipping, await _orderService.GetTotalOrderPrice(userId));

        return shippingPrice;
    }
}
