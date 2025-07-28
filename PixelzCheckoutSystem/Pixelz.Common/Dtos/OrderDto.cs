namespace Pixelz.Models.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

    }
}
