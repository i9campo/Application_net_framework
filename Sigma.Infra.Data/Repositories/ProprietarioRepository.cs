using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class ProprietarioRepository : RepositoryBase<Proprietario>, IProprietarioRepository
    {
        public IEnumerable<Proprietario> ByName(string name)
        {
            return Context.Set<Proprietario>().Where(o => o.nome.ToLower().Contains(name.ToLower())).ToList();
        }

        public IEnumerable<ProprietarioView> GetPfPj(string pfpj)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM Proprietario WHERE pfpj = '" + pfpj + "'");
            return Context.Database.SqlQuery<ProprietarioView>(query.ToString());
        }

        public IEnumerable<Proprietario> GetBySafra(Guid IDSafra, Guid IDUsuario)
        {
            StringBuilder EmpresaID = new StringBuilder();
            EmpresaID.AppendLine("DECLARE @IDEmpresa AS UNIQUEIDENTIFIER");
            EmpresaID.AppendLine("SELECT @IDEmpresa = IDEmpresa FROM UsuarioAtivo WHERE IDUsuario = '" + IDUsuario + "'");
            EmpresaID.AppendLine("SELECT CONVERT(NVARCHAR(MAX),@IDEmpresa)");

            String IDEmpresa = Context.Database.SqlQuery<String>(EmpresaID.ToString()).FirstOrDefault();

            StringBuilder query = new StringBuilder();
            query.AppendLine("      SELECT DISTINCT p.*  FROM AreaServico ars                                           ");
            query.AppendLine("      INNER JOIN Area ar          ON ar.objID = ars.IDArea                                ");
            query.AppendLine("      INNER JOIN Propriedade pr   ON pr.objID = ar.IDPropriedade                          ");
            query.AppendLine("      INNER JOIN Proprietario p   ON p.objID = pr.IDProprietario                                     ");
            query.AppendLine("      INNER JOIN Servico s        ON s.objID = ars.IDServico                                         ");


            if (IDEmpresa.Equals("256B44AE-25E7-456F-9786-1814A5118B5E"))
                query.AppendLine("      WHERE ars.IDSafra = '" + IDSafra.ToString() + "' and p.ativo = 1 ORDER BY p.nome               ");
            else
                query.AppendLine("      WHERE ars.IDSafra = '" + IDSafra.ToString() + "' and p.ativo = 1  AND p.IDEmpresa = '"+IDEmpresa+"' ORDER BY p.nome ");

            return Context.Database.SqlQuery<Proprietario>(query.ToString());
        }

        public IEnumerable<Proprietario_Viewer> GetAllProprietario(Guid IDUsuario)
        {
            StringBuilder EmpresaID = new StringBuilder();
            EmpresaID.AppendLine("DECLARE @IDEmpresa AS UNIQUEIDENTIFIER");
            EmpresaID.AppendLine("SELECT @IDEmpresa = IDEmpresa FROM UsuarioAtivo WHERE IDUsuario = '"+IDUsuario+"'");
            EmpresaID.AppendLine("SELECT CONVERT(NVARCHAR(MAX),@IDEmpresa)"); 

            var IDEmpresa = Context.Database.SqlQuery<string>(EmpresaID.ToString()).FirstOrDefault();

            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT                                            ");
            query.AppendLine("  objID                                       ,   ");
            query.AppendLine("  nome                                        ,   ");
            query.AppendLine("  pfpj                                        ,   ");
            query.AppendLine("  cidade                                      ,   ");
            query.AppendLine("  uf                                          ,   ");
            query.AppendLine("  cep                                         ,   ");
            query.AppendLine("  fone                                        ,   ");
            query.AppendLine("  email                                       ,   ");
            query.AppendLine("  infoAdicionais                              ,   ");
            query.AppendLine("  representante                               ,   ");
            query.AppendLine("  cpfRepresentante                            ,   ");
            query.AppendLine("  telefoneRepresentante                       ,   ");
            query.AppendLine("  CASE                                            ");
            query.AppendLine("     WHEN(CONVERT(BIT,ativo) = 1) THEN 'ATIVO' ELSE 'DESATIVADO' END AS ativo , ");
            query.AppendLine("  IDEmpresa                                       ");
            query.AppendLine("FROM Proprietario                                 "); 
            //if (!IDEmpresa.Equals("256B44AE-25E7-456F-9786-1814A5118B5E"))
            //    query.AppendLine("WHERE Proprietario.IDEmpresa = '" + IDEmpresa + "'  ");
            
            query.AppendLine(" ORDER BY ativo ASC, nome ASC                       ");

            return Context.Database.SqlQuery<Proprietario_Viewer>(query.ToString());
        }

        public IEnumerable<BNGProprietario> GetProprietaioBNGSafra(Guid IDSafra)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT                                                    ");
            query.AppendLine("      DISTINCT p.objID                                   ,");
            query.AppendLine("      p.nome                                              ");
            query.AppendLine("FROM BNG.dbo.Proprietario p                               ");

            query.AppendLine("INNER JOIN BNG.dbo.PropriedadeRural pr ON pr.IDProprietario = p.objID ");
            query.AppendLine("INNER JOIN BNG.dbo.Area a ON a.IDPropriedadeRural = pr.objID          ");
            query.AppendLine("INNER JOIN BNG.dbo.AreaSafra ars ON ars.IDArea = a.objID              ");
            query.AppendLine("WHERE ars.IDSafra = '" + IDSafra + "' ORDER BY p.nome");
            return Context.Database.SqlQuery<BNGProprietario>(query.ToString());
        }
    }
}