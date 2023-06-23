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
    public class TeorFoliarController : ApiController
    {
        private readonly ITeorFoliarAppService _teorFoliarAppService;
        public TeorFoliarController(ITeorFoliarAppService teorFoliarAppService)
        {
            _teorFoliarAppService = teorFoliarAppService;
        }


        // GET api/teorfoliar/
        public IEnumerable<TeorFoliar> Get()
        {
            return _teorFoliarAppService.GetAll();
        }

        // GET api/teorfoliar/5
        public TeorFoliar Get(string objID)
        {
            return _teorFoliarAppService.Find(Guid.Parse(objID));
        }

        // POST api/teorfoliar
        public ValidationResult Post([FromBody] TeorFoliar obj)
        {
            return _teorFoliarAppService.Add(obj);
        }

        // PUT api/teorfoliar/5
        public ValidationResult Put(string objID, [FromBody] TeorFoliar obj)
        {
            return _teorFoliarAppService.Update(obj);
        }

        // DELETE api/teorfoliar/5
        public ValidationResult Delete(string objID)
        {
            TeorFoliar obj = _teorFoliarAppService.Find(Guid.Parse(objID));

            return _teorFoliarAppService.Remove(obj);
        }
    }
}