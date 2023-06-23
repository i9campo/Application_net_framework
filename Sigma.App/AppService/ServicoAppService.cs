using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class ServicoAppService : AppService<Servico>, IServicoAppService
    {
        private readonly IServicoService _Service;
        public ServicoAppService(IServicoService service)
            :base(service)
        {
            _Service = service; 
        }

        public IEnumerable<Servico> GetAllServico()
        {
            return _Service.GetAllServico();
        }

        public IEnumerable<Servico> GetByAreaSafra(Guid IDArea, Guid IDSafra)
        {
            return _Service.GetByAreaSafra(IDArea, IDSafra);
        }

        public IEnumerable<AreaServicoView> GetServico(String IDArea, String IDSafra)
        {
            return _Service.GetServico(IDArea, IDSafra);
        }
    }
}
