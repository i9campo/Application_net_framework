using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI.Controllers
{
    [AllowedOriginFilter]
    public class EmpresaController : ApiController
    {
        // GET api/<controller>
        private readonly IEmpresaAppService _empresaAppService;
        private readonly IUsuarioAtivoAppService _usuarioAtivoAppservico;
        public EmpresaController(IEmpresaAppService empresaAppService, IUsuarioAtivoAppService usuarioAtivoAppService)
        {
            _empresaAppService = empresaAppService;
            _usuarioAtivoAppservico = usuarioAtivoAppService;
            
        }
        public IEnumerable<Empresa> Get()
        {
            Guid IDUsuario = Guid.Parse(User.Identity.GetUserId());
            return _empresaAppService.GetListEmpresa(IDUsuario);
        }

        // GET api/<controller>/5
        public Empresa Get(Guid objID)
        {
            return _empresaAppService.Find(objID);
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public ValidationResult Put(Guid objID, [FromBody] Empresa obj)
        {
            return _empresaAppService.Update(obj);
        }


        [HttpGet]
        [ActionName("CheckedEmpresaActivateByUsuario")]
        [Route("api/empresa/CheckedEmpresaActivateByUsuario")]
        public bool CheckedEmpresaActivateByUsuario(Guid IDUsuario)
        {
            return _empresaAppService.CheckedEmpresaActivateByUsuario(IDUsuario); 
        }

        [HttpPut]
        [ActionName("DisableEmpresa")]
        [Route("api/empresa/DisableEmpresa")]
        public ValidationResult DisableEmpresa(Guid IDEmpresa)
        {
            Empresa obj = _empresaAppService.Find(IDEmpresa);
            obj.ativo = !obj.ativo;

            var updateempresa = _empresaAppService.Update(obj);

            // Essa primeira verifiação será realizada caso a empresa seja desativada ou ativada. 
            if (updateempresa.IsValid)
            {
                IEnumerable<UsuarioAtivoView> oUsuarioAtivo = _empresaAppService.GetUserActivate(IDEmpresa);
                foreach (var item in oUsuarioAtivo)
                {
              
                    // Caso eu ative a empresa, será ativado somente o usuário proprietário. 
                    // Caso contrario desativa todos os usuários. 
                    if ( item.ViewerRoler.Equals("PROPRIETARIO") && obj.ativo == true)
                    {
                        item.Ativo = true;
                        item.Conectado = false;

                        // Utilização do Mapper para converter uma View Model em uma classe do tipo Domain. 
                        UsuarioAtivo oUserActivate = Mapper.Map<UsuarioAtivoView, UsuarioAtivo>(item); 
                        var updateUsuarioAtivo = _usuarioAtivoAppservico.Update(oUserActivate);

                        // Essa verificação será realizada caso aconteça algum erro na ativação ou desativação do usuário ativo. 
                        if (!updateUsuarioAtivo.IsValid)
                            return updateUsuarioAtivo;

                    }else if (obj.ativo == false)
                    {
                        item.Ativo = false;
                        item.Conectado = false; 

                        UsuarioAtivo oUserActivate = Mapper.Map<UsuarioAtivoView, UsuarioAtivo>(item);
                        var updateUsuarioAtivo = _usuarioAtivoAppservico.Update(oUserActivate);

                        if (!updateUsuarioAtivo.IsValid)
                            return updateUsuarioAtivo;
                    }
                }
            }
                
            return updateempresa;
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}