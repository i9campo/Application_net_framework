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
    public class ProblemaController : ApiController
    {
        private readonly IProblemaAppService _problemaAppService;
        public ProblemaController(IProblemaAppService problemaAppService)
        {
            _problemaAppService = problemaAppService;
        }

        // GET api/problema/
        public IEnumerable<Problema> Get()
        {
            return _problemaAppService.GetAll();
        }


        // GET api/problema/5
        public Problema Get(string objID)
        {
            return _problemaAppService.Find(Guid.Parse(objID));
        }

        // POST api/problema
        public ValidationResult Post([FromBody] Problema obj)
        {
            return _problemaAppService.Add(obj);
        }

        // PUT api/problema/
        public ValidationResult Put(string objID, [FromBody] Problema obj)
        {
            return _problemaAppService.Update(obj);
        }

        // DELETE api/problema/5
        public ValidationResult Delete(string objID)
        {
            Problema obj = _problemaAppService.Find(Guid.Parse(objID));

            return _problemaAppService.Remove(obj);
        }
    }
}