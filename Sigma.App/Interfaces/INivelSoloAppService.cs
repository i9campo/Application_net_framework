using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface INivelSoloAppService : IAppService<NivelSolo>
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
    }
}
