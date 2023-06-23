using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;

namespace Sigma.App.AppService
{
    public class UsoProdutoAppService: AppService<UsoProduto>, IUsoProdutoAppService
    {
        private readonly IUsoProdutoService _Service;
        public UsoProdutoAppService(IUsoProdutoService service)
            :base(service)
        {
            _Service = service; 
        }
    }
}
