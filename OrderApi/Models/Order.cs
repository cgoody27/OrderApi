namespace OrderApi.Models
{
    public class Order
    {
        public required string OrderId { get; set; }
        public required string CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public required string Status { get; set; }
    }
}
