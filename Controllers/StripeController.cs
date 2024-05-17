using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripeController : ControllerBase
    {
        private readonly string _stripeSecretKey = "sk_test_51P2BgZSAzlT6XHrmIeqxWUsFUfX6CXJQQayZ9xU5d8v9LlklDFUv7Jf91T7zT9HSzMHBnDJdJVhHMf4i6o8ZZoJw00NrK0C8jD";

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession(List<CartItem> cartItems)
        {
            try
            {
                StripeConfiguration.ApiKey = _stripeSecretKey;

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = cartItems.ConvertAll(item => new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "inr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.ingredient_name,
                                Images = new List<string> { item.ingredient_image },
                            },
                            UnitAmount = (long)(item.ingredient_price * 100), 
                        },
                        Quantity = item.Quantity
                    }),
                    Mode = "payment",
                    SuccessUrl = "http://localhost:3000/success",
                    CancelUrl = "http://localhost:3000/cancel",
                };

                var service = new SessionService();
                var session = await service.CreateAsync(options);

                return Ok(new { sessionId = session.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        public class CartItem
        {
            public string ingredient_name { get; set; }
            public decimal ingredient_price { get; set; }
            public string ingredient_image { get; set; }
            public int Quantity { get; set; }
        }
    }
}