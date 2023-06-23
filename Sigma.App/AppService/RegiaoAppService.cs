using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;

namespace Sigma.App.AppService
{
    public class RegiaoAppService : AppService<Regiao>, IRegiaoAppService
    {
        private readonly IRegiaoService _Service;
        public RegiaoAppService(IRegiaoService service)
            :base(service)
        {
            _Service = service; 
        }
    }
}
