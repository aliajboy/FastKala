﻿using Microsoft.AspNetCore.Mvc;

namespace FastKala.Areas.Admin.Controllers;

[Area("Admin")]
public class OrdersController : Controller
{
    public OrdersController()
    {
        
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddOrders()
    {
        return View();
    }
}