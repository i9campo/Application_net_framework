using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class ServicoController : ApiController
    {
        private readonly IServicoAppService _servicoAppService;
        public ServicoController(IServicoAppService servicoAppService)
        {
            _servicoAppService = servicoAppService;
        }

        // GET api/servico
        public IEnumerable<Servico> Get()
        {
            return _servicoAppService.GetAllServico();
        }

        [HttpGet]
        [ActionName("getbyareasafra")]
        [Route("api/servico/getbyareasafra")]
        public IEnumerable<Servico> GetByAreaSafra(Guid IDArea, Guid IDSafra)
        {
            return _servicoAppService.GetByAreaSafra(IDArea, IDSafra);
        }


        [HttpGet]
        [ActionName("getservico")]
        [Route("api/servico/getservico")]
        public IEnumerable<AreaServicoView> GetServico(String IDArea, String IDSafra)
        {
            return _servicoAppService.GetServico(IDArea, IDSafra);
        }


        //GET api/servico
        public Servico Get(Guid objID)
        {
            return _servicoAppService.Find(objID);
        }

        // POST api/servico
        public ValidationResult Post([FromBody] Servico obj)
        {
            obj.objID = Guid.NewGuid();
            if(obj.tipoTaxa == null)
            {
                obj.tipoTaxa = "";
            }
            if(obj.descricao == null)
            {
                obj.descricao = "";
            }
            return _servicoAppService.Add(obj);
        }

        // PUT api/servico/5
        public ValidationResult Put(string objID, [FromBody] Servico obj)
        {
            return _servicoAppService.Update(obj);
        }

        // DELETE api/servico/5
        public ValidationResult Delete(string objID)
        {
            Servico obj = _servicoAppService.Find(Guid.Parse(objID));
            return _servicoAppService.Remove(obj);
        }
    }
}