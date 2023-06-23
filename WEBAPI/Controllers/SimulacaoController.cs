using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Infra.CrossCutting.Identity.Configuration;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class SimulacaoController : ApiController
    {
        private readonly ApplicationUserManager _aplicationUserManager;
        private readonly ISimulacaoAppService _simulacaoAppService;
        public SimulacaoController(ISimulacaoAppService simulacaoAppService, ApplicationUserManager aplicationUserManager)
        {
            _simulacaoAppService = simulacaoAppService;
            _aplicationUserManager = aplicationUserManager;
        }

        // GET api/<controller>
        public IEnumerable<Simulacao> Get()
        {
            return _simulacaoAppService.GetAll();
        }

        [HttpGet]
        [ActionName("getallsimulacao")]
        [Route("api/simulacao/getallsimulacao")]
        public Simulacao GetAllSimulacao(Guid IDAreaServico, int opcao)
        {
            return _simulacaoAppService.GetAllSimulacao(IDAreaServico, opcao);
        }


        [HttpGet]
        [ActionName("getcultura")]
        [Route("api/simulacao/getcultura")]
        public Simulacao GetCultura()
        {
            return _simulacaoAppService.GetCultura();
        }

        // POST api/<controller>


        [HttpPost]
        [ActionName("Post")]
        [Route("api/simulacao/Post/")]
        public ValidationResult Post([FromBody] Simulacao obj)
        {
            obj.dateINC = DateTime.Now;
            obj.IDUsuarioINC = User.Identity.GetUserId();
            obj.dateALT = null;
            return _simulacaoAppService.Add(obj);

        }

        // PUT api/<controller>/5
        public ValidationResult Put(string objID, [FromBody] Simulacao obj)
        {
            obj.dateALT = DateTime.Now;
            obj.IDUsuarioALT = User.Identity.GetUserId();
            return _simulacaoAppService.Update(obj);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}