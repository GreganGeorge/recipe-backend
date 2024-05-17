using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IEmailService emailService;

        public OtpController(IEmailService service)
        {
            this.emailService = service;
        }
        [HttpPost]
        [Route("send_recovery_email")]
        /*public async Task<IActionResult> Index([FromBody] RecoveryEmailRequest request)
        {
            try
            {
                // Validate request
                if (string.IsNullOrWhiteSpace(request?.OTP) || string.IsNullOrWhiteSpace(request.recipient_email))
                {
                    return BadRequest("Invalid request data.");
                }

                // Send email using SMTP client (configure SMTP settings in your appsettings.json)
                using (SmtpClient emailSender = new SmtpClient())
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(request.recipient_email);
                    mailMessage.Subject = "Recovery OTP";
                    mailMessage.Body = $"Your recovery OTP is: {request.OTP}";

                    await emailSender.SendMailAsync(mailMessage);
                }

                return Ok("Recovery email sent successfully.");
            }*/
        public async Task<IActionResult> SendMail([FromBody] RecoveryEmailRequest request)
        {
            try
            {
                    // Validate request
                if (string.IsNullOrWhiteSpace(request?.OTP) || string.IsNullOrWhiteSpace(request.recipient_email))
                {
                    return BadRequest("Invalid request data.");
                }
                Mailrequest mailrequest = new Mailrequest();
                mailrequest.ToEmail = request.recipient_email;
                mailrequest.Subject = "Recovery OTP";
                mailrequest.Body = $"Your recovery OTP is: {request.OTP}";
                await emailService.SendEmailAsync(mailrequest);
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }

    public class RecoveryEmailRequest
    {
        public string OTP { get; set; }
        public string recipient_email { get; set; }
    }
}
