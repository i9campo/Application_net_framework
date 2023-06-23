using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class RolesRepository : RepositoryBase<Roles>, IRolesRepository
    {
        public IEnumerable<Roles> GetAllRoles()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM AspNetRoles WHERE Tipo IS NOT NULL ORDER BY Tipo");
            return Context.Database.SqlQuery<Roles>(query.ToString()).ToList();
        }

        public IEnumerable<UserView> GetAllRolesByUser(Guid IDUsuario)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select CASE WHEN aspnet.RoleId is not null THEN '1'   ELSE  '0'");
            query.AppendLine("END Checado, asp.*, aspnet.* from AspNetRoles as asp left join AspNetUserRoles as aspnet on asp.Id = aspnet.RoleId and aspnet.UserId = '" + IDUsuario + "'");
            query.AppendLine("where asp.Tipo is not null order by  asp.ViewerRoler  asc, asp.Name desc");
            return Context.Database.SqlQuery<UserView>(query.ToString());
        }

        public IEnumerable<Roles> GetManagerPermission()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM AspNetRoles WHERE Tipo IS NULL ORDER BY Tipo");
            return Context.Database.SqlQuery<Roles>(query.ToString()).ToList();
        }
    }
}
