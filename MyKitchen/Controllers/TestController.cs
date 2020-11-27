using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MyKitchen.Controllers
{
    public class TestController:Controller{

        protected IConfiguration configuration {get; set;}

            public TestController(IConfiguration config)
            {
                configuration = config;
            }

        public async Task<IActionResult> EmailTest()
        {
            var apiKey = configuration["Sendgrid:ApiKey"];


            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mattheweskolin@blueprogrammer.com", "Matthew eskjolin");
            var subject = "This is an E-mail sent using SendGridClient";
            var to = new EmailAddress("matteskolin@gmail.com", "To My Gmail");
            var to1 = new EmailAddress("mjeskolin@gmail.com", "To My Gmail");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var msg1 = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response =  await client.SendEmailAsync(msg);
            var response1 =  await client.SendEmailAsync(msg1);

            return View();

        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Index1()
        {

            return View();
        }

    }
}