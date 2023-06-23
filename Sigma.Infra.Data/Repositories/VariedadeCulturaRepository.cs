using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class VariedadeCulturaRepository : RepositoryBase<VariedadeCultura>, IVariedadeCulturaRepository
    {
        public IEnumerable<VariedadeCultura> GetVariedadeCulturaByCultura(Guid IDCultura)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT * FROM VariedadeCultura Where IDCultura = '" + IDCultura + "'");
            List<VariedadeCultura> lst = Context.Database.SqlQuery<VariedadeCultura>(query.ToString()).ToList();
            return lst;
        }
    }
}