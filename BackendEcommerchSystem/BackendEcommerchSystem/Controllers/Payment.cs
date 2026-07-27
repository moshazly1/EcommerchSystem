using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace BackendEcommerchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Payment : ControllerBase
    {
        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] decimal amount)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(amount * 100),
                    Currency = "usd",
                };
                var service = new PaymentIntentService();
                var PaymentIntent = await service.CreateAsync(options);
                return Ok(new
                {
                    clientSecret = PaymentIntent.ClientSecret
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
