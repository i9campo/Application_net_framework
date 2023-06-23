using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class ParaMetroAreaRepository : RepositoryBase<ParametroArea>, IParametroAreaRepository
    {
        public ParametroAreaView GetAllParametroArea(Guid IDAreaServico, string safra, string area)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT c.nome AS nomeCultura, pa.* FROM ParametroArea pa");
            query.AppendLine("INNER JOIN AreaServico asv ON asv.objID = pa.IDAreaServico");
            query.AppendLine("INNER JOIN Area a ON a.objID = asv.IDArea");
            query.AppendLine("INNER JOIN Safra s ON s.objID = asv.IDSafra");
            query.AppendLine("INNER JOIN Cultura c on c.objID  = pa.IDUltimaCultura");
            query.AppendLine("WHERE a.objID = '" + area + "' and s.objID = '" + safra + " '");

            return Context.Database.SqlQuery<ParametroAreaView>(query.ToString()).SingleOrDefault();
        }

        public ParametroAreaView GetParametroareaByAreaServico(Guid IDAreaServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT ParametroArea.*, Cultura.nome AS culturaValue FROM ParametroArea                                           ");
            query.AppendLine("LEFT JOIN Cultura ON Cultura.objID = ParametroArea.IDUltimaCultura    ");
            query.AppendLine("WHERE IDAreaServico = '" + IDAreaServico + "'                         ");

            return Context.Database.SqlQuery<ParametroAreaView>(query.ToString()).SingleOrDefault();
        }
    }
}
