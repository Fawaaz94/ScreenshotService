using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureClassLibrary.Infrastructure.MessageSerialzer
{
    public interface IMessageSerializer
    {
        T Deserialize<T>(string message);
        string Serialize(object obj);
    }

    public class JsonMessageSerializer : IMessageSerializer
    {
        public JsonMessageSerializer()
        {

        }

        public T Deserialize<T>(string message)
        {
            var obj = JsonConvert.DeserializeObject<T>(message);
            
            return obj;
        }

        public string Serialize(object obj)
        {
            var message = JsonConvert.SerializeObject(obj);
            return message;
        }
    }
}
