using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Services
{
    public interface ISafraService : IService<Safra>
    {
        IEnumerable<Safra> FindSafra(Guid IDSafra);

        /// <returns>Retorna uma lista de dados da tabela safra referente ao banco BNG. </returns>
        IEnumerable<SafraView> GetLstSafraBNG();
    }
}
