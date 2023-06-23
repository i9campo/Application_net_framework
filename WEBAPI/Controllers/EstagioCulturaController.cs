using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class estagioculturaController : ApiController
    {
        private readonly IEstagioCulturaAppService _estagioCulturaAppService;
        public estagioculturaController(IEstagioCulturaAppService estagioCulturaAppService)
        {
            _estagioCulturaAppService = estagioCulturaAppService;
        }

        // GET api/estagiocultura/
        public IEnumerable<EstagioCultura> Get()
        {
            return _estagioCulturaAppService.GetAll().OrderBy(o => o.dapPrecoce).ToList();
        }

        [HttpGet]
        [ActionName("getestagiobycultura")]
        [Route("api/estagiocultura/getestagiobycultura")]
        public IEnumerable<EstagioCultura> GetEstagioByPropriedade(Guid IDCultura)
        {

            return _estagioCulturaAppService.GetEstagioByCultura(IDCultura);
        }

        // GET api/estagiocultura/5
        public EstagioCultura Get(Guid objID)
        {
            return _estagioCulturaAppService.Find(objID);
        }

        // POST api/estagiocultura
        public ValidationResult Post([FromBody] EstagioCultura obj)
        {
            return _estagioCulturaAppService.Add(obj);
        }

        // PUT api/estagiocultura/5
        public ValidationResult Put(string objID, [FromBody] EstagioCultura obj)
        {
            return _estagioCulturaAppService.Update(obj);
        }

        // DELETE api/estagiocultura/5
        public ValidationResult Delete(string objID)
        {
            EstagioCultura obj = _estagioCulturaAppService.Find(Guid.Parse(objID));

            return _estagioCulturaAppService.Remove(obj);
        }
    }
}