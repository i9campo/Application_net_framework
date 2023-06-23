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
    public class ParametroAreaController : ApiController
    {
        private readonly IParametroAreaAppService _parametroAreaAppService;
        public ParametroAreaController(IParametroAreaAppService parametroAreaAppService)
        {

            _parametroAreaAppService = parametroAreaAppService;
        }

        // GET api/<controller>
        public IEnumerable<ParametroArea> Get()
        {
            return _parametroAreaAppService.GetAll();
        }

        [HttpGet]
        [ActionName("getallparametroarea")]
        [Route("api/parametroarea/getallparametroarea")]
        public ParametroAreaView GetAllParametroArea(Guid IDAreaServico, string safra, string area)
        {
            return _parametroAreaAppService.GetAllParametroArea(IDAreaServico, safra, area);
        }

        [HttpGet]
        [ActionName("getparametroareabyareaservico")]
        [Route("api/parametroarea/getparametroareabyareaservico")]
        public ParametroAreaView GetParametroareaByAreaServico(Guid IDAreaServico)
        {
            return _parametroAreaAppService.GetParametroareaByAreaServico(IDAreaServico); 
        }

        //GET api/<controller>/5
        public IEnumerable<ParametroArea> Get(string objID)
        {
            return null;
            //return _parametroAreaAppService.Find(Guid.Parse(objID.ToString()));
        }

        // POST api/<controller>
        public ValidationResult Post(ParametroArea obj)
        {

            return _parametroAreaAppService.Add(obj);
        }

        // PUT api/<controller>/5
        public ValidationResult Put([FromBody] ParametroArea obj)
        {
            return _parametroAreaAppService.Update(obj);
        }

        // DELETE api/<controller>/5
        public ValidationResult Delete(string objID)
        {
            ParametroArea obj = _parametroAreaAppService.Find(Guid.Parse(objID));
            return _parametroAreaAppService.Remove(obj);
        }
    }
}