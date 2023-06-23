using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class ProdutoSimuladorRepository : RepositoryBase<ProdutoSimulador>, IProdutoSimuladorRepository
    {
        public IEnumerable<ProdutoSimuladorProduto> GetAllProdutoSimulador(Guid IDSimulacao)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select* from ProdutoSimulador prosimu");
            query.AppendLine("INNER JOIN Produto pro ON pro.objID = prosimu.IDProduto");
            query.AppendLine("where IDSimulacao = '" + IDSimulacao + "'");


            return Context.Database.SqlQuery<ProdutoSimuladorProduto>(query.ToString()).ToList();
        }

        public IEnumerable<ProdutoFertilizante> GetAllProdutoFertilizante(Guid IDSimulacao)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select*, cult.nome as nomeCultura from ProdutoSimulador prosimu");
            query.AppendLine("INNER JOIN Produto pro ON pro.objID = prosimu.IDProduto");
            query.AppendLine("INNER JOIN Cultura cult ON cult.objID = prosimu.IDCultura");
            query.AppendLine("INNER JOIN EstagioCultura Est ON Est.objID = prosimu.IDEstagioCultura");
            query.AppendLine("where IDSimulacao ='" + IDSimulacao + "'");


            return Context.Database.SqlQuery<ProdutoFertilizante>(query.ToString()).ToList();
        }
    }
}

