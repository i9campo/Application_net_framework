using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IFornecedorAppService : IAppService<Fornecedor>
    {
        IEnumerable<Fornecedor> GetPfPj(string pfpj);
    }
}
