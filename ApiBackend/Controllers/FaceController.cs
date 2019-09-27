using ApiBackend.Applicacion.ExternalAgent;
using ApiBackend.Application.Core.MSGraphAuth.Helpers;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiBackend.Controllers
{
    [RoutePrefix("api/Face")]
    [Authorize]
    public class FaceController : ApiBaseController
    {
        ExternalAgent externalServices = new ExternalAgent(ConfigurationManager.AppSettings["PhotoBlobStorageConnection"],
           ConfigurationManager.AppSettings["PhotoBlobStorageContainer"]);

        [HttpPost]
        [Route("Identify", Name = "Identify")]
        // Get the current user's email address from their profile.
        public async Task<IHttpActionResult> Identify()
        {
            try
            {               
                var tokenResult = await SampleAuthProvider.Instance.GetUserAccessTokenAsync();                
                HttpRequestMessage request = this.Request;

                MemoryStream stream = new MemoryStream(await Request.Content.ReadAsByteArrayAsync());

                // Obtiene los datos actuales del usuario incluida la foto. 
                var identifyResult = await externalServices.Identify(stream, tokenResult.AccessToken);
                return Json(identifyResult);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("Train", Name = "Train")]
        public async Task<IHttpActionResult> Train()
        {
            try
            {                
                var user = await externalServices.Train();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }       
    }
}
