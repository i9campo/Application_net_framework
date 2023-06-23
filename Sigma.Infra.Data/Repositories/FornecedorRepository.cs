using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class FornecedorRepository : RepositoryBase<Fornecedor>, IFornecedorRepository
    {
        public IEnumerable<Fornecedor> GetPfPj(string pfpj)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM Proprietario WHERE pfpj = '" + pfpj + "'");
            return Context.Database.SqlQuery<Fornecedor>(query.ToString());
        }
    }
}
