using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;

namespace Sigma.App.AppService
{
    public class ProprietarioFornecedorAppService : AppService<ProprietarioFornecedor>, IProprietarioFornecedorAppService
    {
        private readonly IProprietarioFornecedorService _Service;
        public ProprietarioFornecedorAppService(IProprietarioFornecedorService service)
            :base(service)
        {
            _Service = service; 
        }
    }
}
