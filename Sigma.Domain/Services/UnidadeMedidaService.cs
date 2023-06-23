using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
using System.Collections.Generic;
namespace Sigma.Domain.Services
{
    public class UnidadeMedidaService : Service<UnidadeMedida>, IUnidadeMedidaService
    {
        private readonly IUnidadeMedidaRepository _repository;

        public UnidadeMedidaService(IUnidadeMedidaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
