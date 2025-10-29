using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Models;

namespace OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrdersDbContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(OrdersDbContext context, ILogger<OrderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Order> GetOrderSummaryAsync(string orderId)
        {
            try
            {
                //var order = await _context.Orders
                //    .AsNoTracking()
                //    .FirstOrDefaultAsync(o => o.OrderId == orderId);

                //if (order == null)
                //{
                //    _logger.LogWarning($"Order {orderId} not found.");
                //}

                //return order;
                if (orderId == "1")
                {
                    return new Order
                    {
                        OrderId = "1",
                        CustomerId = "cust1",
                        Status = "Processing",
                        TotalAmount = 134.33m
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to return summary for order {orderId}.");
                throw;
            }
        }

        public async Task<List<OrderStatusHistory>> GetOrderStatusHistoryAsync(string orderId)
        {
            try
            {
                //return await _context.OrderStatusHistories
                //    .AsNoTracking()
                //    .Where(h => h.OrderId == orderId)
                //    .OrderBy(h => h.OrderStatusTimestamp)
                //    .ToListAsync();

                if (orderId == "1")
                {
                    return new List<OrderStatusHistory>
                    {
                        new OrderStatusHistory { Id = 1, OrderId = "1", Status = "Created", OrderStatusTimestamp = DateTime.UtcNow.AddDays(-3) },
                        new OrderStatusHistory { Id = 2, OrderId = "1", Status = "Processing", OrderStatusTimestamp = DateTime.UtcNow.AddDays(-1) }
                    };
                }
                else
                {
                    return new List<OrderStatusHistory>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to return status history for order {orderId}.");
                throw;
            }
        }
    }
}
