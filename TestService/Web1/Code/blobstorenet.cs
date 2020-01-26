using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web1.Code
{

    public class BlobEntity
    {
        public string name { get; set; }
        //public string url { get; set; }


        public BlobEntity()
        {

        }
        public override string ToString() => $"{name}";
    }

    public class blobstorenet
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
            //var listOfFileNames = new List<string>();

            foreach (var blob in list)
            {
                var blobFileName = blob.Uri.Segments.Last();
                var blobFileUrl = blob.Uri.OriginalString;
                //var blobFileDate = ;

                listOfFileNames.Add(new BlobEntity { name = blobFileName });
                //listOfFileNames.Add(blobFileName);
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

        public static void sendByteArrayToBlob(string filename, byte[] byteArray)
        {

            try
            {
                var container = GetClient();

                container.CreateIfNotExists();

                if (container.Exists())
                {
                    // takenshot is the name of the file that is saved to the blob account
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);

                    try
                    {
                        using (MemoryStream memoryStream = new MemoryStream(byteArray))
                        {
                            blockBlob.UploadFromStream(memoryStream);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    Console.WriteLine("Container Does not exist");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
