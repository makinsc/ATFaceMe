using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace ApiBackend.Controllers
{
    public abstract class ApiBaseController : ApiController
    {
        protected string UserId
        {
            get
            {
                if (User == null) return string.Empty;
                if (User.Identity == null) return string.Empty;
                var user = User.Identity as ClaimsIdentity;
                var userId = user.Claims.FirstOrDefault(a => a.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier") != null ?
                    user.Claims.FirstOrDefault(a => a.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value : string.Empty;
                return userId;
            }
        }

        protected string LanguageFromUrl
        {
            get { return Request != null ? ControllerContext.RouteData.Values["lang"].ToString() : "es"; }
        }

        protected string VersionFromUrl
        {
            get { return Request != null ? Request.RequestUri.Segments[2].Trim('/').ToString() : "v1"; }
        }

        protected string TokenRequest
        {
            get { return ActionContext.Request.Headers.Authorization != null ? ActionContext.Request.Headers.Authorization.Parameter : string.Empty; }
        }
    }
}
