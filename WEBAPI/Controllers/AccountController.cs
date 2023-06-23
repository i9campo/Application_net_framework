using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Sigma.App.Auxiliar;
using Sigma.App.Interfaces;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.ViewTables;
using Sigma.Infra.CrossCutting.Identity.Configuration;
using Sigma.Infra.CrossCutting.Identity.Model;
using WEBAPI.App_Start;
using WEBAPI.Models;

namespace WEBAPI
{
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly IUsuarioAtivoAppService _usuarioAtivoAppService;
        private readonly IRolesAppService _rolesAppService; 


        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IRolesAppService rolesAppService,  IUsuarioAppService usuarioAppService, IUsuarioAtivoAppService usuarioAtivoAppService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usuarioAppService = usuarioAppService;
            _usuarioAtivoAppService = usuarioAtivoAppService;
            _rolesAppService = rolesAppService; 
        }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await _userManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }


        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await _userManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await _userManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }



        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }


        [HttpPost]
        [ActionName("SendEmailRecuperacaoSenha")]
        [Route("api/Account/SendEmailRecuperacaoSenha")]
        /// Método utilizado para enviar e-mail de recuperação de senha. 
        /// POST : /Account/SendEmailRecuperacaoSenha/?email=
        public async Task<IHttpActionResult> SendEmailRecuperacaoSenha(String email)
        {
            if (!ModelState.IsValid)
                return Ok("OS DADOS INFORMADOS ENCONTRA-SE INCOMPLETO. ");

            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null)
                return Ok("E-MAIL INFORMADO NÃO FOI LOCALIZADO NA BASE DE DADOS. "); 


            var Claim = await _userManager.GetClaimsAsync(usuario.Id);

            var send = new Sender_Email();
            String code = EncodeClass.EncodeEmailString(usuario.Id, usuario.UserName);
            try
            {
                //string url = "<a href='https://sigma.siccerrado.com.br/#/rec_senha/" + code + "'> Aqui </a>";
                string url = "<a href='http://localhost:3000/rec_senha/" + code + "'> Aqui </a>";
                string img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTdcpdyayPp22A6Glk495zGyH9IlOUlyQQxi6p_NY06yeel3hB5zK3bJy8jUOwM1dnWSsw&usqp=CAU";
                string HTMLText = "<center> " +
                                    "<table>" +
                                      "<tr> <td><img src=" + img + " /></td> </tr>" +
                                      "<tr style='border: 1px solid black'> " +
                                        "<td> <b><h3> Olá, " + Claim[0].Value + "</h3></b> \n" +
                                             "<b><h3> Houve recentemente uma solicitação para alterar a senha em sua conta, baseado em seus registros.   </h3></b> \n" +
                                             "<b><h3> Para realizar a alteração de sua senha no Sigma Web, Clique " + url + " </h3></b> \n \n" +
                                             "<hr/>" +
                                             "\n <b><h2> Aproveite o Sigma Web! </h2></b>" +
                                       "</td>" +
                                     "</tr>" +
                                     " \n " +
                                     " \n " +
                                     " \n " +
                                    "</table>" +
                                  "</center>";
                send.SendMail(email, "RECUPERAR SENHA", HTMLText);

                return Ok("E-MAIL ENVIADO COM SUCESSO, CONFIRA SUA CAIXA DE ENTRADA OU SPAM. ");
            }
            catch (Exception)
            {
                return Ok("NÃO FOI POSSÍVEL REALIZAR O ENVIO DO E-MAIL, ENTRE EM CONTATO COM A EQUIPE DE SUPORTE. ");
            }
        }


        [HttpGet]
        [ActionName("ResetPassword")]
        [Route("api/Account/ResetPassword")]
        public Boolean ResetPassword(String obj)
        {
            try
            {
                //[0] E-mail.
                //[1] Senha.
                //[2] Token.

                var objeto = obj.Split('_');
                var decode = EncodeClass.DecodeEmailString(objeto[2]);

                var usuario = _userManager.FindById(decode[0].Id);
                if (usuario.Email.Equals(objeto[0]))
                {
                    _userManager.RemovePassword(usuario.Id);
                    _userManager.AddPassword(usuario.Id, objeto[1]);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        [ActionName("EmailExist")]
        [Route("api/Account/EmailExist")]
        public Boolean EmailExist(String Email)
        {
            UserView usuario = _usuarioAppService.FindUserByEmail(Email);
            if (usuario == null)
                return true;
            else
                return false;
        }

        [HttpPost]
        [ActionName("Register")]
        [Route("api/Account/Register")]
        public async Task<IHttpActionResult> Register([FromBody] RegisterViewModel obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                //var user = new ApplicationUser { UserName = obj.Email, Email = obj.Email, IDEmpresa = Guid.Parse("256b44ae-25e7-456f-9786-1814a5118b5e"), Ativo = obj.Ativo };
                var user = new ApplicationUser { UserName = obj.Email, Email = obj.Email };

                Claim claimName = new Claim("FirstName", obj.FirstName);
                Claim claimLName = new Claim("LastName", obj.LastName);

                var result = await _userManager.CreateAsync(user, obj.Password);

                if (!result.Succeeded)
                    return GetErrorResult(result);
                else
                {
                    _userManager.AddClaim(user.Id, claimName);
                    _userManager.AddClaim(user.Id, claimLName);

                    var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Sigma");
                    _userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));

                    // Aqui será feita uma verificação de quantos usuários existe no banco, 
                    // Caso exista somente 1 então o usuário será o Administrador. 
                    // Senão o usuário será somente usuário. 

                    IEnumerable<Usuario> checkedAllUsers = _usuarioAppService.GetAll();
                    if (checkedAllUsers.Count() == 1)
                        await _userManager.AddToRoleAsync(user.Id, "Administrador");
                    else
                        await _userManager.AddToRoleAsync(user.Id, "Usuário");

                    IEnumerable<Roles> AllRoles = _rolesAppService.GetAllRoles();
                    foreach (var item in AllRoles)
                    {
                        await _userManager.AddToRoleAsync(user.Id, item.Name);
                    }

                    try
                    {
                        UsuarioAtivo userActivate = new UsuarioAtivo();
                        userActivate.IDEmpresa = Guid.Parse("256b44ae-25e7-456f-9786-1814a5118b5e");
                        userActivate.Ativo = true;
                        userActivate.IDUsuario = user.Id;
                        _usuarioAtivoAppService.Add(userActivate);
                    }
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        //var provider = new DpapiDataProtectionProvider(user.Id);
                        //_userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
                        //var user = await _userManager.FindByEmailAsync(Email);
                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var Claim = await _userManager.GetClaimsAsync(user.Id);

                        String code = EncodeClass.EncodeString(user.Id, Claim[0].Value, Claim[1].Value, user.UserName);

                        var send = new Sender_Email();
                        string url = "<a href='https://sigma.siccerrado.com.br/#/confirmer/" + code + "'> Aqui </a>";
                        string equipe = "<a href='#'> equipe de suporte </a>";

                        string img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTdcpdyayPp22A6Glk495zGyH9IlOUlyQQxi6p_NY06yeel3hB5zK3bJy8jUOwM1dnWSsw&usqp=CAU";

                        string HTMLText = "<center> " +
                                            "<table>" +
                                              "<tr> <td> <img src=" + img + " /></td>  </tr>" +
                                              "<tr style='border: 1px solid black'> " +
                                                "<td> <center> <b><h1> Olá, " + Claim[0].Value + " " + Claim[1].Value + " </h1></b> </center> \n" +
                                                     "<center> <b><h2> Seja bem vindo à Sigma Web </h2></b> </center> \n" +

                                                     "<b><h2> Para verificarmos que o seu e-mail cadastrado em nosso site pertence a você mesmo, clique " + url + ".</h2></b> \n" +
                                                     "<b><h2> Se você tiver qualquer problema com o acesso em sua conta, entre em contato com nossa " + equipe + " . </h2></b>" +

                                                     "<hr/>" +

                                                     "<center> <b><h2> Obrigado por escolher nosso serviço. </h2></b> </center> \n" +
                                               "</td>" +
                                             "</tr>" +
                                             " \n " +
                                             " \n " +
                                             " \n " +
                                             "<tr><td><p>Este e-mail é automático. Por favor, não responda.</p></td></tr>" +
                                            "</table>" +
                                          "</center>";

                        send.SendMail(user.Email, "Confirmação de Email", HTMLText);
                    }
                    catch (Exception ex)
                    {
                    }

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar o cadastro do usuário. ");
            }
        }

        [HttpGet]
        [ActionName("EmailConfirm")]
        [Route("api/Account/EmailConfirm")]
        public bool EmailConfirm(string token)
        {
            //var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Sigma");
            //_userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
            //token = HttpUtility.UrlDecode(token);
            try
            {
                var decode = EncodeClass.DecodeString(token);
                var usuario = _userManager.FindById(decode[0].Id);

                var Claims = _userManager.GetClaims(decode[0].Id);

                if (decode[0].UserName.Equals(usuario.Email) && decode[0].FirstName.Equals(Claims[0].Value) && decode[0].LastName.Equals(Claims[1].Value))
                {
                    usuario.EmailConfirmed = true;
                    _userManager.Update(usuario);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        [HttpGet]
        [ActionName("ResendEmailConfirm")]
        [Route("api/Account/ResendEmailConfirm")]
        public async Task<IHttpActionResult> ResendEmailConfirm(string Email)
        {
            var user = _usuarioAppService.FindUserByEmail(Email);
            if (user != null && user.EmailConfirmed == false)
            {
                try
                {
                    //var provider = new DpapiDataProtectionProvider(user.Id);
                    //_userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
                    //var user = await _userManager.FindByEmailAsync(Email);
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var Claim = await _userManager.GetClaimsAsync(user.Id);

                    String code = EncodeClass.EncodeString(user.Id, Claim[0].Value, Claim[1].Value, user.UserName);

                    var send = new Sender_Email();
                    //string url = "<a href='https://sigmadev.sigma.local/#/confirmer/" + code + "'> Aqui </a>";
                    string url = "<a href='http://localhost:3000/#/confirmer/" + code + "'> Aqui </a>";
                    string equipe = "<a href='#'> equipe de suporte </a>";

                    string img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTdcpdyayPp22A6Glk495zGyH9IlOUlyQQxi6p_NY06yeel3hB5zK3bJy8jUOwM1dnWSsw&usqp=CAU";

                    string HTMLText = "<center> " +
                                        "<table>" +
                                          "<tr> <td> <img src=" + img + " /></td>  </tr>" +
                                          "<tr style='border: 1px solid black'> " +
                                            "<td> <center> <b><h1> Olá, " + Claim[0].Value + " " + Claim[1].Value + " </h1></b> </center> \n" +
                                                 "<center> <b><h2> Seja bem vindo à Sigma Web </h2></b> </center> \n" +

                                                 "<b><h2> Para verificarmos que o seu e-mail cadastrado em nosso site pertence a você mesmo, clique " + url + ".</h2></b> \n" +
                                                 "<b><h2> Se você tiver qualquer problema com o acesso em sua conta, entre em contato com nossa " + equipe + " . </h2></b>" +

                                                 "<hr/>" +

                                                 "<center> <b><h2> Obrigado por escolher nosso serviço. </h2></b> </center> \n" +
                                           "</td>" +
                                         "</tr>" +
                                         " \n " +
                                         " \n " +
                                         " \n " +
                                         "<tr><td><p>Este e-mail é automático. Por favor, não responda.</p></td></tr>" +
                                        "</table>" +
                                      "</center>";

                    send.SendMail(user.Email, "Confirmação de Email", HTMLText);
                    return Ok("Sucess");
                }
                catch (Exception ex)
                {
                    return Ok("Error");
                }
            }
            if (user == null)
            {
                return Ok("Email não encontrado");
            }
            if (user.EmailConfirmed == true)
            {
                return Ok("Email Ativo");
            }
            return Ok();
        }


        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
                return InternalServerError();

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
                return GetErrorResult(result);

            result = await _userManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
                return GetErrorResult(result);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return InternalServerError();

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        if (error.Contains("taken"))
                            ModelState.AddModelError("model.Email", error.Replace("Name ", "").Replace("is already taken.", "já possui cadastro!"));
                        else
                            ModelState.AddModelError("Default", error);
                    }
                }

                if (ModelState.IsValid)
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }
        #endregion
    }
}
