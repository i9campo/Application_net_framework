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
    public class TipoAmostraController : ApiController
    {
        private readonly ITipoAmostraAppService _tipoAmostraAppService;
        public TipoAmostraController(ITipoAmostraAppService tipoAmostraAppService)
        {
            _tipoAmostraAppService = tipoAmostraAppService;
        }

        // GET api/tipoamostra/
        public IEnumerable<TipoAmostra> Get()
        {
            return _tipoAmostraAppService.GetAll().OrderByDescending(o => o.ativo).ToList();
        }

        // GET api/tipoamostra/5
        public TipoAmostra Get(string objID)
        {
            return _tipoAmostraAppService.Find(Guid.Parse(objID));
        }


        // POST api/tipoamostra
        public ValidationResult Post([FromBody] TipoAmostra obj)
        {
            return _tipoAmostraAppService.Add(obj);
        }

        // PUT api/tipoamostra/5
        public ValidationResult Put(string objID, [FromBody] TipoAmostra obj)
        {
            return _tipoAmostraAppService.Update(obj);
        }

        // DELETE api/tipoamostra/5
        public ValidationResult Delete(string objID)
        {
            TipoAmostra obj = _tipoAmostraAppService.Find(Guid.Parse(objID));

            return _tipoAmostraAppService.Remove(obj);
        }
    }
}