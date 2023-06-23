using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class UsuarioAppService : AppService<Usuario>, IUsuarioAppService
    {
        private readonly IUsuarioService _Service;
        public UsuarioAppService(IUsuarioService service)
            :base(service)
        {
            _Service = service; 
        }

        public bool CheckedRole(string Role, string UserId)
        {
            return _Service.CheckedRole(Role, UserId); 
        }
        public IEnumerable<UserView> GetUserEmpresa(Guid IdEmpresa)
        {
            return _Service.GetUserEmpresa(IdEmpresa);
        }
        public bool FindCheckedUserEmail(string Email)
        {
            return _Service.FindCheckedUserEmail(Email); 
        }

        public UserView FindUser(Guid IDUsuario)
        {
            return _Service.FindUser(IDUsuario); 
        }

        public UserView FindUserByEmail(string email)
        {
            return _Service.FindUserByEmail(email); 
        }

        public IEnumerable<UserView> GetAllConectionUsers(Guid IDUsuario)
        {
            return _Service.GetAllConectionUsers(IDUsuario); 
        }
    }
}
