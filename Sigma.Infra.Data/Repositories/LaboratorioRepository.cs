using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class LaboratorioRepository : RepositoryBase<Laboratorio>, ILaboratorioRepository
    {
        public IEnumerable<Laboratorio> GetLaboratorioByCNPJ(string CNPJ)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM Laboratorio WHERE cnpj = '" + CNPJ + "'");
            return Context.Database.SqlQuery<Laboratorio>(query.ToString());
        }
    }
}