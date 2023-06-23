using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class PropriedadeController : ApiController
    {
        private readonly IPropriedadeAppService _propriedadeAppService;
        public PropriedadeController(IPropriedadeAppService propriedadeAppService)
        {
            _propriedadeAppService = propriedadeAppService;
        }

        [HttpGet]
        [ActionName("hasservicoinsafra")]
        [Route("api/propriedade/hasservicoinsafra")]
        public IEnumerable<Propriedade> HasServicoInSafra(Guid IDSafra, Guid IDProprietario)
        {
            List<Propriedade> lst = null;

            try
            {
                lst = _propriedadeAppService.HasServicoInSafra(IDProprietario, IDSafra).ToList();
            }
            catch (Exception ex){ return null; }
            return lst;
        }


        [HttpGet]
        [ActionName("byproprietario")]
        [Route("api/propriedade/byproprietario")]
        public IEnumerable<Propriedade> ByProprietario(Guid IDProprietario)
        {
            return _propriedadeAppService.ByProprietario(IDProprietario.ToString());
        }



        [HttpGet]
        public IEnumerable<Propriedade> Get()
        {
            return _propriedadeAppService.GetAllPropriedade();
        }

        [HttpGet]
        [ActionName("findPropriedade")]
        [Route("api/propriedade/findPropriedade")]
        public Propriedade Get(Guid objID)
        {
            return _propriedadeAppService.Find(objID);
        }

        [HttpGet]
        [ActionName("GetPropriedadeBNGByProprietario")]
        [Route("api/proprietario/GetPropriedadeBNGProprietario")]
        public IEnumerable<BNGPropriedade> GetPropriedadeBNGByProprietario(Guid IDSafra, Guid IDProprietario)
        {
            return _propriedadeAppService.GetPropriedadeBNGByProprietario(IDSafra, IDProprietario); 
        }

        // POST api/propriedade
        public ValidationResult Post([FromBody] Propriedade obj)
        {
            return _propriedadeAppService.Add(obj);
        }

        // PUT api/propriedade/5
        public ValidationResult Put(Guid objID, [FromBody] Propriedade obj)
        {
            return _propriedadeAppService.Update(obj);
        }

        // DELETE api/propriedade/5
        public ValidationResult Delete(Guid objID)
        {
            Propriedade obj = _propriedadeAppService.Find(objID);

            return _propriedadeAppService.Remove(obj);
        }
    }
}