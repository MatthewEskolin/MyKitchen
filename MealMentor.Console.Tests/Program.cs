using System.Diagnostics.Tracing;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using MealMentor.Shared.Services;
using Microsoft.Extensions.Configuration;

namespace MealMentor.Console.Tests
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await EmailTest();
        }

        //E-mail Test
        private static async Task EmailTest()
        {
            var config = getConfig();
            var emailSender = new EmailSender((IConfiguration)config);
            await new SendGridTest(emailSender).SendTestEmail();
            System.Console.WriteLine("Email Sent?");
        }

        private static object getConfig()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = configurationBuilder.Build();

            var keyUri = $"https://{configuration["keyVaultName"]}.vault.azure.net/";

            // Create a SecretClient using DefaultAzureCredential
            var client = new SecretClient(new Uri(keyUri), new DefaultAzureCredential());

            // Retrieve a secret from the Key Vault
            KeyVaultSecret secret = client.GetSecret("Sendgrid--ApiKey");
            if (secret.Value == null) throw new Exception("Sendgrid ApiKey not found");

            configurationBuilder.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>("Sendgrid:ApiKey", secret.Value)
            }!);

            configuration = configurationBuilder.Build();


            return configuration;

        }
    }

    //create a class for testing 
}
