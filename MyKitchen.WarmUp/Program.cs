using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyKitchen.WarmUp;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, configuration) =>
{
    configuration.Sources.Clear();
    IHostEnvironment env = hostingContext.HostingEnvironment;
    configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
    configuration.Build();


}).Build();

var config = host.Services.GetRequiredService<IConfiguration>();
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

// Application code should start here.
Console.WriteLine("App Started");

var warmup = new AppWarmUp(config);
warmup.RunWarmup();
await host.RunAsync();
