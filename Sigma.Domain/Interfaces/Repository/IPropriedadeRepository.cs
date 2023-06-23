using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IPropriedadeRepository : IRepository<Propriedade>
    {
        /// <summary>
        /// <para>Retorna todas as propriedades.</para>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Propriedade> GetAllPropriedade();

        IEnumerable<Propriedade> ByProprietario(string IDProprietario);

        /// <summary>
        /// Retorna todos as propriedades do proprietário que tem servico na safra
        /// </summary>
        /// <param name="IDProprietario"></param>
        /// <param name="IDSafra"></param>
        /// <returns></returns>
        IEnumerable<Propriedade> HasServicoInSafra(Guid IDProprietario, Guid IDSafra);

        IEnumerable<BNGPropriedade> GetPropriedadeBNGByProprietario(Guid IDSafra, Guid IDProprietario);
    }
}
