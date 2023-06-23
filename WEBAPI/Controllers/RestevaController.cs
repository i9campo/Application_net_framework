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
    public class RestevaController : ApiController
    {
        private readonly IRestevaAppService _restevaAppService;
        public RestevaController(IRestevaAppService restevaAppService)
        {
            _restevaAppService = restevaAppService;
        }


        // GET api/resteva/
        public IEnumerable<Resteva> Get()
        {
            return _restevaAppService.GetAll();
        }

        // GET api/resteva/5
        public Resteva Get(string objID)
        {
            return _restevaAppService.Find(Guid.Parse(objID));
        }

        // POST api/resteva
        public ValidationResult Post([FromBody] Resteva obj)
        {
            return _restevaAppService.Add(obj);
        }

        // PUT api/resteva/5
        public ValidationResult Put(string objID, [FromBody] Resteva obj)
        {
            return _restevaAppService.Update(obj);
        }

        // DELETE api/resteva/5
        public ValidationResult Delete(string objID)
        {
            Resteva obj = _restevaAppService.Find(Guid.Parse(objID));

            return _restevaAppService.Remove(obj);
        }
    }
}