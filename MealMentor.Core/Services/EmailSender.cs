using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using SendGrid;

namespace MealMentor.Core.Services
{

    public class MailGunEmailSender(IConfiguration configuration) : IEmailService
    {
        private IConfiguration Configuration { get; } = configuration;
        public async Task<ICommandResult> SendEmailAsync(string email, string subject, string message)
        {

            var key = Configuration["Mailgun:ApiKey"];
        var options = new RestClientOptions("https://api.mailgun.net")
                {
                    Authenticator = new HttpBasicAuthenticator("api",key)
                };

                var client = new RestClient(options);
                var request = new RestRequest("/v3/blueprogrammer.com/messages", Method.Post);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("from", "MealMentor <postmaster@blueprogrammer.com>");
                request.AddParameter("to", email);//0//Matthew Eskolin <matteskolin@gmail.com>");
                request.AddParameter("subject", subject);//"Hello Matthew Eskolin");
                request.AddParameter("text", message);//"Congratulations Matthew Eskolin, you just sent an email with Mailgun! You are truly awesome!");
                var r1 =  await client.ExecuteAsync(request);
                return r1.IsSuccessful ? 
                    new CommandResult { IsSuccess = true } : 
                    new CommandResult { IsSuccess = false };

        }

        public string GetServiceName()
        {
            return "MailGun";
        }
    }


    public interface IEmailService
    {
        public Task<ICommandResult> SendEmailAsync(string email, string subject, string message);
        public string GetServiceName();
    }
}


