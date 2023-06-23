using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;

namespace Sigma.App.AppService
{
    public class ParametroAreaAppService : AppService<ParametroArea>, IParametroAreaAppService
    {
        private readonly IParametroAreaService _Service;
        public ParametroAreaAppService(IParametroAreaService service)
            :base(service)
        {
            _Service = service; 
        }

        public ParametroAreaView GetAllParametroArea(Guid IDAreaServico, string safra, string area)
        {
            return _Service.GetAllParametroArea(IDAreaServico, safra, area);
        }

        public ParametroAreaView GetParametroareaByAreaServico(Guid IDAreaServico)
        {
            return _Service.GetParametroareaByAreaServico(IDAreaServico); 
        }
    }
}
