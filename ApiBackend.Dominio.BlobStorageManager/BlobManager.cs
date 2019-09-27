using Apibackend.Trasversal.DTOs;
using ApiBackend.Transversal.DTOs.PLC;
using System.IO;

namespace ApiBackend.Dominio.BlobStorageManager
{
    public class BlobManager
    {
        Infraestructura.Agent.BlobStorage.BlobStorageManager blobStorageManager;
        
        public BlobManager(string connectionBlob, string containeBlob)
        {
            blobStorageManager = new Infraestructura.Agent.BlobStorage.BlobStorageManager(connectionBlob, containeBlob);
            
        }

        public UsersBlob GetAllBlobs()
        {
            return blobStorageManager.GetAllBlobsStorage();
        }
        public void SaveBlobstorage(ProfilePhoto profile, string userId)
        {
            blobStorageManager.SaveBlobstorage(profile, userId);
        }
        public byte[] DownloadBlobstorage(string userId)
        {
            return blobStorageManager.DownloadBlobStorage(userId);
        }
        
    }
}

