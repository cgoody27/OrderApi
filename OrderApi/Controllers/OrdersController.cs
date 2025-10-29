using Microsoft.AspNetCore.Mvc;
using OrderApi.Services;

namespace OrderApi.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderSummary(string orderId)
    {
        try
        {
            var orderSummary = await _orderService.GetOrderSummaryAsync(orderId);
            if (orderSummary == null)
                return NotFound();

            return Ok(orderSummary);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving summary for order {orderId}.");

            // using 500 for now. Different errors will create different status codes
            return StatusCode(500, $"Error retrieving summary for order {orderId}.");
        }
    }

    [HttpGet("{orderId}/history")]
    public async Task<IActionResult> GetOrderStatusHistory(string orderId)
    {
        try
        {
            var orderStatusHistory = await _orderService.GetOrderStatusHistoryAsync(orderId);
            if (orderStatusHistory == null)
                return NotFound();

            return Ok(orderStatusHistory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving history for order {orderId}.");

            // using 500 for now. Different errors will create different status codes
            return StatusCode(500, $"Error retrieving history for order {orderId}.");
        }
    }
}
