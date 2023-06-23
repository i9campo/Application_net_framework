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
    public class TipoSoloController : ApiController
    {
         private readonly ITipoSoloAppService _tipoSoloAppService;
        public TipoSoloController(ITipoSoloAppService tipoSoloService)
        {
            _tipoSoloAppService = tipoSoloService;
        }


        // GET api/<controller>
        public IEnumerable<TipoSolo> Get()
        {
            return _tipoSoloAppService.GetAll().OrderBy(o => o.abreviacao).ToList();
        }


        public TipoSolo Get(string objID)
        {
            return _tipoSoloAppService.Find(Guid.Parse(objID));
        }

        public ValidationResult Post([FromBody] TipoSolo obj)
        {
            obj.objID = Guid.NewGuid();
            return _tipoSoloAppService.Add(obj);
        }

        public ValidationResult Put(string objID, [FromBody] TipoSolo obj)
        {
            return _tipoSoloAppService.Update(obj);
        }

        public ValidationResult Delete(string objID)
        {
            TipoSolo obj = _tipoSoloAppService.Find(Guid.Parse(objID));

            return _tipoSoloAppService.Remove(obj);
        }
    }
}