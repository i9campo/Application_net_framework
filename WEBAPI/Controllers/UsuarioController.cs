using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Sigma.App.Auxiliar;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.ViewTables;
using Sigma.Infra.CrossCutting.Identity.Configuration;
using Sigma.Infra.CrossCutting.Identity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class UsuarioController : ApiController
    {
        private Guid UserId { get; set; }
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly IUsuarioAtivoAppService _usuarioAtivoAppService;
        private ApplicationUserManager _userManager;
        private readonly IRolesAppService _rolesAppService;
        private readonly IProprietarioAppService _proprietarioAppService;
        private readonly IEmpresaAppService _empresaAppService; 
        public UsuarioController(IUsuarioAppService usuarioAppService, ApplicationUserManager userManager, IProprietarioAppService proprietarioAppService, IUsuarioAtivoAppService usuarioAtivoAppService, IRolesAppService rolesAppService, IEmpresaAppService empresaAppService)
        {
            _usuarioAppService = usuarioAppService;
            _userManager = userManager;
            _usuarioAtivoAppService = usuarioAtivoAppService;
            _rolesAppService = rolesAppService;
            _proprietarioAppService = proprietarioAppService;
            _empresaAppService = empresaAppService; 
        }
        #region Consultas e buscas. 
        // api/usuario
        public IEnumerable<Usuario> Get()
        {
            // Retorna todos os usuário. 
            return _usuarioAppService.GetAll();
        }
        
        [HttpGet]
        [ActionName("GetEmailConfirmer")]
        [Route("api/Usuario/GetEmailConfirmer")]
        // api/usuario/confirmemail. 
        public bool GetEmailConfirmer(String Email)
        {
            // Este método será utilizado para verificar a confirmação de E-mail do usuário sé já foi realizada ou não. 
            // Com o objetivo de bloquear o acesso do usuário, caso o usuário não tenha confirmado o email. 
            var Usuario = _usuarioAppService.FindUserByEmail(Email);
            return bool.Parse(Usuario.EmailConfirmed.ToString());
        }


        [HttpGet]
        [ActionName("gettypeuser")]
        [Route("api/usuario/gettypeuser")]
        public UserView GetUser()
        {
            // Retorna os dados do usuário logado. 
            UserView objeto = new UserView();
            if (User.Identity.GetUserId() != null)
                objeto = _usuarioAppService.FindUser(Guid.Parse(User.Identity.GetUserId().ToString()));

            return objeto;
        }

        [HttpGet]
        [ActionName("getusuarioativo")]
        [Route("api/Usuario/GetUsuarioAtivo")]
        // api/usuario/getusuarioativo.
        public bool UsuarioAtivo(String Email)
        {
            // Este método será utilizado para verificar sé o usuário está ativo ou não. 
            UserView usuario = _usuarioAppService.FindUserByEmail(Email);
            UsuarioAtivo userActive = _usuarioAtivoAppService.FindTypeUser(usuario.Id);
            return userActive.Ativo;
        }


        [HttpGet]
        [ActionName("getuserauthentication")]
        [Route("api/usuario/getuserauthentication")]
        // api/usuario/getuserauthentication
        public bool GetUserAuthentication()
        {
            // Este método será utilizado para carregar as informações sobre o usuário, sé ele encontra-se conectado ou não. 

            var IdUser = User.Identity.GetUserId();
            if (IdUser != null)
            {
                var usuario = _usuarioAtivoAppService.FindTypeUser(IdUser);
                if (usuario == null)
                    return false;

                return usuario.Conectado;
            }
            else
                return false; 
        }

        [HttpGet]
        [ActionName("checkedemailexist")]
        [Route("api/usuario/checkedemailexist")]
        // api/usuario/checkedemailexist.
        public bool CheckedEmailExist(String Email)
        {
            /// Aqui será verificado sé o email informado pelo usuário existe ou não na base de dados. 
            return _usuarioAppService.FindCheckedUserEmail(Email);
        }
        #endregion

        #region Autenticação/ Verifiações e Confirmações. 
        [HttpGet]
        [ActionName("confirmemail")]
        [Route("api/usuario/confirmemail")]
        // api/usuario/confirmemail. 
        public bool ConfirmEmail(String Token)
        {
            // Este método será utilizado para fazer a verificação do usuário e atualizar o seu registro de confirmação para true. 
            // O tratamento de erro será utilizado porque se acontecer algum erro no processo ele retornará false. 
            // Agora se os dados do Token estiver diferente a autenticação não será válida então o retorno também será false. 
            try
            {
                // Aqui será feito uma decodificação no Token encaminhado via 'URL'. 
                var decode = EncodeClass.DecodeString(Token);

                // Após a decodificação do Token, será feito uma busca do usuário na base de dados. 
                var usuario = _userManager.FindById(decode[0].Id);

                // Para segurança da informação será verificado o nome do usuário também, junto ao Token. 
                var Claims = _userManager.GetClaims(decode[0].Id);

                // Sé os valores forem iguais, a confirmação de email poderá ocorrer com sucesso. 
                if (decode[0].UserName.Equals(usuario.Email) && decode[0].FirstName.Equals(Claims[0].Value) )
                {
                    usuario.EmailConfirmed = true;
                    _userManager.Update(usuario);
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }


        [HttpGet]
        [ActionName("ConfirmerEmail")]
        [Route("api/Usuario/ConfirmerEmail")]
        public async Task<IHttpActionResult> ResendEmailConfirm(string Email)
        {
            var user = _usuarioAppService.FindUserByEmail(Email);

            if (user != null && user.EmailConfirmed == false)
            {
                try
                {
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
                }
                catch (Exception ex)
                {
                    return Ok("Error");
                }
            }
            if (user == null)
            {
                return Ok("E-MAIL INFORMADO NÃO EXISTE NA BASE DE DADOS.");
            }
            if (user.EmailConfirmed == true)
            {
                return Ok("E-MAIL INFORMADO JÁ ENCONTRA-SE CONFIRMADO, ACESSE NOVAMENTE OU ENTRE EM CONTATO COM O SUPORTE. ");
            }
            return Ok("E-MAIL ENCAMINHADO COM SUCESSO, AGORA CONFIRA SEU E-MAIL NA CAIXA DE ENTRADA OU NA CAIXA DE SPAM. ");
        }


        #endregion

        #region Validação, Alterações e Cadastros.

        [HttpPut]
        [ActionName("ResetPassword")]
        [Route("api/Usuario/ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(String obj)
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
                else
                {
                    return Ok("false");
                }
            }
            catch (Exception ex)
            {
                return Ok("false");
            }
            return Ok("true");
        }

        [HttpPost]
        [ActionName("createuser")]
        [Route("api/usuario/createuser")]
        // api/usuario/createuser. 
        public async Task<IHttpActionResult> CreateUser([FromBody] RegisterViewModel obj)
        {
            if (!ModelState.IsValid)
                return Ok("Certifique que todos os campos foram preenchidos corretamente.");

            try
            {
                // Aqui será gerado um objeto Users, para que seja armazenado ao banco de dados na tabela AspNetUsers. 
                var user = new ApplicationUser { UserName = obj.Email, Email = obj.Email };

                // Aqui será gerado dois objetos CLAIM, onde será armazenado o nome do usuário. 
                Claim claimName = new Claim("FirstName", obj.FirstName);

                // Aqui será o processo de armazenamento do usuário na tabela AspNetUser. 
                var result = await _userManager.CreateAsync(user, obj.Password);

                if (!result.Succeeded)
                    return Ok("Certifique que todos os campos foram preenchidos corretamente. ");
                else
                {
                    _userManager.AddClaim(user.Id, claimName);

                    var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Sigma");
                    _userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));

                    Empresa emp = new Empresa();

                    Guid userID = Guid.Parse(User.Identity.GetUserId());
                    var oEmpresa = _empresaAppService.GetEmpresa(userID);

                    if (obj.Cliente == true)
                    {
                        try
                        {
                            emp.objID = Guid.NewGuid();
                            emp.nome = claimName.Value;
                            emp.site = "";
                            emp.fone = "";
                            emp.dataCadastro = DateTime.Now ;
                            emp.ativo = true;
                            _empresaAppService.Add(emp);
                        }
                        catch (Exception)
                        {

                        }

                        try
                        {
                            Proprietario prop = _proprietarioAppService.Find(Guid.Parse(obj.IDCliente));
                            prop.IDEmpresa = emp.objID;
                            _proprietarioAppService.Update(prop); 
                        }
                        catch (Exception)
                        {
                            return Ok("Entre em contato com o suporte técnico, pois ocorreu erro de atualização de cadastro de cliente para usuário. ");
                        }
                    }

                    try
                    {
                        //IEnumerable<Usuario> checkedAllUsers = _usuarioAppService.GetAll();
                        //if (checkedAllUsers.Count() == 1)
                        //    await _userManager.AddToRoleAsync(user.Id, "Administrador");
                        //else
                            await _userManager.AddToRoleAsync(user.Id, "Usuário");

                        IEnumerable<Roles> AllRoles = _rolesAppService.GetAllRoles();
                        foreach (var item in AllRoles)
                        {
                            await _userManager.AddToRoleAsync(user.Id, item.Name);
                        }
                    }
                    catch (Exception)
                    {
                        return Ok("Nenhuma permissão foi registrada no banco de dados, entre em contato com o suporte técnico. ");
                    }

                    try
                    {
                        UsuarioAtivo userActivate = new UsuarioAtivo();

                        userActivate.IDEmpresa = obj.Cliente == true ?  emp.objID : oEmpresa.objID;
                        userActivate.Ativo = true;
                        userActivate.IDUsuario = user.Id;
                        userActivate.Conectado = false;

                        _usuarioAtivoAppService.Add(userActivate);
                    }
                    catch (Exception ex)
                    {
                        return Ok("Não foi possível registrar o usuário ativo, entre em contato com o suporte técnico. ");
                    }

                    try
                    {
                        var Claim = await _userManager.GetClaimsAsync(user.Id);

                        String code = EncodeClass.EncodeString(user.Id, Claim[0].Value, "", user.UserName);

                        var send = new Sender_Email();
                        //string url = "<a href='https://sigma.siccerrado.com.br/#/confirmer/" + code + "'> Aqui </a>";
                        string url = "<a href='http://localhost:3000/#/confirmer/" + code + "'> Aqui </a>";
                        string equipe = "<a href='#'> equipe de suporte </a>";

                        string img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTdcpdyayPp22A6Glk495zGyH9IlOUlyQQxi6p_NY06yeel3hB5zK3bJy8jUOwM1dnWSsw&usqp=CAU";

                        string HTMLText = "<center> " +
                                            "<table>" +
                                              "<tr> <td> <img src=" + img + " /></td>  </tr>" +
                                              "<tr style='border: 1px solid black'> " +
                                                "<td> <center> <b><h1> Olá, " + Claim[0].Value + " </h1></b> </center> \n" +
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
                        return Ok("Não foi possível enviar o email de confirmação de cadastro, entre em contato com o suporte técnico. ");
                    }
                }
                return Ok(user.Id);
            }
            catch (Exception error)
            {
                return Ok("Não foi possível completar o registro do usuário, entre em contato com o suporte técnico. ");
            }

        }

        #endregion 


        [HttpPut]
        [ActionName("activateuser")]
        [Route("api/ususario/activateuser")]
        // api/usuario/activateuser
        public ValidationResult ActivateUser(UserActivateView obj)
        {
            //Este método será utilizado para fazer a ativação do usuário, 
            //Para que o usuário administrador ou usuário que tem permissão a visualização dos usuários, possa visualizar quem estar conectados. 

            UserView usuario = _usuarioAppService.FindUserByEmail(obj.Email);

            if (usuario == null)
                return null; 

            UsuarioAtivo userActive = _usuarioAtivoAppService.FindTypeUser(usuario.Id);

            userActive.Conectado = obj.Conectar; 
            return _usuarioAtivoAppService.Update(userActive); 
        }

        


        #region Usuário
        [HttpGet]
        [ActionName("FindUserByEmail")]
        [Route("api/usuario/FindUserByEmail")]
        public UserView FindUserByEmail(String Email)
        {
            return _usuarioAppService.FindUserByEmail(Email);
        }

        [HttpGet]
        [ActionName("CheckedUserLogin")]
        [Route("api/Usuario/CheckedUserLogin")]
        public bool CheckeUserLogin()
        {
            try
            {
                // Caso o usuário ainda esteja logado. 
                // retorna true; 
                UserId = Guid.Parse(User.Identity.GetUserId());
                return true;
            }
            catch (Exception)
            {
                // Caso o usuário não esteja logado. 
                // retorna false. 
                return false;
            }
        }

        [HttpGet]
        [ActionName("RecuperacaoSenha")]
        [Route("api/Usuario/RecuperacaoSenha")]
        public Boolean RecuperacaoSenha(string currentPassword, string newPassword)
        {
            var userId = User.Identity.GetUserId();
            var senha = _userManager.ChangePassword(userId, currentPassword, newPassword);
            if (!senha.Succeeded)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        [HttpGet]
        [ActionName("GetAllUser")]
        [Route("api/usuario/GetAllUser")]
        public IEnumerable<UserView> GetAllConectionUsers()
        {
            UserId = Guid.Parse(User.Identity.GetUserId());
            return _usuarioAppService.GetAllConectionUsers(UserId).OrderBy(o => o.NomeEmpresa).ToList(); ;
        }

        [HttpGet]
        [ActionName("UserView")]
        [Route("api/usuario/UserView")]
        public UserView FindUser(Guid objId)
        {
            return _usuarioAppService.FindUser(objId);
        }
        #endregion

        #region Roles 
        [HttpGet]
        [ActionName("FindTypeUser")]
        [Route("api/Usuario/FindTypeUser")]
        public UserView FindTypeUser()
        {
            try
            {
                UserId = Guid.Parse(User.Identity.GetUserId());
                return _usuarioAppService.FindUser(UserId);
            }
            catch (Exception ex)
            {
                UserView objeto = new UserView();
                return objeto;
            }
        }

        [HttpGet]
        [ActionName("GetManagerPermission")]
        [Route("api/Usuario/GetManagerPermission")]
        public IEnumerable<Roles> GetManagerPermission()
        {
            return _rolesAppService.GetManagerPermission();
        }

        [HttpGet]
        [ActionName("CreateTablePermission")]
        [Route("api/Usuario/CreateTablePermission")]
        public List<List<UserView>> CreateTablePermission(Guid UserId)
        {
            /// Este código gera os checkbox da tela de permissão.
            /// Obtendo os ID's das roles ("view, novo, edit e excluir") referente a cada View como: "Culturas","Cadastros" etc...  
            /// Para facilitar na hora de selecionar e criar a permissão para o usuário selecionado. 

            string permissionName = "";
            int cont = 0;
            var UserIdLogado = Guid.Parse(User.Identity.GetUserId());

            var listrole = _rolesAppService.GetAllRolesByUser(UserId).OrderBy(o => o.ViewerRoler).ToList();
            var listroleLogado = _rolesAppService.GetAllRolesByUser(UserIdLogado).OrderBy(o => o.ViewerRoler).ToList();

            List<List<UserView>> oMatriz = new List<List<UserView>>();
            List<UserView> oRoles = new List<UserView>();
            bool result;

            for (int i = 0; i < listrole.Count(); i++)
            {
                UserView obj = new UserView();
                if(listrole[i].Name == "View_cad_bt")
                {
                    string sd = "a";
                }
                var user = _usuarioAppService.CheckedRole(listrole[i].Name, UserId.ToString());
                var userLogado = _usuarioAppService.CheckedRole(listrole[i].Name, UserIdLogado.ToString());
                if(userLogado != null)
                {
                    if(userLogado == true)
                    {

                        if (!listrole[i].ViewerRoler.Equals(permissionName) && !String.IsNullOrEmpty(permissionName))
                        {
                            oMatriz.Add(oRoles);
                        }
                        if (!listrole[i].ViewerRoler.Equals(permissionName))
                        {
                            obj = listrole[i];
                            if (obj.Checado.Equals("0"))
                            {
                                obj.UserId = UserId.ToString();
                                obj.Checked = true;

                            }
                            if (obj.Checado.Equals("1"))
                            {
                                result = Convert.ToBoolean(obj.Checked);
                                obj.Checked = false;
                                obj.UserId = UserId.ToString();

                            }
                            permissionName = listrole[i].ViewerRoler;
                            oRoles = new List<UserView>();
                        }
                        else
                        {

                            obj = listrole[i];
                            if (obj.Checado.Equals("0"))
                            {
                                obj.Checked = true;
                                obj.UserId = UserId.ToString();

                            }
                            if (obj.Checado.Equals("1"))
                            {
                                result = Convert.ToBoolean(obj.Checked);
                                obj.Checked = false;
                                obj.UserId = UserId.ToString();

                            }
                            permissionName = listrole[i].ViewerRoler;
                        }
                        oRoles.Add(obj);
                        cont += 1;

                        if (listrole.ToList().Count == cont)
                        {
                            // Aqui será adicionado a última lista de roles. 
                            oMatriz.Add(oRoles);
                        }
                     }

                }
            }
            return oMatriz;
        }
        

        [HttpGet]
        [ActionName("GetPermission")]
        [Route("api/Usuario/GetPermission")]
        public List<UserView> GetPermission()
        {
            // Este método será utilizado para retornar uma lista de permissão referente ao usuário logado. 
            try
            {
                UserId = Guid.Parse(User.Identity.GetUserId());
                return _rolesAppService.GetAllRolesByUser(UserId).ToList();
            }
            catch (Exception)
            {
                // O usuário ainda não logou 
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetRoles")]
        [Route("api/Usuario/GetRoles")]
        public IEnumerable<Roles> GetRoles()
        {
            return _rolesAppService.GetAllRoles();
        }

        [HttpGet]
        [ActionName("GetRolesUser")]
        [Route("api/Usuario/GetRolesUser")]
        public IEnumerable<UserView> GetRolesUser(Guid UserId)
        {
            return _rolesAppService.GetAllRolesByUser(UserId).ToList();
        }

        [HttpGet]
        [ActionName("GetAllRoles")]
        [Route("api/Usuario/GetAllRoles")]
        public IEnumerable<Roles> GetAllRoles()
        {
            return _rolesAppService.GetAllRoles();
        }


        [HttpGet]
        [ActionName("CheckedPermission")]
        [Route("api/Usuario/CheckedPermission")]
        public Boolean CheckedPermission(String Roles)
        {
            UserId = Guid.Parse(User.Identity.GetUserId());
            if (_usuarioAppService.CheckedRole(Roles, UserId.ToString()) == null)
                return false;
            else
                return true;
        }

        [HttpGet]
        [ActionName("URLPermission")]
        [Route("api/Usuario/URLPermission")]
        public Boolean URLPermission(String Roles, String Email)
        {
            var Id = User.Identity.GetUserId();
            if (Id == null)
                Id = _usuarioAppService.FindUserByEmail(Email).Id;

            if (Id != null)
            {
                UserId = Guid.Parse(Id);
                if (_usuarioAppService.CheckedRole(Roles, UserId.ToString()) == true)
                    return true;
                else
                    return false;
            }
            return false; 
        
        }
        #endregion

        #region Cadastro/ Recuperação/ Confimarção de E-Mail


        [HttpPut]
        [ActionName("GetUpdateRoles")]
        [Route("api/Usuario/GetUpdateRoles")]
        public bool GetUpdateRoles(Object Roles)
        {
            var a = Roles.ToString();

            var obj = JsonConvert.DeserializeObject<List<List<UserView>>>(a);
            try
            {
                for (int x = 0; x < obj.Count; x++)
                {
                    for (int y = 0; y < obj[x].Count; y++)
                    {
                        var RolesUser = obj[x][y];
                        if(RolesUser != null) { 
                            if (RolesUser.Checked == true)
                            {
                                PutRemoveAsync(RolesUser.Name, RolesUser.UserId);
                            }
                            if (RolesUser.Checked == false)
                            {
                                PutAddAsync(RolesUser.Name, RolesUser.UserId);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
          

           
        }


        public async Task<ValidationResult> PutRemoveAsync(String RoleId, string UserId)
        {

            try
            {
                var proprietario = _usuarioAppService.FindUser(Guid.Parse(UserId));
                if(proprietario.ViewerRoler == "PROPRIETARIO") {     
                    List<UserView> empresas = _usuarioAppService.GetUserEmpresa(Guid.Parse(proprietario.IDEmpresa.ToString())).ToList();
                    for (int i = 0; i < empresas.Count(); i++)
                    {
                        if(empresas[i].ViewerRoler == "USER") { 
                            _userManager.RemoveFromRoles(empresas[i].UserId, RoleId);
                        }
                    }
                }
                _userManager.RemoveFromRoles(UserId, RoleId);

            }
            catch (Exception ex)
            {

            }



            return null;
        }

        public async Task<ValidationResult> PutAddAsync(String RoleId, string UserId)
        {

            try
            {
                _userManager.AddToRole(UserId, RoleId);
            }
            catch (Exception ex )
            {

            }


            return null;
        }

        [HttpPut]
        [ActionName("UpdateClaim")]
        [Route("api/Usuario/UpdateClaim")]
        public async Task<IHttpActionResult> UpdateClaim([FromBody] UserView obj)
        {
            Claim Nome = new Claim("FirstName", obj.Nome);
            var FirstName = _userManager.GetClaims(obj.Id)[0];
            var removeFirst = await _userManager.RemoveClaimAsync(obj.Id, FirstName);
            if (!removeFirst.Succeeded)
                return null;
            else
            {
                _userManager.AddClaim(obj.Id, Nome);
                return Ok();
            }
        }

        [HttpGet]
        [ActionName("UpdateUser")]
        [Route("api/Usuario/UpdateUser")]
        public IEnumerable<UserRoles> UpdateUser(string Roles, string UserId, bool ativo)
        {
            UsuarioAtivo user = _usuarioAtivoAppService.FindTypeUser(UserId);
            user.Ativo = ativo;
            if (Roles == "Administrador")
            {
                this.PutRemoveAsync("Usuário", UserId);
                this.PutAddAsync(Roles, UserId);
                _usuarioAtivoAppService.Update(user);
            }
            if (Roles == "Usuário" || Roles == "Usuario")
            {
                this.PutRemoveAsync("Administrador", UserId);
                this.PutAddAsync(Roles, UserId);

                _usuarioAtivoAppService.Update(user);
            }
            return null;

        }

        [HttpGet]
        [ActionName("UserAtivo")]
        [Route("api/Usuario/UserAtivo")]
        public UsuarioAtivo UsuarioAtivo(Guid UserId)
        {
            return _usuarioAtivoAppService.UsuarioAtivo(UserId);
        }


        [HttpPut]
        [ActionName("inactivateUser")]
        [Route("api/Usuario/inactivateUser")]
        public ValidationResult inactivateUser(String UserId)
        {
            UsuarioAtivo user = _usuarioAtivoAppService.GetAll().Where(or => or.IDUsuario.Equals(UserId)).FirstOrDefault();
            user.Ativo = !user.Ativo;
            if(user.Ativo == false && user.Conectado == true)
            {
                Put(user.IDUsuario);
            }
            return _usuarioAtivoAppService.Update(user);
        }


        [HttpPut]
        public ValidationResult Put(string conection)
        {
            try
            {
                if (conection == "false")
                {
                    String UserId = User.Identity.GetUserId().ToString();
                    UsuarioAtivo user = _usuarioAtivoAppService.GetAll().Where(or => or.IDUsuario.Equals(UserId)).FirstOrDefault();
                    user.Conectado = false;
                    _usuarioAtivoAppService.Update(user);
                }

                if (conection == "true")
                {
                    String UserId = User.Identity.GetUserId().ToString();
                    UsuarioAtivo user = _usuarioAtivoAppService.GetAll().Where(or => or.IDUsuario.Equals(UserId)).FirstOrDefault();
                    user.Conectado = true;
                    _usuarioAtivoAppService.Update(user);
                }
                if (conection != "true" && conection != "false")
                {
                    UsuarioAtivo user = _usuarioAtivoAppService.GetAll().Where(or => or.IDUsuario.Equals(conection)).FirstOrDefault();
                    user.Conectado = !user.Conectado;
                    _usuarioAtivoAppService.Update(user);
                }
            }
            catch (Exception e) { }

            return null;


        }



        [HttpPost]
        [ActionName("RegisterRoles")]
        [Route("api/Usuario/RegisterRoles")]
        public bool RegisterRoles([FromBody] Roles roles)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    Roles obj = new Roles();
                    if (i == 0)
                    {
                        Guid objID = Guid.NewGuid();
                        obj.Id = objID.ToString();
                        obj.Name = "Edit_" + roles.Name + "";
                        obj.Tipo = roles.Tipo;
                        obj.ViewerRoler = roles.ViewerRoler;
                        _rolesAppService.Add(obj);
                        //_userManager.AddToRole("a4c9d59b-b4b2-499e-8c2c-47ab4e636e2b", obj.Id);

                    }
                    if (i == 1)
                    {
                        Guid objID1 = Guid.NewGuid();
                        obj.Id = objID1.ToString();
                        obj.Name = "Novo_" + roles.Name + "";
                        obj.Tipo = roles.Tipo;
                        obj.ViewerRoler = roles.ViewerRoler;
                        _rolesAppService.Add(obj);
                        //_userManager.AddToRole("a4c9d59b-b4b2-499e-8c2c-47ab4e636e2b", obj.Id);
                    }
                    if (i == 2)
                    {
                        Guid objID2 = Guid.NewGuid();
                        obj.Id = objID2.ToString();
                        obj.Name = "View_" + roles.Name + "";
                        obj.Tipo = roles.Tipo;
                        obj.ViewerRoler = roles.ViewerRoler;
                        _rolesAppService.Add(obj);
                        //_userManager.AddToRole("a4c9d59b-b4b2-499e-8c2c-47ab4e636e2b", obj.Id);
                    }
                    if (i == 3)
                    {
                        Guid objID3 = Guid.NewGuid();
                        obj.Id = objID3.ToString();
                        obj.Name = "Del_" + roles.Name + "";
                        obj.Tipo = roles.Tipo;
                        obj.ViewerRoler = roles.ViewerRoler;
                        _rolesAppService.Add(obj);
                        //_userManager.AddToRole("a4c9d59b-b4b2-499e-8c2c-47ab4e636e2b", obj.Id);

                    }

                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        [HttpPut]
        [ActionName("UpdateRoles")]
        [Route("api/Usuario/UpdateRoles")]
        public bool UpdateRoles([FromBody] Roles roles)
        {
            try
            {
                _rolesAppService.Update(roles);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }


        [HttpGet]
        [ActionName("DeleteRoles")]
        [Route("api/Usuario/DeleteRoles")]
        public bool DeleteRoles(Guid Id)
        {
            return _usuarioAtivoAppService.DeleteRoles(Id);

        }


    }
    #endregion
}
