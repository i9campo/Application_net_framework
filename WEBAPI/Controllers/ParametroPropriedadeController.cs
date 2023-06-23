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
    public class ParametroPropriedadeController : ApiController
    {
        private readonly IParametroPropriedadeAppService _parametroPropriedadeAppService;
        public ParametroPropriedadeController(IParametroPropriedadeAppService parametroPropriedadeAppService)
        {
            _parametroPropriedadeAppService = parametroPropriedadeAppService;
        }

        // GET api/<controller>
        public IEnumerable<ParametroPropriedade> Get()
        {
            return _parametroPropriedadeAppService.GetAll();
        }

        [HttpGet]
        [ActionName("getsolo")]
        [Route("api/parametropropriedade/getsolo")]
        public ParametroSoloView GetSolo(Guid IDAreaServico)
        {
            return _parametroPropriedadeAppService.GetSolo(IDAreaServico);
        }


        [HttpGet]
        [ActionName("getbyareapropriedade")]
        [Route("api/parametropropriedade/getbyareapropriedade")]
        public ParametroPropriedade GetByAreaPropriedade(Guid IDSafra, Guid IDPropriedade)
        {
            return _parametroPropriedadeAppService.GetByAreaPropriedade(IDSafra, IDPropriedade); 
        }

        // GET api/<controller>/5
        public IEnumerable<ParametroPropriedade> Get(Guid objID)
        {
            return _parametroPropriedadeAppService.FindParametroPropriedade(objID);
        }

        // POST api/<controller>
        public ValidationResult Post(ParametroPropriedade obj)
        {
            return _parametroPropriedadeAppService.Add(obj);
        }

        // PUT api/<controller>/5
        public ValidationResult Put(Guid objID, [FromBody] ParametroPropriedade obj)
        {
            return _parametroPropriedadeAppService.Update(obj);
        }

        // DELETE api/<controller>/5
        public ValidationResult Delete(string objID)
        {
            ParametroPropriedade obj = _parametroPropriedadeAppService.Find(Guid.Parse(objID));
            return _parametroPropriedadeAppService.Remove(obj);
        }
    }
}