using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class AmostraRepository : RepositoryBase<Amostra>, IAmostraRepository
    {
        public IEnumerable<Amostra> GetByCultura(Guid IDCultura, int? mediaT)
        {
            StringBuilder query = new StringBuilder();

            if (mediaT != null)
            {
                query.AppendLine("SELECT * FROM Amostra WHERE IDCultura = '" + IDCultura + "'");
            }
            else
            {
                query.AppendLine("SELECT NEWID() as objID, NEWID() as IDCultura, 'Descricao' AS descricao, 5 AS media, * FROM fGetMediaPadraoExportacao('" + IDCultura + "', " + mediaT + ")");
            }

            return Context.Database.SqlQuery<Amostra>(query.ToString());
        }
    }
}
