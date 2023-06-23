using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class ProprietarioFornecedorService : Service<ProprietarioFornecedor>, IProprietarioFornecedorService
    {
        private readonly IProprietarioFornecedorRepository _repository;

        public ProprietarioFornecedorService(IProprietarioFornecedorRepository repository)
            :base (repository)
        {
            _repository = repository; 
        }
    }
}
