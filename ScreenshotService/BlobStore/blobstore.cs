using AzureClassLibrary;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScreenshotService.BlobStore
{
    public static class blobstore
    {

        public static CloudBlobContainer GetClient()
        {

            const string storageAccountConnectionString = "DefaultEndpointsProtocol=https;AccountName=detectifystore;AccountKey=+DXM1TNsJSOmVTisoP4ky940R9S2Y+2prxmrnRtzM3jDTeOYI5L4votugmkPM9JoEnWG0DgcE+v0mo17l1vs4w==;EndpointSuffix=core.windows.net";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageAccountConnectionString);


            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("screenshots");

            return container;
        }

        public static List<BlobEntity> GetBlobList()
        {
            CloudBlobContainer container = GetClient();

            var list = container.ListBlobs();

            var listOfFileNames = new List<BlobEntity>();

            foreach (var blob in list)
            {
                var blobFileName = blob.Uri.Segments.Last();
                
                listOfFileNames.Add(new BlobEntity { name = blobFileName});
            }


            return listOfFileNames;
        }

        public static void SaveBlobToPC(string filename)
        {
            CloudBlobContainer container = GetClient();

            var blob = container.GetBlockBlobReference(filename);

            using (var fileStream = File.OpenWrite($"D:\\{filename}.jpeg"))
            {
                blob.DownloadToStream(fileStream);
            }
        }
    }
}
