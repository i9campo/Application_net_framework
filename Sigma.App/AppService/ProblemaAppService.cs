using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;

namespace Sigma.App.AppService
{
    public class ProblemaAppService : AppService<Problema>, IProblemaAppService
    {
        private IProblemaService _Service;
        public ProblemaAppService(IProblemaService service)
            :base(service)
        {

        }
    }
}
