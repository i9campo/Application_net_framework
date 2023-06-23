using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class ExtracaoCulturaService : Service<ExtracaoCultura>, IExtracaoCulturaService
    {
        private readonly IExtracaoCulturaRepository _repository;
        public ExtracaoCulturaService(IExtracaoCulturaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
