using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using System;

namespace Sigma.Domain.Services
{
    public class UsuarioAtivoService : Service<UsuarioAtivo>, IUsuarioAtivoService
    {
        private readonly IUsuarioAtivoRepository _repository;

        public UsuarioAtivoService(IUsuarioAtivoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public UsuarioAtivo FindTypeUser(string IDUsuario)
        {
            return _repository.FindTypeUser(IDUsuario); 
        }
        public UsuarioAtivo UsuarioAtivo(Guid UserId)
        {
            return _repository.UsuarioAtivo(UserId);
        }
        public UsuarioAtivo CheckedUsuarioAtivo(Guid UserId)
        {
            return _repository.CheckedUsuarioAtivo(UserId);
        }
        public UsuarioAtivo UserLogout(Guid UserId)
        {
            return _repository.UserLogout(UserId);
        }
        public UsuarioAtivo UserLogin(Guid UserId)
        {
            return _repository.UserLogin(UserId);
        }
        public bool RegisterRoles(Roles roles)
        {
            return _repository.RegisterRoles(roles);

        }
        public bool DeleteRoles(Guid Id)
        {
            return _repository.DeleteRoles(Id);

        }
    }
}
