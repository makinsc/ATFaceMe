using ApiBackend.App_Start;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Configuration;
using System.IdentityModel.Tokens;

namespace ApiBackend
{
    public partial class Startup
    {
        private static string clientId = ConfigurationManager.AppSettings["ida_Audience"];

        public void ConfigureAuth(IAppBuilder app)
        {

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            var tvps = new TokenValidationParameters
            {                
                //En esta aplicación, el cliente son aplicaciones nativas en xamarin y deben estar configuradas con el mismo
                //AppId para representarlas y aceptar el token enviado desde el cliente

                ValidAudience = clientId,
                ValidIssuer = "https://login.microsoftonline.com/common/v2.0",
                SaveSigninToken = true,
                // Se debería validar si el usuario es de Atsistemas
                ValidateIssuer = false,
            };

            // seteamos el pipeline de autenticación de OWIN para que use autenticación por token Bearer OAuth 2.0.
            // Las opciones proporcionadas aquí le dicen al middleware sobre el tipo de tokens
            // que se recibirá, que son JWT para el punto final v2.0.

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AccessTokenFormat = new Microsoft.Owin.Security.Jwt.JwtFormat(
                    tvps,
                    new OpenIdConnectCachingSecurityTokenProvider("https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration")),
            });
        }
    }
}
