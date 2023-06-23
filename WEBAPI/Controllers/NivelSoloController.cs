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
    public class NivelSoloController : ApiController
    {

        private readonly INivelSoloAppService _nivelSoloAppService;
        public NivelSoloController(INivelSoloAppService nivelSoloAppService)
        {
            _nivelSoloAppService = nivelSoloAppService;
        }

        // GET api/nivelsolo
        public IEnumerable<NivelSolo> Get()
        {
            return _nivelSoloAppService.GetAll();
        }


        // GET api/nivelsolo
        public NivelSolo Get(Guid objID)
        {
            return _nivelSoloAppService.Find(objID);
        }

        [HttpGet]
        [ActionName("getnivelbycultura")]
        [Route("api/nivelsolo/getnivelbycultura")]
        public IEnumerable<NivelSolo> GetNivelByCultura(Guid IDCultura)
        {
            return _nivelSoloAppService.GetNivelByCultura(IDCultura);
        }


        [HttpGet]
        [ActionName("getnivelbyelemento")]
        [Route("api/nivelsolo/getnivelbyelemento")]
        public IEnumerable<NivelSolo> GetNivelByElemento(Guid IDCultura, string elemento)
        {
            return _nivelSoloAppService.GetNivelByElemento(IDCultura, elemento);
        }

        // POST api/nivelsolo
        public ValidationResult Post([FromBody] NivelSolo obj)
        {
            return _nivelSoloAppService.Add(obj);
        }

        // PUT api/nivelsolo/5
        public ValidationResult Put(string objID, [FromBody] NivelSolo obj)
        {
            return _nivelSoloAppService.Update(obj);
        }

        // DELETE api/nivelsolo/5
        public ValidationResult Delete(string objID)
        {
            NivelSolo obj = _nivelSoloAppService.Find(Guid.Parse(objID));
            return _nivelSoloAppService.Remove(obj);
        }
    }
}