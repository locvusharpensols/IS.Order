using IS.Order.Application.Contracts;
using IS.Order.Application.Features.Orders;
using Microsoft.AspNetCore.Mvc;

namespace IS.Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpPost]
    public async Task<String> PlaceOrder(OrderPlacementRequestDto request)
    {
        await _orderService.CreateOrderAsync(request, CancellationToken.None);
        return "done";
    }
}