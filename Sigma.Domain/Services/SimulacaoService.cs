using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using System;

namespace Sigma.Domain.Services
{
    public class SimulacaoService : Service<Simulacao>, ISimulacaoService
    {
        private readonly ISimulacaoRepository _repository;

        public SimulacaoService(ISimulacaoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
        public Simulacao GetAllSimulacao(Guid IDAreaServico, int opcao)
        {
            return _repository.GetAllSimulacao(IDAreaServico, opcao);
        }
        public Simulacao GetCultura()
        {
            return _repository.GetCultura();
        }
    }
}
