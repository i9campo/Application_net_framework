using Sigma.App.Interfaces;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI.Controllers
{
    [AllowedOriginFilter]
    public class ArquivoAreaController : ApiController
    {
        private IEnumerable<ArquivoAreaView> oList { get; set; }
        private readonly IArquivoAreaAppService _arquivoAreaService;
        public ArquivoAreaController(IArquivoAreaAppService arquivoAreaService)
        {
            _arquivoAreaService = arquivoAreaService; 
        }

        [HttpGet]
        [ActionName("GetListArquivoAreaByAreaServico")]
        [Route("api/arquivoarea/GetListArquivoAreaByAreaServico")]
        public IEnumerable<ArquivoAreaView> GetListArquivoAreaByAreaServico(Guid IDAreaServico)
        {
            return _arquivoAreaService.GetListArquivoAreaByAreaServico(IDAreaServico); 
        }


        [HttpGet]
        [ActionName("GetListArquivoAreaBySafraPropriedade")]
        [Route("api/arquivoarea/GetListArquivoAreaBySafraPropriedade")]
        public IEnumerable<ArquivoAreaView> GetListArquivoAreaBySafraPropriedade(Guid IDSafra, Guid IDPropriedade)
        {
            return _arquivoAreaService.GetListArquivoAreaBySafraPropriedade(IDSafra, IDPropriedade); 
        }

        [HttpGet]
        [ActionName("GetListArquivoAreaBySafraArea")]
        [Route("api/arquivoarea/GetListArquivoAreaBySafraArea")]
        public IEnumerable<ArquivoAreaView> GetListArquivoAreaBySafraArea(Guid IDSafra, Guid IDArea)
        {
            return _arquivoAreaService.GetListArquivoAreaBySafraArea(IDSafra, IDArea);
        }



    }
}