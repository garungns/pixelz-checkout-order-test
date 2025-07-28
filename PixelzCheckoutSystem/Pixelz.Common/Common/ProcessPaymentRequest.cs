namespace Pixelz.Models.Common
{
    public class ProcessPaymentRequest
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
