using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class ProprietarioController : ApiController
    {
        private readonly IProprietarioAppService _proprietarioAppService;
        private readonly IEmpresaAppService _empresaAppService; 
        public ProprietarioController(IProprietarioAppService proprietarioAppService, IEmpresaAppService empresaAppService)
        {
            _proprietarioAppService = proprietarioAppService;
            _empresaAppService = empresaAppService; 
           
        }

        /// <type>HttpGet</type>
        /// <link>'/proprietario/'</link>
        public IEnumerable<Proprietario_Viewer> Get(Guid? IDUsuario)
        {
            //Guid IDUsuario = Guid.Parse(User.Identity.GetUserId()); 
            return _proprietarioAppService.GetAllProprietario(Guid.Parse(IDUsuario.ToString()));
        }

        /// <param name="objID"></param>
        /// <type>HttpGet</type>
        /// <link>'/proprietario/', {params : {objID : 'params.name'}}</link>
        public Proprietario Get(Guid objID)
        {
            return _proprietarioAppService.Find(objID);
        }

        [HttpGet]
        [ActionName("getPfPj")]
        [Route("api/proprietario/getPfPj")]
        public IEnumerable<ProprietarioView> GetPfPj(string registro)
        {
            return _proprietarioAppService.GetPfPj(registro);
        }

        [HttpGet]
        [ActionName("getbysafra")]
        [Route("api/proprietario/getbysafra")]
        public IEnumerable<Proprietario> GetBySafra(Guid IDSafra)
        {
            //Guid IDUsuario = Guid.Parse(User.Identity.GetUserId());
            Guid IDUsuario = Guid.Parse("8bcd1c24-1028-4f09-94b7-fc2985b75ef5");
            return _proprietarioAppService.GetBySafra(IDSafra, IDUsuario).ToList();
        }


        [HttpGet]
        [ActionName("GetProprietarioBNGBySafra")]
        [Route("api/proprietario/GetProprietarioBNGBySafra")]
        public IEnumerable<BNGProprietario> GetProprietaioBNGSafra(Guid IDSafra)
        {
            return _proprietarioAppService.GetProprietaioBNGSafra(IDSafra); 
        }


        /// <param name="obj"></param>
        /// <type>HttpPost</type>
        /// <link>'/proprietario/', obj </link>
        public ValidationResult Post([FromBody] Proprietario obj)
        {
            //obj.IDEmpresa = ((Empresa)_empresaAppService.GetEmpresa(Guid.Parse(User.Identity.GetUserId().ToString()))).objID; 

            obj.IDEmpresa = ((Empresa)_empresaAppService.GetEmpresa(Guid.Parse("a4c9d59b-b4b2-499e-8c2c-47ab4e636e2b"))).objID; 

            return _proprietarioAppService.Add(obj);
        }

        /// <param name="objID"></param>
        /// <param name="obj"></param>
        /// <type>HttpPut</type>
        /// <link>'/proprietario/' + objID, obj</link>
        public ValidationResult Put(Guid objID, [FromBody] Proprietario obj)
        {
            Proprietario p = _proprietarioAppService.Find(objID);

            Auxiliar.CloneObject.CopyLinqObject(obj, p);

            return _proprietarioAppService.Update(p);
        }

        /// <param name="objID"></param>
        /// <type>HttpDelete</type>
        /// <link>'/proprietario/ + objID</link>
        public ValidationResult Delete(Guid objID)
        {
            Proprietario obj = _proprietarioAppService.Find(objID);
            return _proprietarioAppService.Remove(obj);
        }
    }
}