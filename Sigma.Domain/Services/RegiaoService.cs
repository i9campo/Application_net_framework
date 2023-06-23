using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class RegiaoService: Service<Regiao>, IRegiaoService
    {
        private readonly IRegiaoRepository _repository;
        public RegiaoService(IRegiaoRepository repository)
            :base (repository)
        {
            _repository = repository; 
        }
    }
}
