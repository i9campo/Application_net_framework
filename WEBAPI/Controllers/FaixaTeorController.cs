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
    public class FaixaTeorController : ApiController
    {
        private readonly IFaixaTeorAppService _faixaTeorAppService;
        public FaixaTeorController(IFaixaTeorAppService faixaTeorAppService)
        {
            _faixaTeorAppService = faixaTeorAppService;
        }

        // GET api/faixateor/
        public IEnumerable<FaixaTeor> Get()
        {
            return _faixaTeorAppService.GetAll();
        }

        [HttpGet]
        [ActionName("getbyparteplanta")]
        [Route("api/faixateor/getbyparteplanta")]
        public IEnumerable<FaixaTeorView> GetByPartePlanta(Guid IDPartePlanta)
        {
            return _faixaTeorAppService.GetByPartePlanta(IDPartePlanta);
        }

        [HttpGet]
        [ActionName("getbyobjid")]
        [Route("api/faixateor/getbyobjid")]
        public FaixaTeor Get(Guid objID)
        {
            return _faixaTeorAppService.Find(objID);
        }

        // POST api/faixateor
        public ValidationResult Post([FromBody] FaixaTeor obj)
        {
            return _faixaTeorAppService.Add(obj);
        }

        // PUT api/faixateor/5
        public ValidationResult Put(string objID, [FromBody] FaixaTeor obj)
        {
            return _faixaTeorAppService.Update(obj);
        }

        // DELETE api/faixateor/5
        public ValidationResult Delete(string objID)
        {
            FaixaTeor obj = _faixaTeorAppService.Find(Guid.Parse(objID));

            return _faixaTeorAppService.Remove(obj);
        }
    }
}