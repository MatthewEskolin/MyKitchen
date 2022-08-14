using System;
using System.Security.Principal;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyKitchen.Data;

namespace MyKitchen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var sqlConnection = new SqlConnection(@"Server =localhost\SQLEXPRESS; Initial Catalog = MyKitchen; Trusted_Connection = true; ");
            //sqlConnection.Open();
            //sqlConnection.Close();


            IWebHost host = CreateWebHostBuilder(args).Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {

                IServiceProvider services = scope.ServiceProvider;

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

                        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                        var isDevelopment = environment == Environments.Development;

                        if(!isDevelopment)
                        {
                            var azureServicetokenProvider = new AzureServiceTokenProvider();
                            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServicetokenProvider.KeyVaultTokenCallback));

                            var keyuri = $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/";

                            config.AddAzureKeyVault(keyuri,keyVaultClient,new DefaultKeyVaultSecretManager());
                        }


                    })
                    
                    .ConfigureKestrel((ctx,opt) =>
                    {
                        opt.ListenAnyIP(80);

                        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                        var isDevelopment = environment == Environments.Development;

                        if(!isDevelopment)
                        {
                            // opt.ListenAnyIP(443, listenOpt => { listenOpt.UseHttps(ctx.Configuration["CertificateFileLocation"],ctx.Configuration["CertPassword"]); });
                            opt.ListenAnyIP(443, listenOpt => { listenOpt.UseHttps();});
                        }
                    }).
                    UseStartup<Startup>().ConfigureLogging(x =>
                    {
                        x.ClearProviders();
                        //x.AddApplicationInsights();
                        x.AddDebug();
                        x.AddConsole();

                    })
                    // .UseUrls("https://mykitchen.azurewebsites.com")
            ;
    }
}
