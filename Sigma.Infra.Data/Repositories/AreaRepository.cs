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
    public class AreaRepository : RepositoryBase<Area>, IAreaRepository
    {
        public IEnumerable<Area> ByName(string name)
        {
            return Context.Set<Area>().Where(o => o.nome.Contains(name)).ToList();
        }

        public IEnumerable<Area> ByPropriedadeRural(Guid IDPropriedade)
        {
            return Context.Set<Area>().Where(o => o.IDPropriedade.Equals(IDPropriedade)).ToList();
        }

        public IEnumerable<Area> ByProprietario(Guid IDProprietario)
        {
            return Context.Set<Area>().Where(o => o.IDPropriedade.Equals(IDProprietario)).ToList();
        }

        public AreaView GetFullArea(Guid objID)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT Propriedade.nome as nomePropriedade, *, dbo.GetGeoJSON(Area.area_geo) AS geoJson FROM Area ");
            query.AppendLine("JOIN Propriedade ON Propriedade.objID = Area.IDPropriedade ");
            query.AppendLine("WHERE Area.objID = '" + objID.ToString() + "'");
            return Context.Database.SqlQuery<AreaView>(query.ToString()).FirstOrDefault();
        }

        public IEnumerable<Area> GetByPropriedade(Guid IDPropriedade)
        {
            //  3e034926-0ef1-4287-a8e2-5209628753a3' ORDER BY nome
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT objID, IDPropriedade, nome, tamanho, codigo, " +
                                " tipoPredSolo, anoAbertura, " +
                                "altitudeMedia, area_geo  FROM Area a WHERE IDPropriedade = '" + IDPropriedade.ToString() + "' ORDER BY nome");

            return Context.Database.SqlQuery<Area>(query.ToString()).ToList();
            //return Context.Database.SqlQuery<Area>(query.ToString());
        }

        public IEnumerable<AreaPropriedadeView> GetAreaPropriedade(Guid Area)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select prop.IDProprietario as objArea, ar.nome as nomeArea, prop.objID as IDPropriedade, prop.nome as nomePropriedade, area_geo  from Area as ar " +
                                "INNER JOIN Propriedade prop on ar.IDPropriedade = prop.objID " +
                                "where ar.objID = '" + Area.ToString() + "'");
            return Context.Database.SqlQuery<AreaPropriedadeView>(query.ToString()).ToList();
        }

        public IEnumerable<Area> GetByPropriedadeSafra(Guid IDPropriedade, Guid IDSafra)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT DISTINCT ar.objID,ar.IDTipoArea,ar.IDPropriedade,ar.nome,ar.tamanho,ar.codigo,ar.tipoPredSolo,ar.anoAbertura,ar.altitudeMedia ");
            query.AppendLine("FROM  AreaServico ars ");
            query.AppendLine("INNER JOIN Area ar ON ar.objID = ars.IDArea ");
            query.AppendLine("WHERE ars.IDSafra = '" + IDSafra + "' and ar.IDPropriedade = '" + IDPropriedade + "' ");
            query.AppendLine("ORDER BY ar.nome ");
            return Context.Database.SqlQuery<Area>(query.ToString()).ToList();
        }



        public IEnumerable<Area> GetAllArea()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM Area ORDER BY nome");
            return Context.Database.SqlQuery<Area>(query.ToString());
        }

        public IEnumerable<AreaView> VerifyAreaServico(Guid iDPropriedade)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT case when (ars.codigo is not null) then 'SIM' else 'Não' END as contemgeo, a.objID, IDPropriedade, nome, a.tamanho, a.codigo,  tipoPredSolo, anoAbertura, altitudeMedia FROM Area a" +
                              " LEFT JOIN AreaServico ars on a.objID = ars.IDArea" +
                                " WHERE IDPropriedade = '" + iDPropriedade.ToString() + "' ORDER BY nome");
            return Context.Database.SqlQuery<AreaView>(query.ToString()).ToList();
        }

        public IEnumerable<BNGAreaView> GetAreaBNGByPropriedade(Guid IDSafra, Guid IDPropriedade)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT                                                                                            ");
            query.AppendLine("      a.objID                                                                                ,    ");
            query.AppendLine("      a.descricao AS nome                                                                         ");
            query.AppendLine("FROM BNG.dbo.PropriedadeRural p                                                                   ");

            query.AppendLine("                                                                                                  ");

            query.AppendLine("INNER JOIN  BNG.dbo.Area       a   ON a.IDPropriedadeRural = p.objID                              ");
            query.AppendLine("INNER JOIN  BNG.dbo.AreaSafra	asf  ON asf.IDArea           = a.objID                              ");

            query.AppendLine("WHERE asf.IDSafra = '" + IDSafra + "' AND p.objID = '" + IDPropriedade +"' ORDER BY a.descricao   ");

            return Context.Database.SqlQuery<BNGAreaView>(query.ToString()).ToList();
        }

        public IEnumerable<AreaView> FindFullAreaByPropriedade(Guid IDPropriedade)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT                                                                                            ");
            query.AppendLine("     objID                                                                                   ,    ");
            query.AppendLine("     IDPropriedade                                                                           ,    ");
            query.AppendLine("     dbo.GetGeoJSON(area_geo.MakeValid()) AS GeoJson                                         ,    ");
            query.AppendLine("     codigo                                                                                       "); 
            query.AppendLine("FROM Area                                                                                         ");
            query.AppendLine("WHERE IDPropriedade = '" +IDPropriedade+ "' AND area_geo IS NOT NULL                              ");
            return Context.Database.SqlQuery<AreaView>(query.ToString()).ToList();
        }

        public IEnumerable<AreaGrid> GetAllAreaExistedGrid(Guid IDSafra, Guid IDPropriedade)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("	A.objID			    ,");
            query.AppendLine("	A.IDPropriedade	    ,");
            query.AppendLine("	A.nome			    ,");
            query.AppendLine("	A.codigo		     ");
            query.AppendLine("FROM Area A            ");
            query.AppendLine("INNER JOIN AreaServico ASV ON ASV.IDArea = A.objID");
            query.AppendLine("INNER JOIN Grid G ON G.IDAreaServico = ASV.objID  ");
            query.AppendLine("WHERE ASV.IDSafra = '"+IDSafra+"' AND A.IDPropriedade = '"+IDPropriedade+"' AND G.geo IS NOT NULL ");
            query.AppendLine("GROUP BY A.objID, A.IDPropriedade, A.nome, A.codigo");
            return Context.Database.SqlQuery<AreaGrid>(query.ToString()).ToList();
        }
    }
}