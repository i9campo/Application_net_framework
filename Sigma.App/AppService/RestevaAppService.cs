using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;

namespace Sigma.App.AppService
{
    public class RestevaAppService : AppService<Resteva>, IRestevaAppService
    {
        private readonly IRestevaService _Service;
        public RestevaAppService(IRestevaService service)
            :base(service)
        {
            _Service = service; 
        }
    }
}
