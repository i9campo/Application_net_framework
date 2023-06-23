using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;

namespace Sigma.App.AppService
{
    public class ExtracaoCulturaAppService : AppService<ExtracaoCultura>, IExtracaoCulturaAppService
    {
        private readonly IExtracaoCulturaService _Service;
        public ExtracaoCulturaAppService(IExtracaoCulturaService service)
            :base(service)
        {
            _Service = service; 
        }
    }
}
