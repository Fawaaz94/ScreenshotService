using AzureClassLibrary.Infrastructure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureClassLibrary.QueueConnection
{
    public interface ICloudQueueClientFactory
    {
        CloudQueueClient GetClient();
    }

    public class CloudQueueClientFactory : ICloudQueueClientFactory
    {
        private readonly QueueConfig _queueConfig;
        // A reusable client, so that we dont go through socket exhaustion
        private CloudQueueClient _cloudQueueClient;

        public CloudQueueClientFactory(QueueConfig queueConfig)
        {
            _queueConfig = queueConfig; 
        }

        public CloudQueueClient GetClient()
        {
            if (_cloudQueueClient != null)
                return _cloudQueueClient;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_queueConfig.QueueConnectionString);

            _cloudQueueClient = storageAccount.CreateCloudQueueClient();

            return _cloudQueueClient;
        }
    }
}
