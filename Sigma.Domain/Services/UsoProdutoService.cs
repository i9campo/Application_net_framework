using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class UsoProdutoService : Service<UsoProduto>, IUsoProdutoService
    {
        private readonly IUsoProdutoRepository _repository;

        public UsoProdutoService(IUsoProdutoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
