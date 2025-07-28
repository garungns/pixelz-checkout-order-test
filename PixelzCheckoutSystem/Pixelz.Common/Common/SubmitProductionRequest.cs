namespace Pixelz.Models.Common
{
    public class SubmitProductionRequest
    {
        public Guid OrderId { get; set; }
        public List<ProductionItem> Items { get; set; }
    }
    public class ProductionItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
