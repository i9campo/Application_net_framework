using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Service
{
    public interface IProdutoSimuladorService : IService<ProdutoSimulador>
    {
        IEnumerable<ProdutoSimuladorProduto> GetAllProdutoSimulador(Guid IDSimulacao);
        IEnumerable<ProdutoFertilizante> GetAllProdutoFertilizante(Guid iDSimulacao);
    }
}
