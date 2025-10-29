using OrderApi.Models;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderSummaryAsync(string orderId);
        Task<List<OrderStatusHistory>> GetOrderStatusHistoryAsync(string orderId);
    }
}
