using AzureClassLibrary.Infrastructure;
using AzureClassLibrary.Infrastructure.MessageSerialzer;
using AzureClassLibrary.Links;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading.Tasks;

namespace AzureClassLibrary.QueueConnection
{
    public interface IQueueCommunicator
    {
        T Read<T>(string message);

        // Where statement give us access to the route
        Task SendAsync(string obj);
    }

    public class QueueCommunicator : IQueueCommunicator
    {   
        private readonly IMessageSerializer _messageSerializer;
        private readonly ICloudQueueClientFactory _cloudQueueClientFactory;

        public QueueCommunicator(IMessageSerializer messageSerializer,
            ICloudQueueClientFactory cloudQueueClientFactory)
        {
            _messageSerializer = messageSerializer;
            _cloudQueueClientFactory = cloudQueueClientFactory;
        }

        public T Read<T>(string message)
        {
            return _messageSerializer.Deserialize<T>(message);
        }

        // Adding the generic type to the message sending
        public async Task SendAsync(string obj) 
        {
            // Grabbing the reference to the Queue
            var queueReference = _cloudQueueClientFactory.GetClient().GetQueueReference(RouteNames.LinksBox);

            await queueReference.CreateIfNotExistsAsync();


            var queueMessage = new CloudQueueMessage(obj);

            await queueReference.AddMessageAsync(queueMessage);
        }
    
    }
}
