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
    public class SafraRepository : RepositoryBase<Safra>, ISafraRepository
    {
        public IEnumerable<Safra> FindSafra(Guid IDSafra)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM Safra WHERE objID = '" + IDSafra.ToString() + "'");
            return Context.Database.SqlQuery<Safra>(query.ToString());
        }

        public IEnumerable<SafraView> GetLstSafraBNG()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM BNG.dbo.Safra ORDER BY descricao");
            return Context.Database.SqlQuery<SafraView>(query.ToString()).ToList();
        }
    }
}