using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface ISafraRepository : IRepository<Safra>
    {
        IEnumerable<Safra> FindSafra(Guid IDSafra);
        /// <returns>Retorna uma lista de dados da tabela safra referente ao banco BNG. </returns>
        IEnumerable<SafraView> GetLstSafraBNG();
    }
}
