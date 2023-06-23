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
    public class ImagemController : ApiController
    {
        private readonly IImagemAppService _ImagemAppService;
        public ImagemController(IImagemAppService imagemAppService)
        {
            _ImagemAppService = imagemAppService;
        }

        // GET api/imagem
        public IEnumerable<Imagem> Get()
        {
            return _ImagemAppService.GetAll();
        }



        // GET api/imagem
        public Imagem Get(string objID)
        {
            return _ImagemAppService.Find(Guid.Parse(objID));
        }

        // POST api/imagem
        public ValidationResult Post([FromBody] Imagem obj)
        {
            return _ImagemAppService.Add(obj);
        }

        // PUT api/imagem/5
        public ValidationResult Put(string objID, [FromBody] Imagem obj)
        {
            return _ImagemAppService.Update(obj);
        }

        // DELETE api/imagem/5
        public ValidationResult Delete(string objID)
        {
            Imagem obj = _ImagemAppService.Find(Guid.Parse(objID));
            return _ImagemAppService.Remove(obj);
        }
    }
}