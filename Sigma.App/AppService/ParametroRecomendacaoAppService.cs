using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class ParametroRecomendacaoAppService: AppService<ParametroRecomendacao>, IParametroRecomendacaoAppService
    {
        private readonly IParametroRecomendacaoService _Service;
        public ParametroRecomendacaoAppService(IParametroRecomendacaoService service)
            :base(service)
        {
            _Service = service; 
        }
        public IEnumerable<ParametroRecomendacao> FindParametroRecomendacao(Guid objID)
        {
            return _Service.FindParametroRecomendacao(objID);
        }
    }
}
