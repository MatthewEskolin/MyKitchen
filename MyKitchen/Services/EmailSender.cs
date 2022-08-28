using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using MyKitchen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyKitchen.Data;
using MyKitchen.Models.BL;

namespace MyKitchen.Services
{

    public interface IMyKitchenDataService
    {
        Task UpdateSettingsAsync(string settings);

    }

    public class MyKitchenDataService : IMyKitchenDataService
    {
        public ApplicationDbContext _ctx { get; set; }

        private ILogger _Logger;

        private MyKitchen.Models.BL.UserInfo  _userInfo;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyKitchenDataService(ILogger<MyKitchenDataService> logger, ApplicationDbContext ctx, UserInfo userInfo, UserManager<ApplicationUser> userManager)
        {
            _Logger = logger;
            _ctx = ctx;
            _userInfo = userInfo;
            _userManager = userManager;
        }

        public async Task UpdateSettingsAsync(string settings)
        {
            var user = await _userManager.FindByIdAsync(_userInfo.User.Id);

            user.MealImage = settings;
            user.FavoriteFood = "food test";

            user.PhoneNumber = "222";

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                //Success
            }
            else
            {
                _Logger.LogError($"Failed to Update User {result.Errors}");
            }



        }
    }


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


