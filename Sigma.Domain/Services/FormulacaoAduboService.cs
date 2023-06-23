using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class FormulacaoAduboService : Service<FormulacaoAdubo>, IFormulacaoAduboService
    {
        private readonly IFormulacaoAduboRepository _repository;

        public FormulacaoAduboService(IFormulacaoAduboRepository repository)
            :base(repository)
        {
            _repository = repository;
        }
    }
}
