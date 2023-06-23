using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class ProdutoSimuladorService : Service<ProdutoSimulador>, IProdutoSimuladorService
    {
        private readonly IProdutoSimuladorRepository _repository;

        public ProdutoSimuladorService(IProdutoSimuladorRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProdutoSimuladorProduto> GetAllProdutoSimulador(Guid IDSimulacao)
        {
            return _repository.GetAllProdutoSimulador(IDSimulacao);
        }
        public IEnumerable<ProdutoFertilizante> GetAllProdutoFertilizante(Guid IDSimulacao)
        {
            return _repository.GetAllProdutoFertilizante(IDSimulacao);
        }
    }
}
