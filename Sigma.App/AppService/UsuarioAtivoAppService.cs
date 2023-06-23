using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Service;
using System;

namespace Sigma.App.AppService
{
    public class UsuarioAtivoAppService : AppService<UsuarioAtivo>, IUsuarioAtivoAppService
    {
        private readonly IUsuarioAtivoService _Service;
        public UsuarioAtivoAppService(IUsuarioAtivoService service)
            :base(service)
        {
            _Service = service; 
        }

        public UsuarioAtivo FindTypeUser(string IDUsuario)
        {
            return _Service.FindTypeUser(IDUsuario); 
        }

        public UsuarioAtivo UsuarioAtivo(Guid UserId)
        {
            return _Service.UsuarioAtivo(UserId);
        }
        public UsuarioAtivo CheckedUsuarioAtivo(Guid UserId)
        {
            return _Service.CheckedUsuarioAtivo(UserId);
        }

        public UsuarioAtivo UserLogout(Guid UserId)
        {
            return _Service.UserLogout(UserId);
        }

        public UsuarioAtivo UserLogin(Guid UserId)
        {
            return _Service.UserLogin(UserId);
        }

        public bool RegisterRoles(Roles roles)
        {

            return _Service.RegisterRoles(roles);

        }
        public bool DeleteRoles(Guid Id)
        {
            return _Service.DeleteRoles(Id);

        }

    }
}
