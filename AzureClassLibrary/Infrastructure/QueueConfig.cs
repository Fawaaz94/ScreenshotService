using System;
using System.Collections.Generic;
using System.Text;

namespace AzureClassLibrary.Infrastructure
{
    public class QueueConfig
    {
        public string QueueConnectionString { get; set; }

        public QueueConfig()
        {

        }

        public QueueConfig(string queueConenctionString)
        {
            QueueConnectionString = queueConenctionString;
        }
    }
}
