using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class TipoSoloService : Service<TipoSolo>, ITipoSoloService
    {
        private readonly ITipoSoloRepository _repository;
        public TipoSoloService(ITipoSoloRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
        public TipoSolo FindTipoSolo(string tpSolo)
        {
            return _repository.FindTipoSolo(tpSolo);
        }
    }
}
