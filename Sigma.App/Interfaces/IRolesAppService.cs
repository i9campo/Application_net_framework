using Sigma.App.Interfaces._Base;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IRolesAppService : IAppService<Roles>
    {
        /// <summary>
        /// <para>Método utilizado para retornar todas as roles cadastrada no banco de dados.</para>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Roles> GetAllRoles();

        /// <summary>
        /// <para>Retorna uma lista de permissões("ROLES") a partir de um ID de Usuário. </para>
        /// </summary>
        /// <param name="IDUsuario"></param>
        /// <returns></returns>
        IEnumerable<UserView> GetAllRolesByUser(Guid IDUsuario);
        IEnumerable<Roles> GetManagerPermission();
    }
}
