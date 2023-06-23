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
    public class AmostraFoliarController : ApiController
    {
        private readonly IAmostraFoliarAppService _amostraAppService;
        public AmostraFoliarController(IAmostraFoliarAppService amostraAppService)
        {
            _amostraAppService = amostraAppService;
        }

        //b2e84275-7df8-47cf-84d7-efa4a38cfc9a

        /// <type>HttpGet</type>
        /// <link>'/amostrafoliar/'</link>
        public IEnumerable<AmostraFoliar> Get()
        {
            return _amostraAppService.GetAll();
        }

        /// <param name="objID"></param>
        /// <type>HttpGet</type>
        /// <link>'/amostrafoliar/', { params: {objID: "param.name"} }"</link>
        public AmostraFoliar Get(Guid objID)
        {
            return _amostraAppService.Find(objID);
        }

        /// <param name="obj"></param>
        /// <type>HttpPost</type>
        /// <link>'/amostrafoliar/', obj </link>
        public ValidationResult Post([FromBody] AmostraFoliar obj)
        {
            return _amostraAppService.Add(obj);
        }

        /// <param name="objID"></param>
        /// <param name="obj"></param>
        /// <type>HttpPut</type>
        /// <link>'/amostrafoliar/' + objID, obj</link>
        public ValidationResult Put(string objID, [FromBody] AmostraFoliar obj)
        {
            return _amostraAppService.Update(obj);
        }

        /// <param name="objID"></param>
        /// <type>HttpDelete</type>
        /// <link>'/amostrafoliar/ + objID</link>
        public ValidationResult Delete(string objID)
        {
            AmostraFoliar obj = _amostraAppService.Find(Guid.Parse(objID));

            return _amostraAppService.Remove(obj);
        }
    }
}