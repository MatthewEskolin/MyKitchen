using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using MealMentor.Core.Services;

namespace MealMentor.Tests.Integration
{
    public class EmailTests(WebApplicationFactory<MealMentor.Program> factory)
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<MealMentor.Program> _factory = factory;


        //[Fact]
        //public async Task SendEmailTest()
        //{
        //    // Arrange: get EmailSender from DI
        //    using var scope = _factory.Services.CreateScope();
        //    var emailSender = scope.ServiceProvider.GetRequiredService<SendGridEmailSender>();

        //    // Act: send a test email
        //    var response = await emailSender.SendEmailAsync(
        //        "recipient@example.com", // Use a test email address
        //        "Integration Test Subject",
        //        "This is a test email from integration test."
        //    );

        //    // Assert: check that SendGrid accepted the request
        //    //jAssert.True(response.IsSuccessStatusCode!);// $"SendGrid response: {response.StatusCode}");
        //}
    }
}