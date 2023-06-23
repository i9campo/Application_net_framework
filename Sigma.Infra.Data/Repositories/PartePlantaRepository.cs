using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class PartePlantaRepository : RepositoryBase<PartePlanta>, IPartePlantaRepository
    {
        public IEnumerable<PartePlanta> GetPartePlantaByCultura(Guid iDCultura)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM PartePlanta WHERE IDCultura  = '" + iDCultura + "'");
            List<PartePlanta> lst = Context.Database.SqlQuery<PartePlanta>(query.ToString()).ToList();
            return lst;
        }
    }
}