using Apibackend.Trasversal.DTOs;
using ApiBackend.Dominio.ExternalAgents.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Apibackend.Trasversal.DTOs.RequestDTO;
using ApiBackend.Transversal.DTOs.PLC.ResultDTO;
using ApiBackend.Dominio.BlobStorageManager;

namespace ApiBackend.Applicacion.ExternalAgent
{
    public class ExternalAgent
    {
        ExternalAgents domain;
        BlobManager blobManager;
        public ExternalAgent(string blobConection, string blobContainer)
        {
            domain = new ExternalAgents();
            blobManager = new BlobManager(blobConection,blobContainer);
        }

        public async Task<PaginatedUserDetail> getAllusersByFilter(string accessToken, GetAllUserByFilterRequest filterRequest)
        {
            string filter = null;
            if(!string.IsNullOrWhiteSpace(filterRequest.UserName))
            {
                filter += "startswith(givenName,'" + filterRequest.UserName + "')";
                if (!string.IsNullOrWhiteSpace(filterRequest.Surname))
                {
                    filter += "and ";
                }
            }
            if(!string.IsNullOrWhiteSpace(filterRequest.Surname))
            {
                filter += "startswith(surname,'" + filterRequest.Surname + "')";
            }                
            var result = await domain.getAllusersByFilter(accessToken, filter);
            if (!string.IsNullOrEmpty(filterRequest.OfficeLocation))
            {
                result.value = result.value
                .Where(x => !string.IsNullOrEmpty(x.officeLocation) && 
                            x.officeLocation.StartsWith(filterRequest.OfficeLocation))
                .ToList();
            }
            
            var listaBlobs = blobManager.GetAllBlobs();
            result.value.ForEach(a => a.hasPhoto = listaBlobs.listaIdUsers.Contains(a.id));
            return result;
        }

        public async Task<PaginatedUserDetail> getAllusers(string accessToken, string skipToken=null)
        {
            var listaBlobs = blobManager.GetAllBlobs();
            
            var result = await domain.getAllPaginatedUsers(accessToken, skipToken);
            result.value.ForEach(a => a.hasPhoto = listaBlobs.listaIdUsers.Contains(a.id));
            return result;
        }

        public async Task<ProfilePhoto> getUserPhoto(string accessToken, string userId)
        {
            return await domain.getUserPhoto(accessToken, userId);
        }

        public async Task<UserDetail> getUsersById(string idUser, string accessToken)
        {
            return await domain.getUsersById(idUser, accessToken);
        }

        public async Task<IdentifyResult> Identify(Stream image, string accessToken)
        {
            return await domain.Identify(image, accessToken);
        }

        public async Task<string> Train()
        {
            return await domain.TrainGroup();
        }

        public async Task<string> CreateNewPersonFace(Stream image, string name, string idOffice365)
        {
            return await domain.CreateNewPersonFace(image, name, idOffice365);
        }

        public async Task<string> RestartGroup()
        {
            return await domain.RestartGroup();
        }

        public async Task<List<PersonFace>> GetAllPersonIngroup()
        {
            return await domain.GetAllPersonIngroup();
        }
    }
}
