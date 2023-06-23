using Sigma.Domain.Entities;
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
    public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository
    {
        public bool CheckedEmpresaActivateByUsuario(Guid IDUsuario)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("  SELECT                            ");
            query.AppendLine("        CONVERT(BIT, Empresa.ativo) ");
            query.AppendLine("  FROM Empresa                      ");
            query.AppendLine("  INNER JOIN UsuarioAtivo ON UsuarioAtivo.IDEmpresa = Empresa.objID ");
            query.AppendLine("  WHERE UsuarioAtivo.IDUsuario = '" + IDUsuario + "'");

            return Context.Database.SqlQuery<bool>(query.ToString()).FirstOrDefault(); 

        }

        public Empresa GetEmpresa(Guid IDUsuario)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT Empresa.* FROM Empresa");
            query.AppendLine("INNER JOIN UsuarioAtivo ON Empresa.objID = UsuarioAtivo.IDEmpresa");
            query.AppendLine("WHERE UsuarioAtivo.IDUsuario = '" + IDUsuario + "'");
            return Context.Database.SqlQuery<Empresa>(query.ToString()).FirstOrDefault();
        }

        public IEnumerable<UsuarioAtivoView> GetUserActivate(Guid IDEmpresa)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("  SELECT                                                                 ");
            query.AppendLine("        CONVERT(UNIQUEIDENTIFIER, UsuarioAtivo.objID)     AS objID     , ");
            query.AppendLine("        CONVERT(UNIQUEIDENTIFIER, UsuarioAtivo.IDEmpresa) AS IDEmpresa , ");
            query.AppendLine("        CONVERT(NVARCHAR(36), UsuarioAtivo.IDUsuario) AS IDUsuario , ");
            query.AppendLine("        UsuarioAtivo.Ativo                                AS Ativo     , ");
            query.AppendLine("        UsuarioAtivo.Conectado                            AS Conectado , ");
            query.AppendLine("        UsuarioAtivo.IDEmpresa                            AS IDEmpresa , ");
            query.AppendLine("        AspNetRoles.ViewerRoler AS ViewerRoler                           ");
            query.AppendLine("  FROM Empresa                                                           ");
            query.AppendLine("  INNER JOIN UsuarioAtivo ON UsuarioAtivo.IDEmpresa = Empresa.objID      ");
            query.AppendLine("  INNER JOIN AspNetUserRoles ON UsuarioAtivo.IDUsuario = AspNetUserRoles.UserId ");
            query.AppendLine("  RIGHT JOIN  AspNetRoles ON AspNetRoles.Id = AspNetUserRoles.RoleId AND AspNetRoles.Tipo IS NULL ");

            query.AppendLine("  WHERE   ");
            query.AppendLine("       UsuarioAtivo.objID IS NOT NULL AND     ");
            query.AppendLine("       UsuarioAtivo.IDEmpresa IS NOT NULL AND ");
            query.AppendLine("       UsuarioAtivo.IDUsuario IS NOT NULL AND ");
            query.AppendLine("       UsuarioAtivo.Ativo IS NOT NULL     AND ");
            query.AppendLine("       UsuarioAtivo.Conectado IS NOT NULL AND ");
            query.AppendLine("       UsuarioAtivo.IDEmpresa = '" + IDEmpresa + "'");
            query.AppendLine(" GROUP BY ");
            query.AppendLine("       UsuarioAtivo.objID     ,");
            query.AppendLine("       UsuarioAtivo.IDEmpresa ,");
            query.AppendLine("       UsuarioAtivo.IDUsuario ,");
            query.AppendLine("       UsuarioAtivo.Ativo     ,");
            query.AppendLine("       UsuarioAtivo.Conectado ,");
            query.AppendLine("       AspNetRoles.ViewerRoler ");


            return Context.Database.SqlQuery<UsuarioAtivoView>(query.ToString()).ToList(); 
        }
        public IEnumerable<Empresa> GetListEmpresa(Guid IDUsuario)
        {
            StringBuilder Query = new StringBuilder();
            Query.AppendLine(" SELECT* FROM Empresa ");
            Query.AppendLine(" INNER JOIN UsuarioAtivo ON UsuarioAtivo.IDUsuario = '" + IDUsuario + "' ");
            Query.AppendLine(" WHERE Empresa.objID<> UsuarioAtivo.IDEmpresa ");

            return Context.Database.SqlQuery<Empresa>(Query.ToString()).ToList();

        }
    }
}