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
    public class AmostraController : ApiController
    {
        private readonly IAmostraAppService _amostraAppService;
        public AmostraController(IAmostraAppService amostraappservice)
        {
            _amostraAppService = amostraappservice;
        }

        // GET api/<controller>
        public IEnumerable<Amostra> Get()
        {
            return _amostraAppService.GetAll();
        }
        [HttpGet]
        [ActionName("getbycultura")]
        [Route("api/amostra/getbycultura")]
        public IEnumerable<Amostra> GetByCultura(Guid IDCultura, int? mediaT)
        {
            return _amostraAppService.GetByCultura(IDCultura, mediaT);
        }

        // GET api/<controller>/5
        public Amostra Get(Guid objID)
        {
            return _amostraAppService.Find(objID);
        }
        // POST api/<controller>
        public ValidationResult Post([FromBody] Amostra obj)
        {
            return _amostraAppService.Add(obj);
        }
        // PUT api/<controller>/5
        public ValidationResult Put(Guid objID, [FromBody] Amostra obj)
        {
            return _amostraAppService.Update(obj);
        }
        // DELETE api/<controller>/5
        public ValidationResult Delete(Guid objID)
        {
            Amostra obj = _amostraAppService.Find(objID);
            return _amostraAppService.Remove(obj);
        }
    }
}