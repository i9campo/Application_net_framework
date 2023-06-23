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
    public class PartePlantaController : ApiController
    {
        private readonly IPartePlantaAppService _partePlantaAppaService;
        public PartePlantaController(IPartePlantaAppService partePlantaAppService)
        {
            _partePlantaAppaService = partePlantaAppService;
        }

        // GET api/parteplanta
        public IEnumerable<PartePlanta> Get()
        {
            return _partePlantaAppaService.GetAll();
        }

        // GET api/parteplanta
        public PartePlanta Get(Guid objID)
        {
            return _partePlantaAppaService.Find(objID);
        }

        [HttpGet]
        [ActionName("getparteplantabycultura")]
        [Route("api/parteplanta/getparteplantabycultura")]
        public IEnumerable<PartePlanta> GetPartePlantaByCultura(Guid IDCultura)
        {
            return _partePlantaAppaService.GetPartePlantaByCultura(IDCultura);
        }


        // POST api/parteplanta
        public ValidationResult Post([FromBody] PartePlanta obj)
        {
            return _partePlantaAppaService.Add(obj);
        }

        // PUT api/parteplanta/5
        [HttpPut]
        public ValidationResult Put([FromBody] PartePlanta obj)
        {
            return _partePlantaAppaService.Update(obj);
        }

        // DELETE api/parteplanta/5
        public ValidationResult Delete(string objID)
        {
            PartePlanta obj = _partePlantaAppaService.Find(Guid.Parse(objID));
            return _partePlantaAppaService.Remove(obj);
        }
    }
}