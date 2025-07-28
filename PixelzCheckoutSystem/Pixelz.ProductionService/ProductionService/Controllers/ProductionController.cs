using Microsoft.AspNetCore.Mvc;
using Pixelz.Models.Common;

namespace ProductionService.Controllers
{
    [ApiController]
    [Route("api/production")]
    public class ProductionController : ControllerBase
    {
        [HttpPost("submit")]
        public IActionResult SubmitProduction([FromBody] SubmitProductionRequest request)
        {
            //Mock for fail and success full case 
            if (request.Items.Sum(q => q.Quantity) > 100)
            {
                return StatusCode(400);
            }
            else
            {
                Console.WriteLine($"Production request for Order: {request.OrderId} with {request.Items.Count} items.");
                return Ok(new { Message = "Production submitted successfully" });
            }

        }
    }
}
