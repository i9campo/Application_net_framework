using Sigma.Domain.Entities;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Services
{
    public interface IEmpresaService : IService<Empresa>
    {
        /// <summary>
        /// <para>Método utilizado para retornar a empresa referente ao usuário logado. </para>
        /// <para>Necessário passar como parâmetro o ID do Usuário logado. </para>
        /// </summary>
        /// <returns></returns>
        Empresa GetEmpresa(Guid IDUsuario);


        /// <summary>
        /// <para>Método utilizado para retornar uma lista de usuários referente a empresa, através do ID da Empresa. </para>
        /// </summary>
        IEnumerable<UsuarioAtivoView> GetUserActivate(Guid IDEmpresa);

        /// <summary>
        /// <para>Método utilizado para verificar se a empresa está ativa ou não a partir do ID do usuário informado. </para>
        /// </summary>
        /// <param name="IDUsuario"></param>
        /// <returns>Resultado Boolean</returns>
        bool CheckedEmpresaActivateByUsuario(Guid IDUsuario);

        /// <summary>
        /// <para>Método utilizado para retornar lista de empresas onde não contem os dados da empresa do usuário logado. </para>
        /// </summary>
        /// <param name="IDUsuario"></param>
        /// <returns></returns>
        IEnumerable<Empresa> GetListEmpresa(Guid IDUsuario);
    }
}
