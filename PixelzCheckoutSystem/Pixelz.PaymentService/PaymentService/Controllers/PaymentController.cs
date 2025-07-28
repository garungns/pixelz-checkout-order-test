using Microsoft.AspNetCore.Mvc;
using Pixelz.Models.Common;

namespace PaymentService.Controllers
{

    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        [HttpPost("process")]
        public IActionResult ProcessPayment([FromBody] ProcessPaymentRequest request)
        {
            //Mock fail and successful case
            if (request.Amount % 2 != 0)
            {
                return StatusCode(400);
            }
            else
            {
                Console.WriteLine($"Processed payment for Order: {request.OrderId}, Amount: {request.Amount}");
                return Ok(new { Message = "Payment processed successfully" });
            }

        }
    }
}
