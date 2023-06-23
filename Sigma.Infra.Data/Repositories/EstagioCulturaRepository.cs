using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class EstagioCulturaRepository : RepositoryBase<EstagioCultura>, IEstagioCulturaRepository
    {
        public IEnumerable<EstagioCultura> GetEstagioByCultura(Guid IDCultura)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select * from EstagioCultura WHERE IDCultura = '" + IDCultura + "'");
            List<EstagioCultura> lst = Context.Database.SqlQuery<EstagioCultura>(query.ToString()).ToList();
            return lst;
        }
    }
}