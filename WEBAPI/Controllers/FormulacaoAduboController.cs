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
    public class FormulacaoAduboController : ApiController
    {
        private readonly IFormulacaoAduboAppService _formulacaoAduboAppService;
        public FormulacaoAduboController(IFormulacaoAduboAppService formulacaoAduboAppService)
        {
            _formulacaoAduboAppService = formulacaoAduboAppService;
        }

        // GET api/formulacaoadubo/
        public IEnumerable<FormulacaoAdubo> Get()
        {


            return _formulacaoAduboAppService.GetAll();
        }

        // GET api/formulacaoadubo/5
        public FormulacaoAdubo Get(string objID)
        {
            return _formulacaoAduboAppService.Find(Guid.Parse(objID));
        }

        // POST api/formulacaoadubo
        public ValidationResult Post([FromBody] FormulacaoAdubo obj)
        {
            return _formulacaoAduboAppService.Add(obj);
        }

        // PUT api/formulacaoadubo/5
        public ValidationResult Put(string objID, [FromBody] FormulacaoAdubo obj)
        {
            return _formulacaoAduboAppService.Update(obj);
        }

        // DELETE api/formulacaoadubo/5
        public ValidationResult Delete(string objID)
        {
            FormulacaoAdubo obj = _formulacaoAduboAppService.Find(Guid.Parse(objID));

            return _formulacaoAduboAppService.Remove(obj);
        }
    }
}