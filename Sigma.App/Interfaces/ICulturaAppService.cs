using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using Sigma.Domain.ViewTables;

namespace Sigma.App.Interfaces
{
    public interface ICulturaAppService : IAppService<Cultura>
    {
        IEnumerable<Cultura> ByName(String name);
        /// <summary>
        /// Retorna uma lista de Cultura contendo o nome das unidade de medida. 
        /// </summary>
        /// <returns></returns>
        IEnumerable<CulturaView> GetAllCultura();

        /// <summary>
        /// Retorna uma cultura contendo o nome da unidade de medida. 
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        CulturaView FindCultura(Guid objID);
    }
}
