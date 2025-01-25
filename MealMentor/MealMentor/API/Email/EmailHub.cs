using MealMentor.Shared.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace MealMentor.API.Email
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailHub:ControllerBase
    {

        public EmailSender EmailSender { get; set; }

        public EmailHub(EmailSender es)
        {
            this.EmailSender = es;
        }

        [HttpPost("SendTestEmail")]
        public async Task<IActionResult> SendTestEmail()
        {
                var subject = "This is an E-mail sent using SendGridClient";
                var to = "matteskolin@gmail.com";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

                await EmailSender.SendEmailAsync(to, htmlContent, subject);

                return Ok("Success");
        }
    }
}
