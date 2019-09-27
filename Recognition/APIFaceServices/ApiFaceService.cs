using ApiBackend.Transversal.DTOs.PLC.ResultDTO;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBackend.Infraestructura.Agent.Recognition.APIFaceServices
{
    public class ApiFaceService
    {

        // Api Face data
        private const string subscriptionKey = "6c2c65fd79b0481bb641dbdb1b0f273b";
        private const string subscriptionEndpoint = "https://westeurope.api.cognitive.microsoft.com/face/v1.0";

        // Group person data
        private const string groupPersonKey = "ee5ec5bc-8f05-46fd-9074-1d9f924bf2c0";
        private const string groupPersonName = "ATGroup";

        // Service is needed to call the Api Face
        private static FaceServiceClient faceService = new FaceServiceClient(subscriptionKey, subscriptionEndpoint);

        /// <summary>
        /// Create a new Person Group with the groupPersonKey and the groupPersonName by default
        /// </summary>
        public async Task<string> CreatePersonGroup()
        {
            try
            {
                await faceService.DeletePersonGroupAsync(groupPersonKey);
                await faceService.CreatePersonGroupAsync(groupPersonKey, groupPersonName);
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(FaceAPIException) &&
                    ((FaceAPIException)e).ErrorCode.Equals("PersonGroupNotFound"))
                {
                    try
                    {
                        await faceService.CreatePersonGroupAsync(groupPersonKey, groupPersonName);
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                else
                {
                    return e.Message;
                }
            }
            return "OK";
        }

        /// <summary>
        /// Create a new person and add his first photo
        /// </summary>
        /// <param name="personName">The name of the person</param>
        /// <param name="photoStream">The data stream for the photo</param>
        public async Task<string> CreatePersonAndAddPhoto(string personName, Stream photoStream, string IdOffice365)
        {
            CreatePersonResult newPerson = null;
            try
            {
                newPerson = await faceService.CreatePersonAsync(groupPersonKey, personName, IdOffice365);
                if (photoStream != null)
                {
                    await faceService.AddPersonFaceAsync(groupPersonKey, newPerson.PersonId, photoStream);
                }
                return "OK";
            }
            catch (FaceAPIException fe)
            {
                if (newPerson != null)
                {
                    await faceService.DeletePersonAsync(groupPersonKey, newPerson.PersonId);
                }
                return fe.ErrorMessage;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        /// <summary>
        /// Train the person group. This method need to be called before identifying after every change.
        /// </summary>
        public async Task<string> TrainPersonGroup()
        {
            try
            {
                await faceService.TrainPersonGroupAsync(groupPersonKey);

                // Wait until train completed
                while (true)
                {                    
                    var status = await faceService.GetPersonGroupTrainingStatusAsync(groupPersonKey);
                    if (status.Status != Status.Running)
                    {
                        break;
                    }
                    await Task.Delay(1000);
                }

                return "OK";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Identify the person in the person group that appear in the photo. 
        /// </summary>
        /// <param name="photoStream">The data stream for the photo</param>
        /// <returns>The user data of the person, which include Office365 ID</returns>
        public async Task<KeyValuePair<IdentifyResultCode, string>> Identify(Stream photoStream)
        {
            var result = new KeyValuePair<IdentifyResultCode, string>(IdentifyResultCode.ERROR_NO_PERSON, "");

            var faces = new Face[0];

            // First, we need to detect the faces in the photo
            try
            {
                long a = photoStream.Length;
                faces = await faceService.DetectAsync(photoStream);
            }
            catch (Exception e)
            {
                long b = photoStream.Length;
            }

            // We identify only if exists a single face in the photo
            if (faces.Length == 1)
            {
                try
                {
                    var identifyResult = await faceService.IdentifyAsync(groupPersonKey, faces.Select(ff => ff.FaceId).ToArray());
                    var candidate = identifyResult[0].Candidates[0];
                    var personIdentified = await faceService.GetPersonAsync(groupPersonKey, candidate.PersonId);

                    // For the final application, we need to change this for the Office365 ID
                    result = new KeyValuePair<IdentifyResultCode, string>(IdentifyResultCode.PERSON_FOUND, personIdentified.UserData);
                }
                catch (Exception e)
                {
                    result = new KeyValuePair<IdentifyResultCode, string>(IdentifyResultCode.ERROR_UNKNOWN_PERSON, "");
                }

            }
            else if (faces.Length > 1)
            {
                result = new KeyValuePair<IdentifyResultCode, string>(IdentifyResultCode.ERROR_TOO_MANY_FACES, "");
            }

            return result;
        }

        /// <summary>
        /// Get all persons in api face Group.
        /// </summary>
        /// <returns>a list of @PersonFace</returns>
        public async Task<List<Apibackend.Trasversal.DTOs.PersonFace>> GetAllPersonIngroup()
        {
            List<Apibackend.Trasversal.DTOs.PersonFace> result = new List<Apibackend.Trasversal.DTOs.PersonFace>();            
            try
            {
                var personsList = await faceService.ListPersonsAsync(groupPersonKey);
                //convertimos el tipo porque es especifico de la libreria
                personsList.ToList().ForEach(a => result.Add(new Apibackend.Trasversal.DTOs.PersonFace() { Name = a.Name, PersistedFaceIds = a.PersistedFaceIds, PersonId = a.PersonId, UserData = a.UserData }));
            }
            catch (Exception e)
            {
                
            }
            return result;
        }

    }
}
