using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IRecomendacaoFoliarRepository : IRepository<RecomendacaoFoliar>
    {
        /// <summary>
        /// <para>Retorna uma lista de recomendação foliar através do id da cultura.</para>
        /// </summary>
        /// <param name="IDCultura"></param>
        /// <returns></returns>
        IEnumerable<RecomendacaoFoliar> GetByCultura(Guid IDCultura);


        /// <summary>
        /// <para>Retorna uma lista contendo informações da recomendação e o nivel do solo.</para>
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="IDCultura"></param>
        /// <returns></returns>
        IEnumerable<RecomendacaoFoliarView> GetElementoRFNS(Guid objID, Guid IDCultura);
    }
}
