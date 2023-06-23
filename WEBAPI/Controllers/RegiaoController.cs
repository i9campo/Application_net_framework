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
    public class RegiaoController : ApiController
    {
        private readonly IRegiaoAppService _regiaoAppService;
        public RegiaoController(IRegiaoAppService regiaoAppService)
        {
            _regiaoAppService = regiaoAppService;
        }


        // GET api/regiao
        public IEnumerable<Regiao> Get()
        {
            return _regiaoAppService.GetAll();
        }

        // GET api/regiao
        public Regiao Get(string objID)
        {
            return _regiaoAppService.Find(Guid.Parse(objID));
        }

        // POST api/regiao
        public ValidationResult Post([FromBody] Regiao obj)
        {
            return _regiaoAppService.Add(obj);
        }

        // PUT api/regiao/5
        public ValidationResult Put(string objID, [FromBody] Regiao obj)
        {
            return _regiaoAppService.Update(obj);
        }

        // DELETE api/regiao/5
        public ValidationResult Delete(string objID)
        {
            Regiao obj = _regiaoAppService.Find(Guid.Parse(objID));
            return _regiaoAppService.Remove(obj);
        }
    }
}