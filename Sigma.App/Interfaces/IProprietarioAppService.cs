using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IProprietarioAppService : IAppService<Proprietario>
    {
        /// <summary>
        /// <para>Retorna uma lista de proprietario. </para>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Proprietario_Viewer> GetAllProprietario(Guid IDUsuario);

        IEnumerable<Proprietario> ByName(string name);

        /// <summary>
        /// Retorna todos os proprietários que tem serviço na Safra informada
        /// </summary>
        /// <param name="IDSafra"></param>
        /// <returns></returns>
        IEnumerable<Proprietario> GetBySafra(Guid IDSafra, Guid IDUsuario);

        /// <summary>
        /// <para>Retorna uma lista de clientes/ proprietarios através do registro ("CPF/CNPJ").</para>
        /// <para>Método utilizado para validar os campos de registro não permitindo cadastros duplicados.</para>
        /// </summary>
        /// <param name="pfpj"></param>
        /// <returns></returns>
        IEnumerable<ProprietarioView> GetPfPj(string pfpj);


        /// <param name="IDSafra"></param>
        /// <returns>Retorna uma lista de dados de proprietário do banco do BNG. </returns>
        IEnumerable<BNGProprietario> GetProprietaioBNGSafra(Guid IDSafra);

    }
}
