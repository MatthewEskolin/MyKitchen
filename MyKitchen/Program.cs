using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyKitchen.Data;

namespace MyKitchen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {

                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<MyKitchen.Data.ApplicationDbContext>();
                    DbInitializer.Initialize(context);

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration((context,config) =>
                    {
                        var builtConfig = config.Build();

                        var azureServicetokenProvider = new AzureServiceTokenProvider();
                        var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServicetokenProvider.KeyVaultTokenCallback));

                        var keyuri = $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/";

                        config.AddAzureKeyVault(keyuri,keyVaultClient,new DefaultKeyVaultSecretManager());
                    })
                    .ConfigureKestrel((ctx,opt) =>
                    {
                        opt.ListenAnyIP(80);
                        opt.ListenAnyIP(443, listenOpt => { listenOpt.UseHttps(ctx.Configuration["CertificateFileLocation"],ctx.Configuration["CertPassword"]); });
                    }).
                    UseStartup<Startup>().ConfigureLogging(x =>
                    {
                        x.ClearProviders();
                        x.AddApplicationInsights();

                    })
                    .UseUrls("https://mykitchen.azurewebsites.com")
            ;
    }
}
