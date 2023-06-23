using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;

namespace Sigma.App.AppService
{
    public class ProdutividadeVariedadeAppService: AppService<ProdutividadeVariedade>, IProdutividadeVariedadeAppService
    {
        private readonly IProdutividadeVariedadeService _Service;
        public ProdutividadeVariedadeAppService(IProdutividadeVariedadeService service)
            : base(service)
        {
            service =  _Service;
        }
    }
}
