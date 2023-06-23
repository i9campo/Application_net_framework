using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class RolesService : Service<Roles>, IRolesService
    {
        private readonly IRolesRepository _repository;

        public RolesService(IRolesRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Roles> GetAllRoles()
        {
           return _repository.GetAllRoles(); 
        }

        public IEnumerable<UserView> GetAllRolesByUser(Guid IDUsuario)
        {
            return _repository.GetAllRolesByUser(IDUsuario); 
        }
        public IEnumerable<Roles> GetManagerPermission()
        {
            return _repository.GetManagerPermission();
        }
    }

}