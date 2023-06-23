using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class ParametroPropriedadeAppService : AppService<ParametroPropriedade> , IParametroPropriedadeAppService
    {
        private readonly IParametroPropriedadeService _Service;
        public ParametroPropriedadeAppService(IParametroPropriedadeService service)
            :base(service)
        {
            _Service = service; 
        }

        public IEnumerable<ParametroPropriedade> FindParametroPropriedade(Guid objID)
        {
            return _Service.FindParametroPropriedade(objID);
        }

        public ParametroPropriedade GetByAreaPropriedade(Guid IDSafra, Guid IDPropriedade)
        {
            return _Service.GetByAreaPropriedade(IDSafra, IDPropriedade); 
        }

        public ParametroSoloView GetSolo(Guid IDAreaServico)
        {
            return _Service.GetSolo(IDAreaServico);
        }
    }
}
