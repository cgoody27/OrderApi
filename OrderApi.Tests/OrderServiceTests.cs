using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OrderApi.Data;
using OrderApi.Services;
using Xunit;

namespace OrderApi.Tests
{
    public class OrderServiceTests
    {
        private OrdersDbContext GetDbContextWithData()
        {
            var options = new DbContextOptionsBuilder<OrdersDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new OrdersDbContext(options);

            context.Orders.Add(new Models.Order
            {
                OrderId = "1",
                CustomerId = "cust1",
                TotalAmount = 134.00m,
                Status = "Processing"
            });

            context.OrderStatusHistories.AddRange(
                new Models.OrderStatusHistory { Id = 1, OrderId = "1", Status = "Created", OrderStatusTimestamp = DateTime.UtcNow.AddDays(-3) },
                new Models.OrderStatusHistory { Id = 2, OrderId = "1", Status = "Processing", OrderStatusTimestamp = DateTime.UtcNow.AddDays(-1) }
            );

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetOrderSummary_ReturnsOrder()
        {
            var context = GetDbContextWithData();
            var logger = new Mock<ILogger<OrderService>>();
            var service = new OrderService(context, logger.Object);

            var result = await service.GetOrderSummaryAsync("1");

            Assert.NotNull(result);
            Assert.Equal("cust1", result.CustomerId);
        }

        [Fact]
        public async Task GetOrderSummary_WhenOrderNotFound_ReturnsNull()
        {
            var context = GetDbContextWithData();
            var logger = new Mock<ILogger<OrderService>>();
            var service = new OrderService(context, logger.Object);

            var result = await service.GetOrderSummaryAsync("1111111");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetOrderHistory_ReturnsOrderHistory()
        {
            var context = GetDbContextWithData();
            var logger = new Mock<ILogger<OrderService>>();
            var service = new OrderService(context, logger.Object);

            var result = await service.GetOrderStatusHistoryAsync("1");

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, h => h.Status == "Created");
        }

        [Fact]
        public async Task GetOrderHistory_WhenOrderNotFound_ReturnsEmptyList()
        {
            var context = GetDbContextWithData();
            var logger = new Mock<ILogger<OrderService>>();
            var service = new OrderService(context, logger.Object);

            var result = await service.GetOrderStatusHistoryAsync("111111");

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
