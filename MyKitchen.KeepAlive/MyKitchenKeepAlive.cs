using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace MyKitchen.KeepAlive
{
    public class MyKitchenKeepAlive
    {
        [FunctionName("MyKitchenKeepAlive")]
        //public void Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer, ILogger log)
        public void Run([TimerTrigger("*/5 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            log.LogInformation( "We still need to add the actual logic. right now this function app is not doing anything. This message should be logged very 5 minutes.");
        }
    }
}
