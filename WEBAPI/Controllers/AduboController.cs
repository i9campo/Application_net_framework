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
    public class AduboController : ApiController
    {
        private readonly IAduboAppService _aduboAppService;
        public AduboController(IAduboAppService aduboAppService)
        {
            _aduboAppService = aduboAppService;
        }

        /// <type>HttpGet</type>
        /// <link>'/adubo/'</link>
        public IEnumerable<Adubo> Get()
        {
            return _aduboAppService.GetAll();
        }

        /// <param name="objID"></param>
        /// <type>HttpGet</type>
        /// <link>'/adubo/', {params: {objID: 'name.params'}}</link>
        public Adubo Get(Guid objID)
        {
            return _aduboAppService.Find(objID);
        }

        /// <param name="obj"></param>
        /// <type>HttpPost</type>
        /// <link>'/adubo/', obj </link>
        public ValidationResult Post([FromBody] Adubo obj)
        {
            return _aduboAppService.Add(obj);
        }

        /// <param name="objID"></param>
        /// <param name="obj"></param>
        /// <type>HttpPut</type>
        /// <link>'/adubo/' + objID, obj</link>
        public ValidationResult Put(string objID, [FromBody] Adubo obj)
        {
            return _aduboAppService.Update(obj);
        }

        /// <param name="objID"></param>
        /// <type>HttpDelete</type>
        /// <link>'/analisesolo/ + objID</link>
        public ValidationResult Delete(Guid objID)
        {
            Adubo obj = _aduboAppService.Find(objID);
            return _aduboAppService.Remove(obj);
        }
    }
}
