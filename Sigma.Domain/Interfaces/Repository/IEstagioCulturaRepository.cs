using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IEstagioCulturaRepository : IRepository<EstagioCultura>
    {
        /// <summary>
        /// <para>Retorna uma lista a partir do ID da Cultura.</para>
        /// </summary>
        /// <param name="IDCultura"></param>
        /// <returns></returns>
        IEnumerable<EstagioCultura> GetEstagioByCultura(Guid IDCultura);
    }
}
