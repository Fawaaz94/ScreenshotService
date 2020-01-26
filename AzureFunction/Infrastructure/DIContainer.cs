using AzureClassLibrary.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace AzureFunction.Infrastructure
{
    public sealed class DIContainer
    {

        private static readonly IServiceProvider _instance = Build();

        // This is how you create a thread safe singleton in C#
        public static IServiceProvider instance => _instance;


        static DIContainer()
        { }

        private DIContainer()
        { }

        private static IServiceProvider Build()
        {
            var services = new ServiceCollection();

            // Optional is important, because the Json file will not get deployed
            // The cloud used Environment variables
            //var configuration = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            //     .AddEnvironmentVariables()
            //     .Build();

            services.AddSingleton<IScreenshotCommandHandler, ScreenshotCommandHandler>();

            //services.AddAzureQueueLibrary(configuration["AzureWebJobsStorage"]);
            services.AddAzureQueueLibrary("DefaultEndpointsProtocol=https;AccountName=detectifystore;AccountKey=+DXM1TNsJSOmVTisoP4ky940R9S2Y+2prxmrnRtzM3jDTeOYI5L4votugmkPM9JoEnWG0DgcE+v0mo17l1vs4w==;EndpointSuffix=core.windows.net");

            return services.BuildServiceProvider();
        }



    }
}
