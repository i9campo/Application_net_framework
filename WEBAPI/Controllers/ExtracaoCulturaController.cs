using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class ExtracaoCulturaController : ApiController
    {
        private readonly IExtracaoCulturaAppService _extracaoCulturaAppService;
        public ExtracaoCulturaController(IExtracaoCulturaAppService extracaoCulturaAppService)
        {
            _extracaoCulturaAppService = extracaoCulturaAppService;
        }

        // GET api/<controller>
        public IEnumerable<ExtracaoCultura> Get()
        {
            return _extracaoCulturaAppService.GetAll();
        }

        public ExtracaoCultura Get(string objID)
        {
            return _extracaoCulturaAppService.Find(Guid.Parse(objID));
        }

        // POST api/ExtracaoProducao
        public ValidationResult Post([FromBody] ExtracaoCultura objID)
        {
            return _extracaoCulturaAppService.Add(objID);
        }

        // PUT api/ExtracaoProducao/5
        public ValidationResult Put(string objID, [FromBody] ExtracaoCultura obj)
        {
            return _extracaoCulturaAppService.Update(obj);
        }

        // DELETE api/ExtracaoProducao/5
        public ValidationResult Delete(string objID)
        {
            ExtracaoCultura obj = _extracaoCulturaAppService.Find(Guid.Parse(objID));
            return _extracaoCulturaAppService.Remove(obj);
        }
    }
}