using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;

namespace Sigma.App.AppService
{
    public class TeorSoloAppService : AppService<TeorSolo>, ITeorSoloAppService
    {
        private readonly ITeorSoloService _Service;
        public TeorSoloAppService(ITeorSoloService service)
            :base(service)
        {
            _Service = service; 
        }
    }
}
