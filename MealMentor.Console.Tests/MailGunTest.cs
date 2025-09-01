using MealMentor.Core.Services;

namespace MealMentor.Console.Tests
{
    public class MailGunTest(MailGunEmailSender emailSender)
    {
        public async Task SendTestEmail()
        {
            var subject = "This is a test email sent using SendGridClient";
            var to = "matteskolin@gmail.com";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

            await emailSender.SendEmailAsync(to, subject, htmlContent);
        }
    }
}

