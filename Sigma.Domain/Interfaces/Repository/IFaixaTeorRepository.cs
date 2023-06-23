using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IFaixaTeorRepository : IRepository<FaixaTeor>
    {
        /// <summary>
        /// <para>Este metodo retorna uma lista de faixa teor a partir da parte da planta.</para>
        /// </summary>
        /// <param name="iDPartePlanta"></param>
        /// <returns></returns>
        IEnumerable<FaixaTeorView> GetByPartePlanta(Guid iDPartePlanta);

        IEnumerable<FaixaTeor> GetBy(string nutriente, Guid EstagioCultura, Guid PartePlanta);
    }
}
