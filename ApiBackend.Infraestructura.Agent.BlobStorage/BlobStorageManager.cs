using Apibackend.Trasversal.DTOs;
using ApiBackend.Transversal.DTOs.PLC;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

namespace ApiBackend.Infraestructura.Agent.BlobStorage
{
    public class BlobStorageManager
    {
        string _storageConection;
        string _storageContainer;
        public BlobStorageManager(string accountConection, string blobContainer)
        {
            _storageConection = accountConection;
            _storageContainer = blobContainer;
        } 

        public void SaveBlobstorage(ProfilePhoto photo, string userId)
        {
            try
            {
                CloudStorageAccount storageAccount =
                    CloudStorageAccount.Parse(_storageConection);

                // Create the CloudBlobClient that is used to call the Blob Service for that storage account.
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a container called 'quickstartblobs'. 
                CloudBlobContainer cloudBlobContainer =
                    cloudBlobClient.GetContainerReference(_storageContainer);

                //"image/jpeg"
                string extension = "jpg";
                if (photo.OdataMediaContentType.ToLower().Split('/')[1] != "jpeg")
                {
                    extension = photo.OdataMediaContentType.ToLower().Split('/')[1];
                }

                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(userId + "." + extension);

                blockBlob.UploadFromStream(photo.photobytes, photo.photobytes.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] DownloadBlobStorage( string userId)
        {
            byte[] photo;
            try
            {
                CloudStorageAccount storageAccount =
                    CloudStorageAccount.Parse(_storageConection);

                // Create the CloudBlobClient that is used to call the Blob Service for that storage account.
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a container called 'quickstartblobs'. 
                CloudBlobContainer cloudBlobContainer =
                    cloudBlobClient.GetContainerReference(_storageContainer);

                //"image/jpeg"
                string extension = "jpg";
                
                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(userId + "." + extension);
                MemoryStream st = new MemoryStream();
                blockBlob.DownloadToStream(st);
                photo = st.ToArray();
                st.Dispose();
                return photo;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public UsersBlob GetAllBlobsStorage()
        {
            UsersBlob result = new UsersBlob();
            try
            {
                CloudStorageAccount storageAccount =
                    CloudStorageAccount.Parse(_storageConection);

                // Create the CloudBlobClient that is used to call the Blob Service for that storage account.
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a container called 'quickstartblobs'. 
                CloudBlobContainer cloudBlobContainer =
                    cloudBlobClient.GetContainerReference(_storageContainer);

                var blobList = cloudBlobContainer.ListBlobs();
                foreach(IListBlobItem elem in blobList)
                {
                    string[] listpath = elem.Uri.AbsoluteUri.Split('/');
                    //cojo la ultima
                    string file = listpath[listpath.Length - 1];
                    result.listaIdUsers.Add(file.Split('.')[0]);
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
