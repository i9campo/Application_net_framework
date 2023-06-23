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
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public bool CheckedRole(string Role, string UserId)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("CASE ");
            query.AppendLine("  WHEN(ar.Name = '" + Role + "') THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END CheckedViewer");
            query.AppendLine("FROM AspNetUserRoles aur ");
            query.AppendLine("INNER JOIN AspNetUsers au ON au.Id = aur.UserId ");
            query.AppendLine("INNER JOIN AspNetRoles ar ON ar.Id = aur.RoleId ");
            query.AppendLine("WHERE ar.Name = '" + Role + "' AND au.Id = '" + UserId + "'");
            return Context.Database.SqlQuery<bool>(query.ToString()).SingleOrDefault();
        }

        public bool FindCheckedUserEmail(String Email)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT                    ");
            query.AppendLine("      CASE                ");
            query.AppendLine("          WHEN (COUNT(Id) = 0) THEN CONVERT(bit, 0) ELSE CONVERT(bit, 1) END AS Exist ");
            query.AppendLine("FROM AspNetUsers WHERE UserName = '" + Email + "'");
            return Context.Database.SqlQuery<bool>(query.ToString()).FirstOrDefault();
        }

        public UserView FindUser(Guid IDUsuario)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select Asp.UserId, aspClaims.ClaimValue, userAtivo.Ativo, userAtivo.IDEmpresa, aspNet.Email, Asp.RoleId, aspNet.EmailConfirmed, aspRoles.ViewerRoler  from AspNetUserRoles Asp");
            query.AppendLine("Inner Join AspNetUsers aspNet on asp.UserId = aspNet.Id ");
            query.AppendLine("Inner join AspNetUserClaims aspClaims on asp.UserId = aspClaims.UserId");
            query.AppendLine("Inner Join AspNetRoles aspRoles on aspRoles.Id = asp.RoleId");
            query.AppendLine("Inner join UsuarioAtivo userAtivo on userAtivo.IDUsuario = asp.UserId");
            query.AppendLine("where aspNet.Id = '" + IDUsuario + "' and aspRoles.Tipo is null and aspClaims.ClaimType = 'FirstName'");
            return Context.Database.SqlQuery<UserView>(query.ToString()).SingleOrDefault();
        }
        public IEnumerable<UserView> GetUserEmpresa(Guid IdEmpresa)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select Asp.UserId, aspClaims.ClaimValue, userAtivo.Ativo, aspNet.Email, Asp.RoleId, aspNet.EmailConfirmed, aspRoles.ViewerRoler  from AspNetUserRoles Asp");
            query.AppendLine("Inner Join AspNetUsers aspNet on asp.UserId = aspNet.Id ");
            query.AppendLine("Inner join AspNetUserClaims aspClaims on asp.UserId = aspClaims.UserId");
            query.AppendLine("Inner Join AspNetRoles aspRoles on aspRoles.Id = asp.RoleId");
            query.AppendLine("Inner join UsuarioAtivo userAtivo on userAtivo.IDUsuario = asp.UserId");
            query.AppendLine("where userAtivo.IDEmpresa = '" + IdEmpresa + "' and aspRoles.Tipo is null and aspClaims.ClaimType = 'FirstName'");
            return Context.Database.SqlQuery<UserView>(query.ToString());
        }

        public UserView FindUserByEmail(string email)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(" SELECT acF.Id AS IDFirstName,  au.*,  acF.ClaimValue AS FirstName FROM AspNetUsers au ");
            query.AppendLine(" INNER JOIN AspNetUserClaims acF ON acF.UserId = au.Id AND acF.ClaimType = 'FirstName' ");
            query.AppendLine(" WHERE Email = '" + email + "'");

            return Context.Database.SqlQuery<UserView>(query.ToString()).SingleOrDefault();
        }

        public IEnumerable<UserView> GetAllConectionUsers(Guid IDUsuario)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @IDEmpresa AS UNIQUEIDENTIFIER");
            query.AppendLine("SELECT @IDEmpresa = IDEmpresa FROM UsuarioAtivo WHERE IDUsuario = '" + IDUsuario + "'");

            query.AppendLine("If (@IDEmpresa = '256B44AE-25E7-456F-9786-1814A5118B5E')");
            query.AppendLine("   BEGIN                                               "); 

            query.AppendLine("      SELECT                                            ");
            query.AppendLine("          users.Id                AS UserId          ,");
            query.AppendLine("          roles.Id				AS RoleId		   ,");
            query.AppendLine("          usersclaims.ClaimValue  AS ClaimValue      ,");  
            query.AppendLine("          usersactivate.Conectado	AS Conectado	   ,");
            query.AppendLine("	        usersactivate.Ativo	    AS Ativo		   ,");
            query.AppendLine("          users.Email				AS Email		   ,");
            query.AppendLine("          users.EmailConfirmed	AS EmailConfirmed  ,");
            query.AppendLine("          roles.ViewerRoler		AS ViewerRoler     ,");
            query.AppendLine("          roles.Name              AS TipoUsuario     ,");
            query.AppendLine("          Empresa.nome as NomeEmpresa");
            query.AppendLine("      FROM AspNetUserRoles usersroles                 ");

            query.AppendLine("      INNER JOIN AspNetUsers  users			    ON usersroles.UserId = users.Id");
            query.AppendLine("      INNER JOIN AspNetRoles  roles			    ON usersroles.RoleId = roles.Id");
            query.AppendLine("      INNER JOIN UsuarioAtivo	usersactivate   ON users.Id			 = usersactivate.IDUsuario");
            query.AppendLine("      INNER JOIN Empresa						ON Empresa.objID     = usersactivate.IDEmpresa");
            query.AppendLine("      INNER JOIN AspNetUserClaims usersclaims ON usersroles.UserId = usersclaims.UserId");

            query.AppendLine("      where users.Id <> '"+IDUsuario+"' AND users.Id <> 'a4c9d59b-b4b2-499e-8c2c-47ab4e636e2b'  AND roles.Tipo is null AND usersclaims.ClaimType = 'FirstName'");

            query.AppendLine(" END ");
            query.AppendLine("ELSE ");
            query.AppendLine(" BEGIN ");
            query.AppendLine("      SELECT                                            ");
            query.AppendLine("          users.Id                AS UserId          ,");
            query.AppendLine("          roles.Id				AS RoleId		   ,");
            query.AppendLine("          usersclaims.ClaimValue  AS ClaimValue      ,");
            query.AppendLine("          usersactivate.Conectado	AS Conectado	   ,");
            query.AppendLine("	        usersactivate.Ativo	    AS Ativo		   ,");
            query.AppendLine("          users.Email				AS Email		   ,");
            query.AppendLine("          users.EmailConfirmed	AS EmailConfirmed  ,");
            query.AppendLine("          roles.ViewerRoler		AS ViewerRoler     ,");
            query.AppendLine("          roles.Name              AS TipoUsuario     ,"); 
            query.AppendLine("          Empresa.nome as NomeEmpresa");
            query.AppendLine("      FROM AspNetUserRoles usersroles                 ");

            query.AppendLine("      INNER JOIN AspNetUsers  users			    ON usersroles.UserId = users.Id");
            query.AppendLine("      INNER JOIN AspNetRoles  roles			    ON usersroles.RoleId = roles.Id");
            query.AppendLine("      INNER JOIN UsuarioAtivo	usersactivate   ON users.Id			 = usersactivate.IDUsuario");
            query.AppendLine("      INNER JOIN Empresa						ON Empresa.objID     = usersactivate.IDEmpresa");
            query.AppendLine("      INNER JOIN AspNetUserClaims usersclaims ON usersroles.UserId = usersclaims.UserId");

            query.AppendLine("      where users.Id <> '" + IDUsuario + "' AND users.Id <> 'a4c9d59b-b4b2-499e-8c2c-47ab4e636e2b'  AND roles.Tipo is null AND usersclaims.ClaimType = 'FirstName' AND Empresa.objID = @IDEmpresa");
            query.AppendLine(" END ");

            return Context.Database.SqlQuery<UserView>(query.ToString());
        }
    }
}

 