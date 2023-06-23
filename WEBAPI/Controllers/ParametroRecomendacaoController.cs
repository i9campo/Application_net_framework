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
    public class ParametroRecomendacaoController : ApiController
    {
        private readonly IParametroRecomendacaoAppService _parametroRecomendacaoAppService;
        public ParametroRecomendacaoController(IParametroRecomendacaoAppService parametroRecomendacaoAppService)
        {
            _parametroRecomendacaoAppService = parametroRecomendacaoAppService;
        }

        // GET api/<controller>
        public IEnumerable<ParametroRecomendacao> Get()
        {
            return _parametroRecomendacaoAppService.GetAll();
        }


        // GET api/<controller>/5
        public IEnumerable<ParametroRecomendacao> Get(Guid objID)
        {
            return _parametroRecomendacaoAppService.FindParametroRecomendacao(objID);
        }

        // POST api/<controller>
        public ValidationResult Post([FromBody] ParametroRecomendacao obj)
        {

            return _parametroRecomendacaoAppService.Add(obj);
        }

        // PUT api/<controller>/5
        public ValidationResult Put(int objID, [FromBody] ParametroRecomendacao obj)
        {
            return _parametroRecomendacaoAppService.Update(obj);
        }

        // DELETE api/<controller>/5
        public ValidationResult Delete(string objID)
        {
            ParametroRecomendacao obj = _parametroRecomendacaoAppService.Find(Guid.Parse(objID));
            return _parametroRecomendacaoAppService.Remove(obj);
        }
    }
}