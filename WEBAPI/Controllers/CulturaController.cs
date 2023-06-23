using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Sigma.Domain.ViewTables;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class CulturaController : ApiController
    {
        private readonly ICulturaAppService _culturaAppService;
        public CulturaController(ICulturaAppService culturaAppService)
        {
            _culturaAppService = culturaAppService;
        }

        // GET api/cultura/
        public IEnumerable<Cultura> Get()
        {
            //return _culturaAppService.GetAllCultura();
            return _culturaAppService.GetAll();
        }

        // GET api/cultura/5
        public CulturaView Get(Guid objID)
        {
            return _culturaAppService.FindCultura(objID);
            //return null; 
        }

        // POST api/cultura
        public ValidationResult Post([FromBody] Cultura obj)
        {
            return _culturaAppService.Add(obj);
        }

        // PUT api/cultura/5
        public ValidationResult Put(string objID, [FromBody] Cultura obj)
        {
            return _culturaAppService.Update(obj);
        }

        // DELETE api/cultura/5
        public ValidationResult Delete(string objID)
        {
            Cultura obj = _culturaAppService.Find(Guid.Parse(objID));

            return _culturaAppService.Remove(obj);
        }
    }
}