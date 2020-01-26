using System;
using System.Collections.Generic;
using System.Text;

namespace AzureClassLibrary
{
    public class BlobEntity
    {
        public string name { get; set; }

        public BlobEntity()
        {

        }
        public override string ToString() => $"{name}";
    }
}
