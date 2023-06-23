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
    public class TeorSoloController : ApiController
    {
        private readonly ITeorSoloAppService _teorSoloAppService;
        public TeorSoloController(ITeorSoloAppService teorSoloAppService)
        {
            _teorSoloAppService = teorSoloAppService;
        }



        // GET api/teorsolo
        public IEnumerable<TeorSolo> Get()
        {
            return _teorSoloAppService.GetAll();
        }

        // GET api/teorsolo
        public TeorSolo Get(string objID)
        {
            return _teorSoloAppService.Find(Guid.Parse(objID));
        }

        // POST api/teorsolo
        public ValidationResult Post([FromBody] TeorSolo obj)
        {
            return _teorSoloAppService.Add(obj);
        }

        // PUT api/teorsolo/5
        public ValidationResult Put(string objID, [FromBody] TeorSolo obj)
        {
            return _teorSoloAppService.Update(obj);
        }

        // DELETE api/teorsolo/5
        public ValidationResult Delete(string objID)
        {
            TeorSolo obj = _teorSoloAppService.Find(Guid.Parse(objID));
            return _teorSoloAppService.Remove(obj);
        }
    }
}