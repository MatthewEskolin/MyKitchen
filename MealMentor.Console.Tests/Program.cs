using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using MealMentor.Core.Services;
using Microsoft.Extensions.Configuration;

namespace MealMentor.Console.Tests
{
    internal static class Program
    {
        public static string env_arg = string.Empty;

        static async Task Main(string[] args)
        {
            if (args[0] == "dev") ;
            {
                env_arg = args[0];
            }
            
            var config = await GetConfig();

            var emailSender  = new MailGunEmailSender(config);
            await emailSender.SendEmailAsync("matthew.eskolin@outlook.com", "This is a test email sent using MailGun", "<strong>and easy to do anywhere, even with C#</strong>");
            System.Console.WriteLine("Email sent successfully.");
        }


        private static async Task<IConfiguration> GetConfig()
        {
            await Task.Yield();

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


            //check if aspnetcore_development is development environment variable is set
            if(env_arg == "dev")
            {
                configurationBuilder.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

            }


            var configuration = configurationBuilder.Build();

            var keyUri = $"https://{configuration["keyVaultName"]}.vault.azure.net/";

            // Create a SecretClient using DefaultAzureCredential
            var client = new SecretClient(new Uri(keyUri), new DefaultAzureCredential());

            // Retrieve a secret from the Key Vault
            KeyVaultSecret secret = await client.GetSecretAsync("Mailgun--ApiKey");
            if (secret.Value == null) throw new Exception("MailGun ApiKey not found");

            configurationBuilder.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>("MailGun:ApiKey", secret.Value)
            }!);

            configuration = configurationBuilder.Build();


            return configuration;

        }
    }

    //create a class for testing 
}
