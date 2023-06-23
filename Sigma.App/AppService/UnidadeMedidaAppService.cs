using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;

namespace Sigma.App.AppService
{
    public class UnidadeMedidaAppService : AppService<UnidadeMedida>, IUnidadeMedidaAppService
    {
        private readonly IUnidadeMedidaService _Service;
        public UnidadeMedidaAppService(IUnidadeMedidaService service)
            :base(service)
        {
            _Service = service; 
        }
    }
}
