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
    public class UnidadeDeLaboratorioController : ApiController
    {
        private readonly IUnidadeDeLaboratorioAppService _UnidadeService;
        public UnidadeDeLaboratorioController(IUnidadeDeLaboratorioAppService service)
        {
            _UnidadeService = service;
        }

        // GET api/unidadedelaboratorio
        public IEnumerable<UnidadeDeLaboratorioView> Get()
        {
            return _UnidadeService.GetAllDesc();
        }

        // GET api/unidadedelaboratorio/5
        public UnidadeDeLaboratorioView Get(Guid objID)
        {
            return _UnidadeService.FindDesc(objID);
        }

        // POST api/unidadedelaboratorio
        public ValidationResult Post([FromBody] UnidadeDeLaboratorio obj)
        {
            return _UnidadeService.Add(obj);
        }

        // PUT api/unidadedelaboratorio/5
        public ValidationResult Put(Guid objID, [FromBody] UnidadeDeLaboratorio obj)
        {
            return _UnidadeService.Update(obj);
        }

        // DELETE api/unidadedelaboratorio/5
        public ValidationResult Delete(Guid objID)
        {
            UnidadeDeLaboratorio obj = _UnidadeService.Find(objID);
            return _UnidadeService.Remove(obj);
        }
    }
}