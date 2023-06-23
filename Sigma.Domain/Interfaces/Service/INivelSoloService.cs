using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Services
{
    public interface INivelSoloService : IService<NivelSolo>
    {
        /// <summary>
        /// <para> Retorna uma lista de nível de solo através do iD da cultura.</para>
        /// </summary>
        /// <param name="IDCultura"></param>
        /// <returns></returns>
        IEnumerable<NivelSolo> GetNivelByCultura(Guid IDCultura);

        /// <summary>
        /// <para>Retorna uma lista de nível de solo através do iD da cultura e o nome do elemento especifico.</para> 
        /// </summary>
        /// <param name="IDCultura"></param>
        /// <param name="elemento"></param>
        /// <returns></returns>
        IEnumerable<NivelSolo> GetNivelByElemento(Guid IDCultura, string elemento);


        NivelSolo GetBy(string elemento, Cultura c);
        NivelSolo[] GetSolo(Cultura c);
    }
}
