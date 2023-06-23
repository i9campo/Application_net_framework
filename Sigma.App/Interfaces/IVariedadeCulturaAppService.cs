using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IVariedadeCulturaAppService : IAppService<VariedadeCultura>
    {
        /// <summary>
        /// <para>Retorna uma lista de variedade cultura através do IDCultura.</para>
        /// </summary>
        /// <param name="IDCultura"></param>
        /// <returns></returns>
        IEnumerable<VariedadeCultura> GetVariedadeCulturaByCultura(Guid IDCultura);
    }
}
