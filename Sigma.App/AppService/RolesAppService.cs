using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class RolesAppService : AppService<Roles>, IRolesAppService
    {
        private readonly IRolesService _Service;  
        public RolesAppService(IRolesService service)
            :base(service)
        {
            _Service = service; 
        }

        public IEnumerable<Roles> GetAllRoles()
        {
            return _Service.GetAllRoles(); 
        }

        public IEnumerable<UserView> GetAllRolesByUser(Guid IDUsuario)
        {
            return _Service.GetAllRolesByUser(IDUsuario); 
        }

        public IEnumerable<Roles> GetManagerPermission()
        {
            return _Service.GetManagerPermission();
        }
    }
}
