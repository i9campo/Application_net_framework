using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Service
{
    public interface IVariedadeCulturaService : IService<VariedadeCultura>
    {
        /// <summary>
        /// <para>Retorna uma lista de variedade cultura através do IDCultura.</para>
        /// </summary>
        /// <param name="IDCultura"></param>
        /// <returns></returns>
        IEnumerable<VariedadeCultura> GetVariedadeCulturaByCultura(Guid IDCultura);
    }
}
