using Pixelz.Models.Constants;

namespace OrderService.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
