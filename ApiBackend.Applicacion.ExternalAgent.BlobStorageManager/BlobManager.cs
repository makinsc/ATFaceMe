using Apibackend.Trasversal.DTOs;
using ApiBackend.Transversal.DTOs.PLC;
using System.IO;

namespace ApiBackend.Applicacion.ExternalAgent.BlobStorageManager
{
    public class BlobManager
    {
        ApiBackend.Dominio.BlobStorageManager.BlobManager blobStorageManager;

        public BlobManager(string connectionBlob, string containeBlob)
        {
            blobStorageManager = new Dominio.BlobStorageManager.BlobManager(connectionBlob, containeBlob);

        }

        public UsersBlob GetAllBlobs()
        {
            return blobStorageManager.GetAllBlobs();
        }
        public void SaveBlobstorage(ProfilePhoto profile, string userId)
        {
            blobStorageManager.SaveBlobstorage(profile, userId);
        }

        public byte[] DownloadBlobStorage( string userId)
        {
            return blobStorageManager.DownloadBlobstorage(userId);
        }
        
    }
}
