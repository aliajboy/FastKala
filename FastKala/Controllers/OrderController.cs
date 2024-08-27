using FastKala.Application.Data;
using FastKala.Application.Interfaces.OnlinePayment;
using FastKala.Application.Interfaces.Order;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Orders;
using FastKala.Domain.Enums.Global;
using FastKala.Domain.Enums.Orders;
using FastKala.Domain.Models.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly UserManager<FastKalaUser> _userManager;
    private readonly IOrderService _orderService;
    private readonly IZarinPalService _zarinpalService;

    public OrderController(UserManager<FastKalaUser> userManager, IOrderService orderService, IZarinPalService zarinPalService)
    {
        _userManager = userManager;
        _orderService = orderService;
        _zarinpalService = zarinPalService;
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
            var iranStates = await _orderService.GetIranStates();
            if (iranStates != null)
            {
                return View(new CheckoutViewModel() { TotalPrice = totalPrice, IranCities = iranStates });
            }
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
            checkout.Error = "اطلاعات ارسالی نامعتبر است!";
            return View(checkout);
        }
        if (!checkout.AcceptTerms)
        {
            checkout.Error = "لطفا قبل از خرید، شرایط و قوانین استفاده از خدمات فروشگاه را بپذیرید";
            return View(checkout);
        }
        var userid = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userid))
        {
            return Unauthorized();
        }

        long totalPrice = await _orderService.GetTotalOrderPrice(userid);
        long shippingPrice = await _orderService.GetShippingPrice(checkout.ShippingMethod, totalPrice, checkout.TownId, checkout.CityId);

        if ((totalPrice + shippingPrice) != checkout.TotalPrice)
        {
            checkout.Error = "مبلغ ارسال شده نامعتبر است";
            return View(checkout);
        }

        var result = await _zarinpalService.RequestPayment(checkout.TotalPrice, "خرید آنلاین", checkout.Phone, "https://localhost:7002/");
        if (result.errors == null && result.data?.code == 100)
        {
            checkout.Authority = result.data.authority;
            var submitOrderResult = await _orderService.SubmitOrder(checkout, userid, shippingPrice);
            if (submitOrderResult.OperationStatus == OperationStatus.Success)
            {
                return Redirect("https://payment.zarinpal.com/pg/StartPay/" + result.data.authority);
            }
        }

        checkout.Error = "خطا در ارسال به درگاه پرداخت";
        return View(checkout);
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
    public async Task<long> GetShippingPrice(ShippingMethods shipping, int state, int city)
    {
        string userId = _userManager.GetUserId(User) ?? "";
        long shippingPrice = await _orderService.GetShippingPrice(shipping, await _orderService.GetTotalOrderPrice(userId), state, city);

        return shippingPrice;
    }

    [HttpPost]
    public async Task<List<IranCities>?> GetCities(int id)
    {
        var shippingPrice = await _orderService.GetIranCities(id);

        return shippingPrice;
    }
}
