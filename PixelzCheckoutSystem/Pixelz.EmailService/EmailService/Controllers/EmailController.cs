using Microsoft.AspNetCore.Mvc;
using Pixelz.Models.Common;

namespace EmailService.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailController : ControllerBase
    {
        [HttpPost("send")]
        public IActionResult Send([FromBody] SendEmailRequest request)
        {
            Console.WriteLine($"Email sent to {request.To} - Subject: {request.Subject}");
            return Ok(new { Message = "Email sent successfully" });
        }
    }
}
