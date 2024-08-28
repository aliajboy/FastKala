using FastKala.Application.Interfaces.Order;
using FastKala.Application.Interfaces.Product;
using FastKala.Application.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastKala.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;

    public OrdersController(IOrderService orderService, IProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrders(20, 1);
        if (orders == null)
        {
            return View(new OrderListViewModel());
        }
        return View(new OrderListViewModel() { Orders = orders });
    }

    public IActionResult AddOrder()
    {
        return View();
    }

    public async Task<IActionResult> EditOrder(int id)
    {
        var order = await _orderService.GetOrder(id);
        var orderItems = await _orderService.GetOrderItems(id);

        if (order == null || orderItems == null)
        {
            return NotFound();
        }
        var viewModel = new OrderViewModel();
        viewModel.Order = order;
        viewModel.OrderItems = orderItems;
        var iranCities = await _orderService.GetIranStatesAndCities();
        if (iranCities != null)
        {
            viewModel.IranCities = iranCities;
        }

        foreach (var item in orderItems)
        {
            var product = await _productService.GetProductById(item.ProductId);
            viewModel.Products.Add(product.Product);
        }

        return View(viewModel);
    }
}