using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using MyKitchen;
using Microsoft.Extensions.Configuration;

namespace MyKitchen{

    public class EmailSender : IEmailSender
    {
            
        public IConfiguration Configuration { get; }

        public EmailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,IConfiguration configuration)
        // {
        //     Options = optionsAccessor.Value;
        // }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var key = Configuration["Sendgrid:ApiKey"];
            // return Execute(Options.SendGridKey, subject, message, email);
            return Execute(key, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("MatthewEskolin@bluprogrammer.com", "Matthew Eskolin"),
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


