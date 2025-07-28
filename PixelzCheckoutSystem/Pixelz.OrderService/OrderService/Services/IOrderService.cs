using Pixelz.Models.Common;
using Pixelz.Models.Dtos;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<ApiResponse<IEnumerable<OrderDto>>> GetAllOrdersAsync();
        Task<ApiResponse<IEnumerable<OrderDto>>> SearchOrdersAsync(Guid customerId, string name);
        Task<ApiResponse<bool>> CheckoutOrderAsync(Guid customerId, Guid orderId);
    }
}