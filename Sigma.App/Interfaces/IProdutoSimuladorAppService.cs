using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IProdutoSimuladorAppService : IAppService<ProdutoSimulador>
    {
        IEnumerable<ProdutoSimuladorProduto> GetAllProdutoSimulador(Guid IDSimulacao);
        IEnumerable<ProdutoFertilizante> GetAllProdutoFertilizante(Guid IDSimulacao);
    }
}
