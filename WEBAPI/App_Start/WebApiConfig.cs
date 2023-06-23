using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using FluentValidation.WebApi;
using Microsoft.Owin.Security.OAuth;
using WEBAPI.App_Start;

namespace WEBAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new AllowedOriginFilter()); 
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "api",
                routeTemplate: "api/{controller}/{objID}",
                defaults: new { objID = RouteParameter.Optional }
            );


            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}
