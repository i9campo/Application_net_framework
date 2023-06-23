using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IPropriedadeAppService : IAppService<Propriedade>
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


        /// <param name="IDProprietario"></param>
        /// <returns>Retorna uma lista de dados do banco BNG referente a propriedade.</returns>
        IEnumerable<BNGPropriedade> GetPropriedadeBNGByProprietario(Guid IDSafra, Guid IDProprietario);

    }
}
