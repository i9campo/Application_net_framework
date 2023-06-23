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
    public class FornecedorController : ApiController
    {
        private readonly IFornecedorAppService _fornecedorAppService;
        public FornecedorController(IFornecedorAppService fornecedorAppService)
        {
            _fornecedorAppService = fornecedorAppService;
        }

        // GET api/fornecedor/
        public IEnumerable<Fornecedor> Get()
        {
            return _fornecedorAppService.GetAll().OrderBy(o => o.nome);
        }

        //Get api/fornecedor/5
        public Fornecedor Get(Guid objID)
        {
            return _fornecedorAppService.Find(objID);
        }

        [HttpGet]
        [ActionName("getFornecedorPfPj")]
        [Route("api/fornecedor/getFornecedorPfPj")]
        public IEnumerable<Fornecedor> GetPfPj(string pfpj)
        {

            var obj = _fornecedorAppService.GetPfPj(pfpj);


            return _fornecedorAppService.GetPfPj(pfpj);
        }

        // POST api/fornecedor
        public ValidationResult Post([FromBody] Fornecedor obj)
        {
            return _fornecedorAppService.Add(obj);
        }

        // PUT api/fornecedor/5
        public ValidationResult Put(string objID, [FromBody] Fornecedor obj)
        {
            return _fornecedorAppService.Update(obj);
        }

        // DELETE api/fornecedor/5
        public ValidationResult Delete(string objID)
        {
            Fornecedor obj = _fornecedorAppService.Find(Guid.Parse(objID));

            return _fornecedorAppService.Remove(obj);
        }
    }
}