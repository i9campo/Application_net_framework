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
    public class PropriedadeRepository : RepositoryBase<Propriedade>, IPropriedadeRepository
    {
        public IEnumerable<Propriedade> ByProprietario(string IDProprietario)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM Propriedade WHERE IDProprietario ='" + IDProprietario + "' ORDER BY nome");
            List<Propriedade> lst = Context.Database.SqlQuery<Propriedade>(query.ToString()).ToList();
            return lst;
        }

        public IEnumerable<Propriedade> GetAllPropriedade()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM Propriedade ORDER BY nome");
            return Context.Database.SqlQuery<Propriedade>(query.ToString());
        }

        public IEnumerable<BNGPropriedade> GetPropriedadeBNGByProprietario(Guid IDSafra, Guid IDProprietario)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT                                                                                ");
            query.AppendLine("      DISTINCT p.objID                                                               ,");
            query.AppendLine("      p.nome                                                                          ");
            query.AppendLine("FROM  BNG.dbo.PropriedadeRural p                                                      ");
            query.AppendLine("                                                                                      ");

            query.AppendLine("INNER JOIN BNG.dbo.Area        a ON a.IDPropriedadeRural = p.objID                    ");
            query.AppendLine("INNER JOIN BNG.dbo.AreaSafra asv ON asv.IDArea           = a.objID                    ");
            query.AppendLine("WHERE asv.IDSafra = '" + IDSafra + "' AND p.IDProprietario = '" + IDProprietario + "' ORDER BY p.nome "); 
            return Context.Database.SqlQuery<BNGPropriedade>(query.ToString());
        }

        public IEnumerable<Propriedade> HasServicoInSafra(Guid IDProprietario, Guid IDSafra)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT DISTINCT pr.objID, pr.IDProprietario,pr.IDRegiao,pr.nome,pr.endereco,pr.cidade,pr.uf,pr.fone,pr.fax,pr.distancia,pr.areaTotal,");
            query.AppendLine("pr.areaIrrigada,pr.areaPlantada,pr.infoAdicionais,pr.ie, NULL as geo, NULL AS imgMap,NULL AS imgMGF ");
            query.AppendLine("FROM  AreaServico ars ");
            query.AppendLine("INNER JOIN Area ar ON ar.objID = ars.IDArea ");
            query.AppendLine("INNER JOIN Propriedade pr ON pr.objID = ar.IDPropriedade ");
            query.AppendLine("INNER JOIN Proprietario p ON p.objID = pr.IDProprietario ");
            query.AppendLine("INNER JOIN Servico s on s.objID = ars.IDServico ");
            query.AppendLine("WHERE ars.IDSafra = '" + IDSafra + "' and p.ativo = 1 and pr.IDProprietario = '" + IDProprietario + "' ");
            query.AppendLine("ORDER BY pr.nome");
            List<Propriedade> lst = Context.Database.SqlQuery<Propriedade>(query.ToString()).ToList();
            return lst;
        }
    }
}