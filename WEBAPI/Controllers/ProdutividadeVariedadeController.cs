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
    public class ProdutividadeVariedadeController : ApiController
    {
        private readonly IProdutividadeVariedadeAppService _produtividadeVariedadeAppService;
        public ProdutividadeVariedadeController(IProdutividadeVariedadeAppService produtividadeVariedadeAppService)
        {
            _produtividadeVariedadeAppService = produtividadeVariedadeAppService;
        }


        // GET api/produtividadevariedade/
        public IEnumerable<ProdutividadeVariedade> Get()
        {
            return _produtividadeVariedadeAppService.GetAll();
        }

        // GET api/produtividadevariedade/5
        public ProdutividadeVariedade Get(string objID)
        {
            return _produtividadeVariedadeAppService.Find(Guid.Parse(objID));
        }

        // POST api/produtividadevariedade
        public ValidationResult Post([FromBody] ProdutividadeVariedade obj)
        {
            return _produtividadeVariedadeAppService.Add(obj);
        }

        // PUT api/produtividadevariedade/5
        public ValidationResult Put(string objID, [FromBody] ProdutividadeVariedade obj)
        {
            return _produtividadeVariedadeAppService.Update(obj);
        }

        // DELETE api/produtividadevariedade/5
        public ValidationResult Delete(string objID)
        {
            ProdutividadeVariedade obj = _produtividadeVariedadeAppService.Find(Guid.Parse(objID));

            return _produtividadeVariedadeAppService.Remove(obj);
        }
    }
}