using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using Sigma.Infra.CrossCutting.Identity.Configuration;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class ProdutoSimuladorController : ApiController
    {

        private readonly ApplicationUserManager _aplicationUserManager;
        private readonly IProdutoSimuladorAppService _produtoSimuladorAppService;
        public ProdutoSimuladorController(IProdutoSimuladorAppService produtoSimuladorAppService, ApplicationUserManager aplicationUserManager)
        {
            _produtoSimuladorAppService = produtoSimuladorAppService;
            _aplicationUserManager = aplicationUserManager;

        }


        [HttpGet]
        [ActionName("getallprodutosimulador")]
        [Route("api/produtosimulador/getallprodutosimulador")]
        public IEnumerable<ProdutoSimuladorProduto> GetAllProdutoSimulador(Guid IDSimulacao)
        {
            return _produtoSimuladorAppService.GetAllProdutoSimulador(IDSimulacao);
        }

        [HttpGet]
        [ActionName("getallprodutofertilizante")]
        [Route("api/produtosimulador/getallprodutofertilizante")]
        public IEnumerable<ProdutoFertilizante> GetAllProdutoFertilizante(Guid IDSimulacao)
        {
            return _produtoSimuladorAppService.GetAllProdutoFertilizante(IDSimulacao);
        }

        [HttpPost]
        [ActionName("Post")]
        [Route("api/produtosimulador/Post/")]
        public ValidationResult Post([FromBody] ProdutoSimulador obj)
        {
            obj.objID = Guid.NewGuid();
            obj.dateINC = DateTime.Now;
            obj.IDUsuarioINC = User.Identity.GetUserId();
            obj.dateALT = null;
            return _produtoSimuladorAppService.Add(obj);


        }
        // PUT api/<controller>/5
        public ValidationResult Put(string objID, [FromBody] ProdutoSimulador obj)
        {

            obj.dateALT = DateTime.Now;
            obj.IDUsuarioALT = User.Identity.GetUserId();

            return _produtoSimuladorAppService.Update(obj);
        }



        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}