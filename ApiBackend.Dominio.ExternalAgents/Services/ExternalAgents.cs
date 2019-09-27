using Apibackend.Trasversal.DTOs;
using ApiBackend.Infraestructura.Agent.Recognition.APIFaceServices;
using ApiBackend.Infraestructura.Agents.MSGraph;
using ApiBackend.Transversal.DTOs.PLC.ResultDTO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ApiBackend.Dominio.ExternalAgents.Services
{
    public class ExternalAgents
    {
        MSGraphService msGraph;
        ApiFaceService apiFace;

        public ExternalAgents()
        {
            msGraph = new MSGraphService();
            apiFace = new ApiFaceService();
        }

        public async Task<PaginatedUserDetail> getAllusersByFilter(string accessToken, string filter)
        {
            return await msGraph.getAllusersByFilter(accessToken,filter);
        }

        public async Task<PaginatedUserDetail> getAllPaginatedUsers(string accessToken, string skiptoken = null)
        {
            return await msGraph.getAllPaginatedUsers(accessToken, skiptoken);
        }

        public async Task<ProfilePhoto> getUserPhoto(string accessToken, string userId)
        {
            return await msGraph.GetProfilePhoto(userId, accessToken);
        }

        public async Task<UserDetail> getUsersById(string idUser, string accessToken)
        {
            return await msGraph.getUser(idUser, accessToken);
        }
        public async Task<string> TrainGroup()
        {
            return await apiFace.TrainPersonGroup();
        }
        public async Task<IdentifyResult> Identify(Stream image,string accessToken)
        {
            IdentifyResult result = new IdentifyResult();

            var identifyResult = await apiFace.Identify(image);

            result.ResultCode = identifyResult.Key;

            if(identifyResult.Key == IdentifyResultCode.PERSON_FOUND)
            {
                result.User = await getUsersById(identifyResult.Value, accessToken);
            }

            return result;
        }

        public async Task<string> RestartGroup()
        {
            return await apiFace.CreatePersonGroup();
        }

        public async Task<string> CreateNewPersonFace(Stream image, string name, string idOffice365)
        {
            return await apiFace.CreatePersonAndAddPhoto(name, image, idOffice365);            
        }

        public async Task<List<PersonFace>> GetAllPersonIngroup()
        {
            return await apiFace.GetAllPersonIngroup();
        }

    }
}
