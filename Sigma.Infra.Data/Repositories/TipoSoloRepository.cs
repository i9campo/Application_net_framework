using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class TipoSoloRepository : RepositoryBase<TipoSolo>, ITipoSoloRepository
    {
        public TipoSolo FindTipoSolo(string tpSolo)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select* from TipoSolo where Abreviacao = '" + tpSolo + "'");
            return Context.Database.SqlQuery<TipoSolo>(query.ToString()).SingleOrDefault();
        }
    }
}
