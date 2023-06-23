using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class ProdutoSimuladorAppService: AppService<ProdutoSimulador>, IProdutoSimuladorAppService
    {
        private readonly IProdutoSimuladorService _Service;
        public ProdutoSimuladorAppService(IProdutoSimuladorService service)
            :base(service)
        {
            _Service = service; 
        }
        public IEnumerable<ProdutoSimuladorProduto> GetAllProdutoSimulador(Guid IDSimulacao)
        {
            return _Service.GetAllProdutoSimulador(IDSimulacao);
        }
        public IEnumerable<ProdutoFertilizante> GetAllProdutoFertilizante(Guid IDSimulacao)
        {
            return _Service.GetAllProdutoFertilizante(IDSimulacao);
        }
    }
}
