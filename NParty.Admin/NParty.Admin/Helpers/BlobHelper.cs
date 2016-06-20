using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace NParty.Admin.Helpers
{
    public class BlobHelper
    {
        private static BlobHelper _instance;

        public static BlobHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BlobHelper();
                }

                return _instance;
            }
        }

        public string UploadImage(System.IO.Stream stream, string referenceName)
        {
            Bitmap original = (Bitmap)Image.FromStream(stream);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("ESportsBlobServer"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(referenceName);

            System.IO.Stream newStream = ImageHelper.GetStream(ImageHelper.ResizeImage(original, ImageSize.Big), ImageBlobFormat.Jpeg, 90);
            newStream.Position = 0;

            blockBlob.UploadFromStream(newStream);
            return blockBlob.Uri.AbsoluteUri;
        }
    }
}