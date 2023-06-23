using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Service
{
    public interface IUsuarioService : IService<Usuario>
    {
        /// <summary>
        /// <para>Método utilizado para verificar sé o E-mail informado existe ou não na base de dados. </para>
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        bool FindCheckedUserEmail(String Email);
        IEnumerable<UserView> GetUserEmpresa(Guid idEmpresa);

        /// <summary>
        /// <para>Método utilizado para trazer todas as informações de um usuário através do e-mail.</para>
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        UserView FindUserByEmail(String email);

        /// <summary>
        /// <para>Retorna dados do usuário a partir do ID do Usuário. </para>
        /// </summary>
        /// <param name="IDUsuario"></param>
        /// <returns></returns>
        UserView FindUser(Guid IDUsuario);

        /// <summary>
        /// <para> Retorna uma lista de todos os usuários que estão conectados e desconectados.  </para>
        /// <para> O Parâmetro IDUsuario será utilizado para remover o usuário que está logado da lista de usuários.  </para>
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        IEnumerable<UserView> GetAllConectionUsers(Guid IDUsuario);

        /// <summary>
        /// <para>Método utilizado para verificar se a role foi marcada ou não. </para>
        /// </summary>
        /// <param name="Role"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        bool CheckedRole(String Role, String UserId);
    }
}
