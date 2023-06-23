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
    public class LaboratorioController : ApiController
    {

        private readonly ILaboratorioAppService _laboratorioAppService;
        public LaboratorioController(ILaboratorioAppService laboratorioAppService)
        {
            _laboratorioAppService = laboratorioAppService;
        }

        // GET api/laboratorio/
        public IEnumerable<Laboratorio> Get()
        {
            return _laboratorioAppService.GetAll().OrderBy(o => o.nome).ToList();
        }

        // GET api/laboratorio/5
        public Laboratorio Get(string objID)
        {
            return _laboratorioAppService.Find(Guid.Parse(objID));
        }


        [HttpGet]
        [ActionName("getlaboratoriobycnpj")]
        [Route("api/laboratorio/getlaboratoriobycnpj")]
        public IEnumerable<Laboratorio> GetLaboratorioByCNPJ(string registro)
        {
            return _laboratorioAppService.GetLaboratorioByCNPJ(registro);
        }


        // POST api/laboratorio
        public ValidationResult Post([FromBody] Laboratorio obj)
        {
            obj.objID = Guid.NewGuid(); 
            return _laboratorioAppService.Add(obj);
        }

        // PUT api/laboratorio/5
        public ValidationResult Put(string objID, [FromBody] Laboratorio obj)
        {
            return _laboratorioAppService.Update(obj);
        }

        // DELETE api/laboratorio/5
        public ValidationResult Delete(string objID)
        {
            Laboratorio obj = _laboratorioAppService.Find(Guid.Parse(objID));

            return _laboratorioAppService.Remove(obj);
        }
    }
}
