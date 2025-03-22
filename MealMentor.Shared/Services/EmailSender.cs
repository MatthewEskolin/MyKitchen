using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MealMentor.Shared.Services
{
    public class EmailSender(IConfiguration configuration)
    {
        private IConfiguration Configuration { get; } = configuration;

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var key = Configuration["Sendgrid:ApiKey"];

            if (key == null) throw new System.Exception("Sendgrid ApiKey not found");

            return Execute(key, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("mattheweskolin@blueprogrammer.com", "Kitchen Assistant"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

      
    }


}


