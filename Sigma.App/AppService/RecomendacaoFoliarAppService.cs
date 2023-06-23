using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class RecomendacaoFoliarAppService : AppService<RecomendacaoFoliar>, IRecomendacaoFoliarAppService
    {
        private readonly IRecomendacaoFoliarService _Service;
        public RecomendacaoFoliarAppService(IRecomendacaoFoliarService service)
            :base(service)
        {
            _Service = service; 
        }

        public IEnumerable<RecomendacaoFoliar> GetByCultura(Guid IDCultura)
        {
            return _Service.GetByCultura(IDCultura);
        }

        public IEnumerable<RecomendacaoFoliarView> GetElementoRFNS(Guid objID, Guid IDCultura)
        {
            return _Service.GetElementoRFNS(objID, IDCultura);
        }
    }
}
