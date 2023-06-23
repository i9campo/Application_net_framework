using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;

namespace Sigma.App.AppService
{
    public class TipoSoloAppService : AppService<TipoSolo>, ITipoSoloAppService
    {
        private readonly ITipoSoloService _Service;
        public TipoSoloAppService(ITipoSoloService service)
            :base(service)
        {
            _Service = service; 
        }
        public TipoSolo FindTipoSolo(string tpSolo)
        {
            return _Service.FindTipoSolo(tpSolo);
        }
    }
}
