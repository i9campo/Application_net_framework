using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Cors;
using System.Web.Http.Cors;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;


[assembly: OwinStartup(typeof(WEBAPI.App_Start.Startup))]
namespace WEBAPI.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var appSettings = WebConfigurationManager.AppSettings;

            if (!string.IsNullOrWhiteSpace(appSettings["cors:Origins"]))
            {
                // Load CORS settings from Web.config
                var corsPolicy = new EnableCorsAttribute(
                    appSettings["cors:Origins"],
                    appSettings["cors:Headers"],
                    appSettings["cors:Methods"]);

                // Enable CORS for ASP.NET Identity
                app.UseCors(new CorsOptions
                {
                    PolicyProvider = new CorsPolicyProvider
                    {
                        PolicyResolver = request =>
                            request.Path.Value == "/token" ?
                            corsPolicy.GetCorsPolicyAsync(null, CancellationToken.None) :
                            Task.FromResult<CorsPolicy>(null)
                    }
                });

                // Enable CORS for Web API
                //app.UseCors(new CorsOptions() { };
            }

            ConfigureAuth(app);
        }
    }
}