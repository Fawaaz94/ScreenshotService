using System;
using System.Collections.Generic;
using System.Text;

namespace AzureClassLibrary.Links
{
    public abstract class BaseMessageQueue
    {
        public string Route { get; set; }

        public BaseMessageQueue(string route)
        {
            Route = route;
        }
    }
}
