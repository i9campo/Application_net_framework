using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface INivelSoloRepository : IRepository<NivelSolo>
    {
        /// <summary>
        /// <para> Retorna uma lista de nível de solo através do iD da cultura.</para>
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        IEnumerable<NivelSolo> GetNivelByCultura(Guid objID);

        /// <summary>
        /// <para>Retorna uma lista de nível de solo através do iD da cultura e o nome do elemento especifico.</para> 
        /// </summary>
        /// <param name="IDCultura"></param>
        /// <param name="elemento"></param>
        /// <returns></returns>
        IEnumerable<NivelSolo> GetNivelByElemento(Guid IDCultura, string elemento);

        NivelSolo GetBy(string elemento, Guid Cultura);
    }
}
