using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Service._Base;
using System;

namespace Sigma.Domain.Interfaces.Service
{
    public interface IUsuarioAtivoService : IService<UsuarioAtivo>
    {
        /// <summary>
        /// <para>Retorna os dados do usuário ativo a partir do ID do usuário. </para>
        /// </summary>
        /// <param name="IDUsuario"></param>
        /// <returns></returns>
        UsuarioAtivo FindTypeUser(String IDUsuario);
        UsuarioAtivo UsuarioAtivo(Guid userId);
        UsuarioAtivo CheckedUsuarioAtivo(Guid userId);
        UsuarioAtivo UserLogout(Guid userId);
        UsuarioAtivo UserLogin(Guid userId);
        bool RegisterRoles(Roles roles);
        bool DeleteRoles(Guid Id);
    }
}
