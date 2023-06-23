using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;

namespace Sigma.App.AppService
{
    public class TeorFoliarAppService : AppService<TeorFoliar>, ITeorFoliarAppService
    {
        private readonly ITeorFoliarService _Service;
        public TeorFoliarAppService(ITeorFoliarService service)
            :base(service)
        {

        }
    }
}
