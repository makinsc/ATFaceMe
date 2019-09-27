using Apibackend.Trasversal.DTOs;
using ApiBackend.Infraestructura.Agents.MSGraph.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ApiBackend.Infraestructura.Agents.MSGraph
{
    public class MSGraphService
    {

        /// <summary>
        /// Get the current user's profile photo.
        /// </summary>
        /// <param name="IdUser"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<ProfilePhoto> GetProfilePhoto(string IdUser, string accessToken)
        {
            // Get the profile photo of the current user (from the user's mailbox on Exchange Online). 
            // This operation in version 1.0 supports only a user's work or school mailboxes and not personal mailboxes. 
            string endpoint = "https://graph.microsoft.com/v1.0/users/"+ IdUser + "/photo/$value";
            ProfilePhoto result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint))
                    {
                        //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                        var response = await client.SendAsync(request);

                        // If successful, Microsoft Graph returns a 200 OK status code and the photo's binary data. If no photo exists, returns 404 Not Found.
                        if (response.IsSuccessStatusCode)
                        {
                            result = await GetImageFormatPhoto(IdUser, accessToken);
                            result.photobytes = await response.Content.ReadAsStreamAsync();

                            return result;
                        }
                        else
                        {
                            // If no photo exists, the sample uses a local file.
                            return null;//File.OpenRead(System.Web.Hosting.HostingEnvironment.MapPath("/Content/test.jpg"));
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
           
        }

        //Devuelve las propiedades de la imagen sin el array de bytes
        /// <summary>
        /// Ge image properties without byte array.
        /// </summary>
        /// <param name="IdUser"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<ProfilePhoto> GetImageFormatPhoto(string IdUser, string accessToken)
        {

            // Get the profile photo of the current user (from the user's mailbox on Exchange Online). 
            // This operation in version 1.0 supports only a user's work or school mailboxes and not personal mailboxes. 
            string endpoint = "https://graph.microsoft.com/v1.0/users/" + IdUser + "/photo";
            try
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint))
                    {
                        //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                        var response = await client.SendAsync(request);

                        // If successful, Microsoft Graph returns a 200 OK status code and the photo's binary data. If no photo exists, returns 404 Not Found.
                        if (response.IsSuccessStatusCode)
                        {
                            string stringResult = await response.Content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<ProfilePhoto>(stringResult);                            
                        }
                        else
                        {
                            // If no photo exists, the sample uses a local file.
                            return null;//File.OpenRead(System.Web.Hosting.HostingEnvironment.MapPath("/Content/test.jpg"));
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Get all users filtered by name and surname
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="filter">startswith(givenName,'jos') and startswith(surname,'sal')</param>
        /// <returns></returns>
        public async Task<PaginatedUserDetail> getAllusersByFilter(string accessToken, string filter)
        {
            string endpoint = "https://graph.microsoft.com/v1.0/users?$filter="+filter;
            PaginatedUserDetail result = null;
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string stringResult = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<PaginatedUserDetail>(stringResult);

                            result.value = result.value.OrderBy(x => x.displayName)
                                                    .ToList();

                            return result;
                        }
                        return result;
                    }
                }
            }
        }

        public async Task<PaginatedUserDetail> getAllPaginatedUsers(string accessToken, string skiptoken = null)
        {
            string endpoint = "https://graph.microsoft.com/v1.0/users?$orderby=displayName";
            if(!string.IsNullOrEmpty(skiptoken))
            {
                endpoint = skiptoken;
            }
            PaginatedUserDetail result = null;
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string stringResult = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<PaginatedUserDetail>(stringResult);

                            //foreach (UserDetail user in result.value)
                            //{
                            //    user.photo = await GetImageFormatPhoto(user.id, accessToken);
                            //    user.hasPhoto = user.photo != null;
                            //}
                            return result;
                        }
                        return result;
                    }
                }
            }
        }


        public async Task<UserDetail> getUser(string IdUser, string accessToken)
        {
            string endpoint = "https://graph.microsoft.com/v1.0/users/"+ IdUser ;
            UserDetail result = null;
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string stringResult = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<UserDetail>(stringResult);                            
                            var photo = await GetProfilePhoto(result.id, accessToken);
                            result.hasPhoto = photo != null;
                            return result;
                        }
                        return result;
                    }
                }
            }
        }
    }
}
