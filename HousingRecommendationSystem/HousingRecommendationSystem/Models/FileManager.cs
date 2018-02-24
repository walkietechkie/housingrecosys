using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HousingRecommendationSystem
{
    public class FileManager : IFileManager
    {
        private CloudBlobContainer _container;
        public FileManager()
        {
            Initialize();
        }
        private void Initialize()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                                    CloudConfigurationManager.GetSetting("walkietechkie_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            _container = blobClient.GetContainerReference("walkietechkie-container");
            _container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
        }

        public string GetClipsFilePath()
        {
            // Retrieve reference to a blob named "ClipsScript.clp".
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference("ClipsScript.clp");

            var tempPath = Path.GetTempPath() + "\\ClipsScript.clp";
            // Save blob contents to a file.
            using (var fileStream = File.OpenWrite(tempPath))
            {
                blockBlob.DownloadToStream(fileStream);
            }

            return tempPath;
        }
    }
}