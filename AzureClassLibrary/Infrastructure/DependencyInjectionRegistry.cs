using AzureClassLibrary.Infrastructure.MessageSerialzer;
using AzureClassLibrary.QueueConnection;
using Microsoft.Extensions.DependencyInjection;

namespace AzureClassLibrary.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddAzureQueueLibrary(this IServiceCollection service, string queueConnectionString)
        {
            service.AddSingleton(new QueueConfig(queueConnectionString));
            service.AddSingleton<ICloudQueueClientFactory, CloudQueueClientFactory>();
            service.AddSingleton<IMessageSerializer, JsonMessageSerializer>();

            // Adding Transient for Azure function - since the function wont know what a scope is, it will know what transient is
            // Also so that we want this to be used by different components until we want to dispose of it
            service.AddTransient<IQueueCommunicator, QueueCommunicator>();

            return service;
        }
    }
}
