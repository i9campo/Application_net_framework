using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        IEnumerable<Fornecedor> GetPfPj(string pfpj);
    }
}
