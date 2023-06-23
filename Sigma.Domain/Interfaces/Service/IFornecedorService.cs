

using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Service
{
    public interface IFornecedorService : IService<Fornecedor>
    {
        IEnumerable<Fornecedor> GetPfPj(string pfpj);
    }
}
