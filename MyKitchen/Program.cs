using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        private static IWebHost Host { get; set; }
        private static ILogger Logger { get; set; }


        public static void Main(string[] args)
        {

            //I would really like to cleanup my logs, as well as transition to the new .NET 6 WebApplicationBuilder
            Host = CreateWebHostBuilder(args).Build();
            SeedDataBase();
            InitLogger();

            var addr = Host.ServerFeatures.Get<IServerAddressesFeature>();
            var addrString = addr.Addresses.Aggregate((current, next) => current + "," + next);


            Logger.LogTrace($"ASPNETCORE_ENVIRONMENT={Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
            Logger.LogInformation($"Program Running at {addrString}");
            

            Host.Run();
        }

        private static void SeedDataBase()
        {
            using IServiceScope scope = Host.Services.CreateScope();
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

        public static void InitLogger()
        {
            using IServiceScope scope = Host.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();
            Logger = logger;
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
                            //Todo update to use AzureDefaultCredential
                            //Todo why not use a developent keyvault?

                            var azureServicetokenProvider = new AzureServiceTokenProvider();
                            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServicetokenProvider.KeyVaultTokenCallback));

                            var keyuri = $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/";

                            config.AddAzureKeyVault(keyuri,keyVaultClient,new DefaultKeyVaultSecretManager());
                        }


                    })
                    
                    //.ConfigureKestrel((ctx,opt) =>
                    //{
                    //    opt.ListenAnyIP(80);

                    //    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    //    var isDevelopment = environment == Environments.Development;

                    //    if(!isDevelopment)
                    //    {
                    //        // opt.ListenAnyIP(443, listenOpt => { listenOpt.UseHttps(ctx.Configuration["CertificateFileLocation"],ctx.Configuration["CertPassword"]); });
                    //        opt.ListenAnyIP(443, listenOpt => { listenOpt.UseHttps();});
                    //    }
                    //}).
                   .UseStartup<Startup>().ConfigureLogging(x =>
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
