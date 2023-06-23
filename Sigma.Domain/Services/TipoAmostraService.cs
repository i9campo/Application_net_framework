using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class TipoAmostraService : Service<TipoAmostra>, ITipoAmostraService
    {
        private readonly ITipoAmostraRepository _repository;
        public TipoAmostraService(ITipoAmostraRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
