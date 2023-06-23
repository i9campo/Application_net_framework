using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Interfaces.Service._Base;

namespace Sigma.App.AppService
{
    public class ItensAnalisesLaboratorioAppService : AppService<SequenciaImportacao>, IItensAnaliseLaboratorioAppService
    {

        private readonly IItensAnaliseLaboratorioService _Service;
        public ItensAnalisesLaboratorioAppService(IItensAnaliseLaboratorioService  service) 
            : base(service)
        {
            _Service = service;
        }
    }
}
