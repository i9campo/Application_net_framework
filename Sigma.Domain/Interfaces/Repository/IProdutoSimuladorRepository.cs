using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IProdutoSimuladorRepository : IRepository<ProdutoSimulador>
    {
        IEnumerable<ProdutoSimuladorProduto> GetAllProdutoSimulador(Guid IDSimulacao);
        IEnumerable<ProdutoFertilizante> GetAllProdutoFertilizante(Guid IDSimulacao);
    }
}
