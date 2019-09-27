using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ApiBackend.Startup))]
namespace ApiBackend
{
   
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Invoca el metodo ConfigureAuth el cual pondrá el pipeline de OWIN para que use OAuth 2.0
            ConfigureAuth(app);
        }
    }
}