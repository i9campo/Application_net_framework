using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class ArquivoAreaAppService : AppService<ArquivoAreaView>, IArquivoAreaAppService
    {
        private readonly IArquivoAreaService _ArquivoAreaService;
        public ArquivoAreaAppService(IArquivoAreaService arquivoAreaService)
            :base(arquivoAreaService)
        {
            _ArquivoAreaService = arquivoAreaService; 
        }

        public IEnumerable<ArquivoAreaView> GetListArquivoAreaByAreaServico(Guid IDAreaServico)
        {
            return _ArquivoAreaService.GetListArquivoAreaByAreaServico(IDAreaServico); 
        }

        public IEnumerable<ArquivoAreaView> GetListArquivoAreaBySafraArea(Guid IDSafra, Guid IDArea)
        {
            return _ArquivoAreaService.GetListArquivoAreaBySafraArea(IDSafra, IDArea); 
        }

        public IEnumerable<ArquivoAreaView> GetListArquivoAreaBySafraPropriedade(Guid IDSafra, Guid IDPropriedade)
        {
            return _ArquivoAreaService.GetListArquivoAreaBySafraPropriedade(IDSafra, IDPropriedade);
        }
    }
}
