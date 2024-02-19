using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyKitchen.WarmUp;

namespace MyKitchen.KeepAlive
{
    public class MyKitchenKeepAlive
    {
        [FunctionName("MyKitchenKeepAlive")]
        public void Run_MyitchenKeepAlive([TimerTrigger("0 */20 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"MyKitchenKeepAlive Triggered at: {DateTime.Now}");
            log.LogInformation("This Message should be logged every 20 minutes");

            //try to make call to app-warmup task
            var warmup = new AppWarmUp();

            log.LogTrace("Warmup Instance Created..");
            log.LogTrace("Starting Warmup..");
            warmup.RunWarmup();

            var next = myTimer.Schedule.GetNextOccurrence(DateTime.Now);

            log.LogTrace($"Next Trigger at {next}");
            log.LogTrace($"Warmup Complete.. next occurance is at ");
        }



        [FunctionName("MyKitchenKeepAlive2")]
        public void Run_MyKitchenKeepAlive2([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation($"MyKitchenKeepAlive Triggered at: {DateTime.Now}");
            log.LogInformation("This Message should be logged every 20 minutes");

            //try to make call to app-warmup task
            var warmup = new AppWarmUp();

            log.LogTrace("Warmup Instance Created..");
            log.LogTrace("Starting Warmup..");
            warmup.RunWarmup();

           // var next = myTimer.Schedule.GetNextOccurrence(DateTime.Now);

            //log.LogTrace($"Next Trigger at {next}");
            log.LogTrace($"Warmup Complete.. next occurance is at ");
        }

    }

}
