using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyKitchen.WarmUp;

//Write Debug Output to the Console
TextWriterTraceListener myWriter = new TextWriterTraceListener(System.Console.Out);
Trace.Listeners.Add(myWriter);

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.Sources.Clear();
        IHostEnvironment env = hostingContext.HostingEnvironment;
        configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
        configuration.Build();


    }).Build();

var config = host.Services.GetRequiredService<IConfiguration>();

Debug.WriteLine("App Environment Set to " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
Debug.Assert(false);
// Application code should start here.
Console.WriteLine("App Started");

var warmup = new AppWarmUp(config);
warmup.RunWarmup();
await host.RunAsync();

