using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IEstagioCulturaAppService : IAppService<EstagioCultura>
    {
        /// <summary>
        /// <para>Retorna uma lista a partir do ID da Cultura.</para>
        /// </summary>
        /// <param name="IDCultura"></param>
        /// <returns></returns>
        IEnumerable<EstagioCultura> GetEstagioByCultura(Guid IDCultura);
    }
}
