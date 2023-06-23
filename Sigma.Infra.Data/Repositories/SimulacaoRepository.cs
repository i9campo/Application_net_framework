using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class SimulacaoRepository : RepositoryBase<Simulacao>, ISimulacaoRepository
    {
        public Simulacao GetAllSimulacao(Guid IDAreaServico, int opcao)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select * from Simulacao where IDAreaServico = '" + IDAreaServico + "' and opcao = '" + opcao + "' ");
            return Context.Database.SqlQuery<Simulacao>(query.ToString()).SingleOrDefault();
        }
        public Simulacao GetCultura()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select objID, nome from Cultura");
            return Context.Database.SqlQuery<Simulacao>(query.ToString()).SingleOrDefault();
        }
    }
}
