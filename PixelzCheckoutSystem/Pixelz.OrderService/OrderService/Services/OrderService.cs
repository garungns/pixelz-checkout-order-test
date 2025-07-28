using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Data;
using Pixelz.Models.Common;
using Pixelz.Models.Constants;
using Pixelz.Models.Dtos;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceUrls _serviceUrls;
        private readonly string _paymentProcessUrl;
        private readonly string _productionSubmitUrl;
        private readonly string _emailSendUrl;

        public OrderService(OrderDbContext context
            , IHttpClientFactory httpClientFactory, IOptions<ServiceUrls> serviceUrls)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _serviceUrls = serviceUrls.Value;
            _paymentProcessUrl = _serviceUrls.Payment.Endpoints["Process"];
            _productionSubmitUrl = _serviceUrls.Production.Endpoints["Submit"];
            _emailSendUrl = _serviceUrls.Email.Endpoints["Send"];
        }

        public async Task<ApiResponse<IEnumerable<OrderDto>>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.Customer)
                .ToListAsync();

            var result = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                Name = o.Name,
                CustomerId = o.Customer.Id,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString()
            });

            return ApiResponse<IEnumerable<OrderDto>>.Success(result);
        }

        public async Task<ApiResponse<IEnumerable<OrderDto>>> SearchOrdersAsync(Guid customerId, string name)
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.Customer)
                .Where(o => o.CustomerId == customerId && (string.IsNullOrEmpty(name) || o.Name.Contains(name, StringComparison.OrdinalIgnoreCase)))
                .ToListAsync();

            var result = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                Name = o.Name,
                CustomerId = o.Customer.Id,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString()
            });

            return ApiResponse<IEnumerable<OrderDto>>.Success(result);
        }

        public async Task<ApiResponse<bool>> CheckoutOrderAsync(Guid customerId,Guid orderId)
        {
            var order = await _context.Orders
                                .Include(o => o.Items)
                                .Include(o => o.Customer)
                                .FirstOrDefaultAsync(o => o.Id == orderId && o.CustomerId == customerId);

            if (order == null)
                return ApiResponse<bool>.Fail("Order not found.");

            if (order.Status != OrderStatus.Pending)
                return ApiResponse<bool>.Fail("Order is not in a valid state for checkout.");

            // 1. Call PaymentService
            var paymentClient = _httpClientFactory.CreateClient("Payment");

            var paymentResponse = await paymentClient.PostAsJsonAsync(_paymentProcessUrl, new
            {
                OrderId = order.Id,
                Amount = order.TotalAmount
            });

            if (!paymentResponse.IsSuccessStatusCode)
            {
                order.Status = OrderStatus.PaymentFailed;
                await _context.SaveChangesAsync();
                return ApiResponse<bool>.Fail("Payment failed.");
            }

            // 2. Call ProductionService
            var productionClient = _httpClientFactory.CreateClient("Production");
            var prodResponse = await productionClient.PostAsJsonAsync(_productionSubmitUrl, new
            {
                OrderId = order.Id,
                Items = order.Items.Select(i => new { i.Id, i.ProductId, i.Quantity }).ToList()
            });

            if (!prodResponse.IsSuccessStatusCode)
            {
                order.Status = OrderStatus.ProductionFailed;
                await _context.SaveChangesAsync();
                return ApiResponse<bool>.Fail("Failed to submit order to production system.");
            }

            // 3. Call EmailService
            var emailClient = _httpClientFactory.CreateClient("Email");
            await emailClient.PostAsJsonAsync(_emailSendUrl, new
            {
                To = order.Customer?.Email,
                Subject = "Your order has been placed!",
                Body = $"Order #{order.Id} is successful!"
            });

            order.Status = OrderStatus.Completed;
            await _context.SaveChangesAsync();

            return ApiResponse<bool>.Success(true, "Order checked out successfully.");
        }
    }
}
