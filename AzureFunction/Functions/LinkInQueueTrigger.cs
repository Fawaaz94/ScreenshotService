using System;
using AzureClassLibrary.Infrastructure;
using AzureClassLibrary.Link;
using AzureClassLibrary.QueueConnection;
using AzureFunction.Infrastructure;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AzureFunction.Functions
{
    public static class LinkInQueueTrigger
    {
        [FunctionName("LinkInQueueTrigger")]
        public static async Task Run(
            [QueueTrigger(RouteNames.LinksBox, Connection = "AzureWebJobsStorage")]
            string message, 
            ILogger log)
        {
            try
            {   
                var handler = DIContainer.instance.GetService<IScreenshotCommandHandler>();
                await handler.Handle(message);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Something went wrong with the ScreenshotQueueTrigger {message}");
                throw; // this throw allows requeueing of messages - up to 5 times
            }

            log.LogInformation($"C# Queue trigger function processed: {message}");
        }
    }
}
