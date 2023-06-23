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
    public class ParametroPropriedadeRepository : RepositoryBase<ParametroPropriedade>, IParametroPropriedadeRepository
    {
        public IEnumerable<ParametroPropriedade> FindParametroPropriedade(Guid objID)
        {
            throw new NotImplementedException();
        }

        public ParametroPropriedade GetByAreaPropriedade(Guid IDSafra, Guid IDPropriedade)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM ParametroPropriedade WHERE IDSafra = '" + IDSafra + "' AND IDPropriedade ='" + IDPropriedade + "'");

            return Context.Database.SqlQuery<ParametroPropriedade>(query.ToString()).SingleOrDefault();
        }

        ParametroSoloView IParametroPropriedadeRepository.GetSolo(Guid iDAreaServico)
        {
            throw new NotImplementedException();
        }
    }
}
