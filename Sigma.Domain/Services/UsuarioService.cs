using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class UsuarioService : Service<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepsitory;
        public UsuarioService(IUsuarioRepository usuarioRepository)
            :base(usuarioRepository)
        {
            _usuarioRepsitory = usuarioRepository; 
        }

        public bool CheckedRole(string Role, string UserId)
        {
            return _usuarioRepsitory.CheckedRole(Role, UserId); 
        }

        public bool FindCheckedUserEmail(string Email)
        {
            return _usuarioRepsitory.FindCheckedUserEmail(Email); 
        }

        public UserView FindUser(Guid IDUsuario)
        {
            return _usuarioRepsitory.FindUser(IDUsuario); 
        }

        public UserView FindUserByEmail(string email)
        {
            return _usuarioRepsitory.FindUserByEmail(email); 
        }
        public IEnumerable<UserView> GetUserEmpresa(Guid IdEmpresa)
        {
            return _usuarioRepsitory.GetUserEmpresa(IdEmpresa);
        }

        public IEnumerable<UserView> GetAllConectionUsers(Guid IDUsuario)
        {
            return _usuarioRepsitory.GetAllConectionUsers(IDUsuario); 
        }
    }
}
