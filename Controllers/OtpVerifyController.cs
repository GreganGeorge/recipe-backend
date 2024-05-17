using Microsoft.AspNetCore.Mvc;
using System;
namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpVerifyController : ControllerBase
    {
        [HttpPost]
        [Route("verify_otp")]
        public IActionResult VerifyOTP([FromBody] VerifyOTPRequest request)
        {
            try
            {
                // Validate request
                if (string.IsNullOrWhiteSpace(request?.enteredOTP))
                {
                    return BadRequest("Invalid OTP data.");
                }
                bool isValidOTP = request.enteredOTP == request.OTP; // Example OTP, replace with actual logic

                return Ok(new { isValid = isValidOTP });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }

    public class VerifyOTPRequest
    {
        public string enteredOTP { get; set; }
        public string OTP { get; set; }
    }
}
