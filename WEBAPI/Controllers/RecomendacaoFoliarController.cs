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
    public class RecomendacaoFoliarController : ApiController
    {
        private readonly IRecomendacaoFoliarAppService _recomendacaoFoliarAppService;
        public RecomendacaoFoliarController(IRecomendacaoFoliarAppService recomendacaoFoliarAppService)
        {
            _recomendacaoFoliarAppService = recomendacaoFoliarAppService;
        }

        // GET api/recomendacaofoliar/
        public IEnumerable<RecomendacaoFoliar> Get()
        {
            return _recomendacaoFoliarAppService.GetAll();
        }

        // GET api/recomendacaofoliar/5
        public RecomendacaoFoliar Get(Guid objID)
        {
            return _recomendacaoFoliarAppService.Find(objID);
        }

        [HttpGet]
        [ActionName("getelementorfns")]
        [Route("api/recomendacaofoliar/getelementorfns")]
        public IEnumerable<RecomendacaoFoliarView> GetElementoRFNS(Guid objID, Guid IDCultura)
        {
            return _recomendacaoFoliarAppService.GetElementoRFNS(objID, IDCultura);
        }


        [HttpGet]
        [ActionName("getbycultura")]
        [Route("api/recomendacaofoliar/getbycultura")]
        public IEnumerable<RecomendacaoFoliar> GetByCultura(Guid IDCultura)
        {
            return _recomendacaoFoliarAppService.GetByCultura(IDCultura);
        }


        // POST api/recomendacaofoliar
        public ValidationResult Post([FromBody] RecomendacaoFoliar obj)
        {
            return _recomendacaoFoliarAppService.Add(obj);
        }

        // PUT api/recomendacaofoliar/5
        public ValidationResult Put(string objID, [FromBody] RecomendacaoFoliar obj)
        {
            return _recomendacaoFoliarAppService.Update(obj);
        }

        // DELETE api/recomendacaofoliar/5
        public ValidationResult Delete(string objID)
        {
            RecomendacaoFoliar obj = _recomendacaoFoliarAppService.Find(Guid.Parse(objID));

            return _recomendacaoFoliarAppService.Remove(obj);
        }
    }
}