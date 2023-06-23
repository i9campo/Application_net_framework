using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;

namespace Sigma.Domain.Services
{
    public class ProdutividadeVariedadeService : Service<ProdutividadeVariedade>, IProdutividadeVariedadeService
    {
        private readonly IProdutividadeVariedadeRepository _repository;
        public ProdutividadeVariedadeService(IProdutividadeVariedadeRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
