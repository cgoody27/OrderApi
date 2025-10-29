namespace OrderApi.Models
{
    public class OrderStatusHistory
    {
        public int Id { get; set; }
        public required string OrderId { get; set; }
        public required string Status { get; set; }
        public DateTime OrderStatusTimestamp { get; set; }
    }
}
