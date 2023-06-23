using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IFaixaTeorAppService : IAppService<FaixaTeor>
    {
        /// <summary>
        /// <para>Este metodo retorna uma lista de faixa teor a partir da parte da planta.</para>
        /// </summary>
        /// <param name="iDPartePlanta"></param>
        /// <returns></returns>
        IEnumerable<FaixaTeorView> GetByPartePlanta(Guid iDPartePlanta);
    }
}
