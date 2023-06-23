using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class SequenciaImportacaoRepository : RepositoryBase<SequenciaImportacao>, ISequenciaImportacaoRepository
    {
        public SequenciaImportacao FindSequenciaByLaboratorio(Guid IDLaboratorio)
        {
            StringBuilder query = new StringBuilder(); 
            query.AppendLine("SELECT * FROM SequenciaImportacao WHERE IDLaboratorio = '" + IDLaboratorio.ToString() + "'");
            return Context.Database.SqlQuery<SequenciaImportacao>(query.ToString()).FirstOrDefault();
        }
    }
}
