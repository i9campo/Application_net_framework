using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class UsuarioAtivoRepository : RepositoryBase<UsuarioAtivo>, IUsuarioAtivoRepository
    {
        public UsuarioAtivo FindTypeUser(string IDUsuario)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM UsuarioAtivo WHERE IDUsuario = '" + IDUsuario + "'");
            return Context.Database.SqlQuery<UsuarioAtivo>(query.ToString()).SingleOrDefault();
        }

        public UsuarioAtivo UsuarioAtivo(Guid UserId)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM UsuarioAtivo WHERE IDUsuario = '" + UserId + "'");
            return Context.Database.SqlQuery<UsuarioAtivo>(query.ToString()).SingleOrDefault();
        }

        public UsuarioAtivo CheckedUsuarioAtivo(Guid UserId)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM UsuarioAtivo WHERE IDUsuario = '" + UserId + "'");
            return Context.Database.SqlQuery<UsuarioAtivo>(query.ToString()).SingleOrDefault();
        }
        public UsuarioAtivo UserLogout(Guid UserId)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("UPDATE UsuarioAtivo SET Conectado = 'false' WHERE UsuarioAtivo.IDUsuario = '" + UserId + "'");
            return Context.Database.SqlQuery<UsuarioAtivo>(query.ToString()).SingleOrDefault();
        }
        public UsuarioAtivo UserLogin(Guid UserId)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("UPDATE UsuarioAtivo SET conectado = 'true' WHERE UsuarioAtivo.IDUsuario = '" + UserId + "'");

            return Context.Database.SqlQuery<UsuarioAtivo>(query.ToString()).SingleOrDefault();

        }
        public bool RegisterRoles(Roles roles)
        {
            bool retorno = false;
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO AspNetRoles(Id, Name, Tipo, ViewerRoler) VALUES('" + roles.Id + "','" + roles.Name + "', '" + roles.Tipo + "', '" + roles.ViewerRoler + "')");
                Context.Database.SqlQuery<Roles>(query.ToString()).SingleOrDefault();
                retorno = true;
            }
            catch (Exception e)
            {
                retorno = false;
            }
            return retorno;
        }

        public bool DeleteRoles(Guid Id)
        {
            bool retorno = false;
            try
            {
                String Delete;
                Delete = "Delete from AspNetRoles where Id = '" + Id.ToString() + "'";
                Context.Database.ExecuteSqlCommand(Delete);
                retorno = true;
            }
            catch (Exception e)
            {
                retorno = false;
            }

            return retorno;

        }
    }
}
