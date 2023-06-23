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
    public class SafraController : ApiController
    {
        private readonly ISafraAppService _safraAppService;
        public SafraController(ISafraAppService safraAppService)
        {
            _safraAppService = safraAppService;
        }

        // GET api/safra
        public IEnumerable<Safra> Get()
        {
            return _safraAppService.GetAll().OrderByDescending(o => o.anoInicial).ToList();
        }

        [HttpGet]
        [ActionName("GetLstSafraBNG")]
        [Route("api/safra/GetLstSafraBNG")]
        public IEnumerable<SafraView> GetLstSafraBNG()
        {
            return _safraAppService.GetLstSafraBNG(); 
        }

        // GET api/safra
        public IEnumerable<Safra> Get(Guid objID)
        {
            return _safraAppService.FindSafra(objID);
        }

        // POST api/Safra
        public ValidationResult Post([FromBody] Safra obj)
        {
            return _safraAppService.Add(obj);
        }

        // PUT api/Safra/5
        public ValidationResult Put(string objID, [FromBody] Safra obj)
        {
            return _safraAppService.Update(obj);
        }

        // DELETE api/Safra/5
        public ValidationResult Delete(string objID)
        {
            Safra obj = _safraAppService.Find(Guid.Parse(objID));
            return _safraAppService.Remove(obj);
        }
    }
}