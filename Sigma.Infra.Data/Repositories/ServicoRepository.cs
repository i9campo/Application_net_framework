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
    public class ServicoRepository : RepositoryBase<Servico>, IServicoRepository
    {
        public IEnumerable<Servico> GetAllServico()
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT * From Servico ORDER BY nome ");
            return Context.Database.SqlQuery<Servico>(query.ToString());
        }

        public IEnumerable<Servico> GetByAreaSafra(Guid IDArea, Guid IDSafra)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT Servico.* FROM Servico ");
            query.AppendLine("INNER JOIN AreaServico ON AreaServico.IDServico = Servico.objID ");
            query.AppendLine("WHERE AreaServico.IDArea = '" + IDArea + "' AND AreaServico.IDSafra = '" + IDSafra + "' ");
            query.AppendLine("ORDER BY Servico.nome");
            return Context.Database.SqlQuery<Servico>(query.ToString()).ToList();
        }

        public IEnumerable<AreaServicoView> GetServico(String IDArea, String IDSafra)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @AreaID TABLE ([ID][NVARCHAR](MAX) NULL)\n");
            query.AppendLine("BEGIN \n INSERT INTO @AreaID SELECT value FROM STRING_SPLIT('" + IDArea + "', '_')\n END\n");
            query.AppendLine("Select distinct AreaServico.numServico, ar.nome as Servico, concat (AreaServico.numServico,'_',ar.objID) as ID from AreaServico");
            query.AppendLine("Inner Join Area as ar ON ar.objID in (SELECT value FROM STRING_SPLIT('" + IDArea + "', '_'))");
            query.AppendLine(" where AreaServico.IDArea in (SELECT value FROM STRING_SPLIT('" + IDArea + "', '_')) and AreaServico.IDSafra = '" + IDSafra + "' order by Servico");
            return Context.Database.SqlQuery<AreaServicoView>(query.ToString()).ToList();
        }
    }
}