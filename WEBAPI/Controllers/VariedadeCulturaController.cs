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
    public class VariedadeCulturaController : ApiController
    {
        private readonly IVariedadeCulturaAppService _variedadeCulturaAppService;
        public VariedadeCulturaController(IVariedadeCulturaAppService variedadeCulturaAppService)
        {
            _variedadeCulturaAppService = variedadeCulturaAppService;
        }

        // GET api/variedadecultura
        public IEnumerable<VariedadeCultura> Get()
        {
            return _variedadeCulturaAppService.GetAll();
        }

        // GET api/variedadecultura/5
        public VariedadeCultura Get(string objID)
        {
            return _variedadeCulturaAppService.Find(Guid.Parse(objID));
        }


        [HttpGet]
        [ActionName("getvariedadeculturabycultura")]
        [Route("api/variedadecultura/getvariedadeculturabycultura")]
        public IEnumerable<VariedadeCultura> GetVariedadeCulturaByCultura(Guid IDCultura)
        {
            return _variedadeCulturaAppService.GetVariedadeCulturaByCultura(IDCultura);
        }

        // POST api/variedadecultura
        public ValidationResult Post([FromBody] VariedadeCultura obj)
        {
            return _variedadeCulturaAppService.Add(obj);
        }

        // PUT api/variedadecultura/5
        public ValidationResult Put(string objID, [FromBody] VariedadeCultura obj)
        {
            return _variedadeCulturaAppService.Update(obj);
        }

        // DELETE api/variedadecultura/5
        public ValidationResult Delete(string objID)
        {
            VariedadeCultura obj = _variedadeCulturaAppService.Find(Guid.Parse(objID));
            return _variedadeCulturaAppService.Remove(obj);
        }
    }
}