using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using System;
using System.Collections.Generic;
using Sigma.Domain.ViewTables;
namespace Sigma.Domain.Interfaces.Repository
{
    public interface ICulturaRepository : IRepository<Cultura>
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
