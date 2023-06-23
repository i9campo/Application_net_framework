using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Sigma.App.Auxiliar;
using Sigma.Infra.CrossCutting.Identity.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WEBAPI.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private string IDUser { get; set; }
        public ApplicationOAuthProvider(string publicClientId)
        {

            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            try
            {
                //userManager.AddPassword("8bcd1c24-1028-4f09-94b7-fc2985b75ef5", "@Michel.123"); 
                var oUsuario = await userManager.FindAsync(context.UserName, context.Password);
                if (oUsuario == null)
                {
                    context.SetError("invalid_grant", "Email ou senha está incorreto");
                    return; 
                }

                var claims = userManager.GetClaims(oUsuario.Id);

                ClaimsIdentity oAuthIdentity = await oUsuario.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookiesIdentity = await oUsuario.GenerateUserIdentityAsync(userManager, CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = CreateProperties(oUsuario.Email, claims);

                AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);

                IDUser = oUsuario.Id; 
            }
            catch (Exception ex)
            {
                context.SetError(ex.Message);
                return;
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            context.AdditionalResponseParameters.Add("IDUser", EncodeClass.EncodeGUID(IDUser)); 

            return Task.FromResult<object>(context);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string Email, IList<Claim> claims)
        {
            IDictionary<string, string> data = new Dictionary<string, string> { { "Email", Email } };
            foreach (var claim in claims)
            {
                data.Add(claim.Type, claim.Value);
            }

            return new AuthenticationProperties(data);
        }
    }
}