﻿using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO;
using WebStore.Interfaces.Services;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers;

[ApiController]
[Route(WebAPIAddresses.V1.Orders)]
public class OrdersApiController : ControllerBase
{


    private readonly IOrderService _OrderService;
    private readonly ILogger<OrdersApiController> _Logger;

    public OrdersApiController(IOrderService OrderService, ILogger<OrdersApiController> Logger)
    {
        _OrderService = OrderService;
        _Logger = Logger;
    }

    [HttpGet("user/{UserName}")]
    public async Task<IActionResult> GetUserOrders(string UserName)
    {
        var orders = await _OrderService.GetUserOrdersAsync(UserName);
        return Ok(orders.ToDTO());
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetOrderById(int Id)
    {
        var order = await _OrderService.GetOrderByIdAsync(Id);
        if (order is null)
            return NotFound();
        return Ok(order.ToDTO());        
    }   

    [HttpPost("{UserName}")]
    public async Task<IActionResult> CreateOrder(string UserName, [FromBody] CreateOrderDTO Model)
    {
        var cart = Model.Items.ToCartView();
        var order_model = Model.Order;
        var order = await _OrderService.CreateOrderAsync(UserName, cart, order_model);
        return CreatedAtAction(nameof(GetOrderById), new { order.Id }, order.ToDTO());

    }
}
