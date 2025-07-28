using Microsoft.AspNetCore.Mvc;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //Use to get the customerid for testing other api
        [HttpGet("orders")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return Ok(result);
        }


        [HttpGet("{customerId}/orders")]
        public async Task<IActionResult> SearchOrderAsync(Guid customerId, [FromQuery] string name)
        {
            var result = await _orderService.SearchOrdersAsync(customerId, name);
            return Ok(result);
        }


        [HttpPost("{customerId}/checkout/{orderId}")]
        public async Task<IActionResult> CheckoutOrderAsync(Guid customerId, Guid orderId)
        {
            var result = await _orderService.CheckoutOrderAsync(customerId, orderId);

            return Ok(result);
        }
    }
}
