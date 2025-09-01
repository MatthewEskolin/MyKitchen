using Microsoft.Extensions.Configuration;

namespace MealMentor.Core.Services;

//public class MailGunEmailSender(IConfiguration configuration) : IEmailService
//{
//    private IConfiguration Configuration { get; } = configuration;
//    public async Task<ICommandResult> SendEmailAsync(string email, string subject, string message)
//    {

//        var key = Configuration["Mailgun:ApiKey"];
//        var domain = "blueprogrammer.com";
//        var fromemail = "noreply@blueprogrammer.com";
//        var fromname = "MealMentor";

//        ArgumentNullException.ThrowIfNull(key, "Mailgun ApiKey not found");
//        if (key == null) throw new Exception("Mailgun ApiKey not found");

//        var messageBuilder = new MessageBuilder()
//            .SetFromAddress(fromEmail, fromName)
//            .AddToRecipient(email)
//            .SetSubject(subject)
//            .SetTextBody(message)
//            .SetHtmlBody(message);

//        try
//        {
//            var response = await client.SendMessageAsync(messageBuilder);

//            if (response.IsSuccessStatusCode)
//            {
//                return new CommandResult { IsSuccess = true };
//            }
//            else
//            {
//                return new CommandResult { IsSuccess = false };
//            }
//        }
//        catch
//        {
//            return new CommandResult { IsSuccess = false };
//        }

//    }
//}