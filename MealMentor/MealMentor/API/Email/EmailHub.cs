using Microsoft.AspNetCore.Mvc;
using MealMentor.Core.Services;

namespace MealMentor.API.Email
{   
    /// <summary>
    /// Provides endpoints for sending emails using the Registered email service.
    /// </summary>
    /// <remarks>This controller is designed to handle email-related operations, such as sending test emails.
    /// It uses the <see cref="SendGridEmailSender"/> service to send emails.</remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class EmailHub(IEmailService es) : ControllerBase
    {
        private IEmailService EmailService { get; set; } = es;

        [HttpPost("SendTestEmail")]
        public async Task<IActionResult> SendTestEmail()
        {
                var subject = $"MealMentor.Api.Email Test";
                var to = "matthew.eskolin@outlook.com";
                var htmlContent = $"<strong>Service Name: {EmailService.GetServiceName()}";

                await EmailService.SendEmailAsync(to, htmlContent, subject);

                return Ok("Success");
        }
    }
}
