using System.Net.Http;
using System.Net;
using System.Web.Http.Controllers;
using System;
using System.Web.Http.Filters;
using System.Linq;

namespace WEBAPI.App_Start
{
    public class AllowedOriginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Retorna uma resposta de erro ou redireciona para uma página específica
            if (!IsAllowedOrigin(actionContext.Request))
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Acesso não autorizado.");
        }
        private bool IsAllowedOrigin(HttpRequestMessage request)
        {
            try
            {
                string origin = request.Headers.GetValues("Origin")?.FirstOrDefault();
                // Verificar se a URL de origem corresponde à esperada
                if (origin == "http://localhost:3000")
                    return string.Equals(origin, "http://localhost:3000", StringComparison.OrdinalIgnoreCase);
                else
                    return string.Equals(origin, "https://sigma-web.vercel.app", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception)
            {

                return false; 
            }
        }
    }
}