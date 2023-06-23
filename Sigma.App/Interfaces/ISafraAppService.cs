using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface ISafraAppService : IAppService<Safra>
    {
        IEnumerable<Safra> FindSafra(Guid IDSafra);

        /// <returns>Retorna uma lista de dados da tabela safra referente ao banco BNG. </returns>
        IEnumerable<SafraView> GetLstSafraBNG(); 

    }
}
