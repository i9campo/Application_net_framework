using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class UnidadeMedidaController : ApiController
    {
        private readonly IUnidadeMedidaAppService _unidademedidaAppService;
        public UnidadeMedidaController(IUnidadeMedidaAppService unidademedidaAppService)
        {
            _unidademedidaAppService = unidademedidaAppService;
        }


        // GET api/unidademedida
        public IEnumerable<UnidadeMedida> Get()
        {
            List<UnidadeMedida> lst = _unidademedidaAppService.GetAll().OrderBy(o => o.nome).ToList();
            return lst;
        }

        // GET api/unidademedida
        public UnidadeMedida Get(string objID)
        {
            return _unidademedidaAppService.Find(Guid.Parse(objID));
        }

        // POST api/unidademedida
        public ValidationResult Post([FromBody] UnidadeMedida obj)
        {
            return _unidademedidaAppService.Add(obj);
        }

        // PUT api/unidademedida/5
        public ValidationResult Put(string objID, [FromBody] UnidadeMedida obj)
        {
            return _unidademedidaAppService.Update(obj);
        }

        // DELETE api/unidademedida/5
        public ValidationResult Delete(string objID)
        {
            UnidadeMedida obj = _unidademedidaAppService.Find(Guid.Parse(objID));

            return _unidademedidaAppService.Remove(obj);
        }
    }
}