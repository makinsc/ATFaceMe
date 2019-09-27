using Apibackend.Trasversal.DTOs;
using Microsoft.Identity.Client;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiBackend.Application.Core.MSGraphAuth.Helpers
{
    public sealed class SampleAuthProvider : IAuthProvider
    {

        // Properties used to get and manage an access token.
        private string redirectUri = ConfigurationManager.AppSettings["ida_RedirectUri"];
        private string appId = ConfigurationManager.AppSettings["ida_AppId"]; 
        private string appSecret = ConfigurationManager.AppSettings["ida_AppSecret"];
        private string scopes = ConfigurationManager.AppSettings["ida_GraphScopes"];
        private static readonly SampleAuthProvider instance = new SampleAuthProvider();
        private SampleAuthProvider() { }

        public static SampleAuthProvider Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Obtiene un token de acceso, no sin antes tratar de recuperarlo de caché
        /// </summary>
        /// <returns></returns>
        public async Task<AuthResult> GetUserAccessTokenAsync()
        {            
            // Get the raw token that the add-in page received from the Office host.
            var bootstrapContext = ClaimsPrincipal.Current.Identities.First().BootstrapContext
                as BootstrapContext;
            UserAssertion userAssertion = new UserAssertion(bootstrapContext.Token);

            // Get the access token for MS Graph. 
            ClientCredential clientCred = new ClientCredential(appSecret);
            ConfidentialClientApplication cca =
                new ConfidentialClientApplication(appId,
                                                    redirectUri, clientCred, null, null);

            AuthenticationResult result = null;
            try
            {
                // The AcquireTokenOnBehalfOfAsync method will first look in the MSAL in memory cache for a
                // matching access token. Only if there isn't one, does it initiate the "on behalf of" flow
                // with the Azure AD V2 endpoint.
                result = await cca.AcquireTokenOnBehalfOfAsync(scopes.Split(' '), userAssertion, "https://login.microsoftonline.com/fa97be54-b037-491c-93bc-61111ddb87d9/oauth2/v2.0");
                return new AuthResult() { AccessToken = result.AccessToken, UserId = result.User.Identifier };
            }
            catch (MsalUiRequiredException e)
            {
                throw e;
            }            
        }

        public async Task<AuthResult> GetUserAccessTokenAsync(string token)
        {
            // Get the raw token that the add-in page received from the Office host.

            UserAssertion userAssertion = new UserAssertion(token);
            // Get the access token for MS Graph. 
            ClientCredential clientCred = new ClientCredential(appSecret);
            ConfidentialClientApplication cca =
                new ConfidentialClientApplication(appId,
                                                    redirectUri, clientCred, null, null);

            AuthenticationResult result = null;
            try
            {
                // The AcquireTokenOnBehalfOfAsync method will first look in the MSAL in memory cache for a
                // matching access token. Only if there isn't one, does it initiate the "on behalf of" flow
                // with the Azure AD V2 endpoint.
                result = await cca.AcquireTokenOnBehalfOfAsync(scopes.Split(' '), userAssertion, "https://login.microsoftonline.com/fa97be54-b037-491c-93bc-61111ddb87d9/oauth2/v2.0");
                return new AuthResult() { AccessToken = result.AccessToken, UserId = result.User.Identifier, idToken = result.IdToken , ExpiresOn = result.ExpiresOn};
            }
            catch (MsalUiRequiredException e)
            {
                throw e;
            }
        }
    }
}