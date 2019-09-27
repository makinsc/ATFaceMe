using Apibackend.Trasversal.DTOs.RequestDTO;
using ApiBackend.Applicacion.ExternalAgent;
using ApiBackend.Application.Core.MSGraphAuth.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using ApiBackend.Transversal.DTOs.PLC;
using System.Configuration;

namespace ApiBackend.Controllers
{
    [RoutePrefix("api/User")]
    [Authorize]
    public class UserController : ApiBaseController
    {        
        ExternalAgent externalServices = new ExternalAgent(ConfigurationManager.AppSettings["PhotoBlobStorageConnection"],
            ConfigurationManager.AppSettings["PhotoBlobStorageContainer"]);

        [HttpGet]
        [Route(Name = "Get")]
        // Get the current user's email address from their profile.
        public async Task<IHttpActionResult> Get()
        {
            try
            {               
                var tokenResult = await SampleAuthProvider.Instance.GetUserAccessTokenAsync();
                // Obtiene los datos actuales del usuario incluida la foto. 
                var user = await externalServices.getUsersById(UserId, tokenResult.AccessToken);
                return Ok(user);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);   
            }
        }

        [HttpGet]
        [Route("GetById/{id}", Name = "GetById")]
        public async Task<IHttpActionResult> GetById(string id)
        {
            try
            {
                var tokenResult = await SampleAuthProvider.Instance.GetUserAccessTokenAsync();

                var user = await externalServices.getUsersById(id, tokenResult.AccessToken);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        [HttpGet]
        [Route("GetAll", Name = "GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var tokenResult = await SampleAuthProvider.Instance.GetUserAccessTokenAsync();
                string skipToken = null; 
                if(Request.Headers.Contains(Endpoints.HeaderSkipToken))
                {
                    skipToken = Request.Headers.GetValues(Endpoints.HeaderSkipToken).FirstOrDefault();
                }

                // Obtiene los datos actuales del usuario incluida la foto. 
                var user = await externalServices.getAllusers(tokenResult.AccessToken,skipToken);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("GetAllByFilter", Name = "GetAllByFilter")]
        public async Task<IHttpActionResult> GetAllByFilter(GetAllUserByFilterRequest filter)
        {
            if (!ModelState.IsValid || filter == null)
            { return BadRequest(); }
            try
            {
                var tokenResult = await SampleAuthProvider.Instance.GetUserAccessTokenAsync();
                // Obtiene los datos actuales del usuario incluida la foto. 
                var user = await externalServices.getAllusersByFilter(tokenResult.AccessToken, filter);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
