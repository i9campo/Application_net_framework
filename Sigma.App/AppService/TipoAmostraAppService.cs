using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;

namespace Sigma.App.AppService
{
    public class TipoAmostraAppService : AppService<TipoAmostra>, ITipoAmostraAppService
    {
        private readonly ITipoAmostraService _Service;
        public TipoAmostraAppService(ITipoAmostraService service)
            :base(service)
        {
            _Service = service; 
        }
    }
}
