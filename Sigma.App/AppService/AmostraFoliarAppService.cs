using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;

namespace Sigma.App.AppService
{
    public class AmostraFoliarAppService : AppService<AmostraFoliar>, IAmostraFoliarAppService
    {
        private readonly IAmostraFoliarService _Service;
        public AmostraFoliarAppService(IAmostraFoliarService service)
            : base(service)
        {
            _Service = service;
        }
    }
}
