using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class AreaServicoRepository : RepositoryBase<AreaServico>, IAreaServicoRepository
    {
        public AreaServicoViewer FindAreaServico(Guid objID)
        {
            return Context.Database.SqlQuery<AreaServicoViewer>("EXEC find_areaservico '"+ objID + "'").FirstOrDefault();
        }

        public bool UpdateGeo(Guid objID, string geoString, string jsonField, float tamanho)
        {
            decimal tm = decimal.Parse(tamanho.ToString().Replace(".", ",")); 
            return Context.Database.SqlQuery<bool>("EXEC update_geo_areaservico '" + objID + "', '" + geoString + "','" + jsonField +"', " + tm.ToString().Replace(",", ".")).FirstOrDefault();
        }


        public AreaServicoView FindFullAreaServico(Guid? objID, Guid? IDArea, Guid? IDSafra, bool? returngeo)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT                                                              ");
            query.AppendLine("      asv.*                                                       , ");
            query.AppendLine("      prt.objID                   as IDProprietario               , ");
            query.AppendLine("      prop.objID                  as IDPropriedade                , ");
            query.AppendLine("      sf.descricao                as safra                        , ");
            query.AppendLine("      prt.nome                    as proprietario                 , ");
            query.AppendLine("      prop.nome                   as propriedade                  , ");
            query.AppendLine("      ar.nome                     as area                         , ");
            query.AppendLine("      'POLY   ' +     ar.nome     as nome                         , ");
            query.AppendLine("      sv.nome                     as servico                      , ");
            query.AppendLine("      CONVERT(BIT,0)			    as semciclo	                    , ");
            query.AppendLine("      dbo.GetGeoJSON(asv.geo.MakeValid()) as geoJson                ");
            query.AppendLine("FROM AreaServico asv                                                ");

            query.AppendLine("  INNER JOIN Safra sf         ON sf.objID = asv.IDSafra             ");
            query.AppendLine("  INNER JOIN Servico sv       ON sv.objID = asv.IDServico           ");
            query.AppendLine("  INNER JOIN Area ar          ON ar.objID = asv.IDArea              ");
            query.AppendLine("  INNER JOIN Propriedade prop ON prop.objID = ar.IDPropriedade      ");
            query.AppendLine("  INNER JOIN Proprietario prt ON prt.objID = prop.IDProprietario    ");
            query.AppendLine("WHERE                                                               ");

            if (objID != null)
                query.AppendLine("  asv.objID = '" + objID + "'                                   ");
            else
                query.AppendLine("  asv.objID IS NOT NULL                                         ");

            if (IDArea != null)
                query.AppendLine(" AND asv.IDArea =  '" + IDArea + "'                             ");

            if (IDSafra != null)
                query.AppendLine(" AND asv.IDSafra = '" + IDSafra + "'                            ");

            if (returngeo == true)
                query.AppendLine(" AND asv.geo IS NOT NULL                                        ");

            return Context.Database.SqlQuery<AreaServicoView>(query.ToString()).FirstOrDefault();
        }


        public IEnumerable<AreaServicoView> GetAreaServico(Guid? IDAreaServico, Guid IDSafra, Guid? IDArea, Guid? IDPropriedade, Guid? IDServico, bool? returngeo, int returnquery)
        {
            StringBuilder query = new StringBuilder();
            if (returnquery == 0)
            {
                query.AppendLine("SELECT                                                            ");
                query.AppendLine("      AreaServico.objID                                       ,   ");
                query.AppendLine("      Area.objID as IDArea                                    ,   ");
                query.AppendLine("      AreaServico.IDSafra as IDSafra                          ,   ");
                query.AppendLine("      AreaServico.IDServico                                   ,   ");
                query.AppendLine("      AreaServico.codigo                                      ,   ");
                query.AppendLine("      'POLI '+Area.nome +' - ' + convert(varchar(1),AreaServico.numServico)   as nome                             ,   ");
                query.AppendLine("      Area.nome           as area                             ,   ");
                query.AppendLine("      Area.tamanho        as tamanhoArea                      ,   ");
                query.AppendLine("      AreaServico.tamanho as tamanho                          ,   ");
                query.AppendLine("      AreaServico.numServico                                  ,   ");
                query.AppendLine("      Servico.nome        as Servico                              ");

                if (returngeo == true)
                    query.AppendLine(",     dbo.GetGeoJSON(geo.MakeValid()) as geoJson              ");


                query.AppendLine("FROM AreaServico                                                  ");
                query.AppendLine("  INNER JOIN Area     ON Area.objID       = AreaServico.IDArea    ");
                query.AppendLine("  INNER JOIN Servico  ON Servico.objID    = AreaServico.IDServico ");
                query.AppendLine("WHERE                                                             ");

                // Condição para retornar a área serviço a partir da área e safra. 
                if (IDArea != null)
                    query.AppendLine("  Area.objID = '" + IDArea.ToString() + "'               AND ");

                // Condição para retornar a área serviço a partir da propriedade e safra. 
                if (IDPropriedade != null)
                    query.AppendLine(" Area.IDPropriedade = '" + IDPropriedade.ToString() + "' AND ");

                // Condição para retornar todos os registros de área serviço que contém geo. 
                if (returngeo == true)
                    query.AppendLine(" AreaServico.geo is not null  AND                            ");

                if (IDAreaServico != null)
                    query.AppendLine(" AreaServico.objID ='" + IDAreaServico + "' AND              "); 

                query.AppendLine(" AreaServico.IDSafra = '" + IDSafra.ToString() + "'              ");
                query.AppendLine(" ORDER BY numServico                                             ");

            }
            if (returnquery == 1)
            {
                /// Essa declaração será a tabela de retorno ("RESULTADO").
                query.AppendLine("DECLARE @Resultado TABLE(              ");
                query.AppendLine("  [objID] [UNIQUEIDENTIFIER]      NULL,");
                query.AppendLine("  [IDArea][UNIQUEIDENTIFIER]      NULL,");
                query.AppendLine("  [IDSafra][UNIQUEIDENTIFIER]     NULL,");
                query.AppendLine("  [IDServico][UNIQUEIDENTIFIER]   NULL,");
                query.AppendLine("  [nome][NVARCHAR](MAX)           NULL,");
                query.AppendLine("  [area][NVARCHAR](MAX)           NULL,");
                query.AppendLine("  [tamanhoArea][FLOAT]            NULL,");
                query.AppendLine("  [tamanho][FLOAT]                NULL,");
                query.AppendLine("  [numServico][INT]               NULL,");
                query.AppendLine("  [Servico][NVARCHAR](MAX)        NULL,");
                query.AppendLine("  [PossuiArea][NVARCHAR](MAX)     NULL,");
                query.AppendLine("  [PossuiZona][INT]               NULL,");
                query.AppendLine("  [PossuiParametroArea][INT]      NULL "); 
                query.AppendLine(")");

                #region Adição do GRID para verifiação.

                /// Table de resultado, onde o objetivo será em inserir os dados encontrados do GRID. 
                query.AppendLine("DECLARE @ResultGrid TABLE(                    ");
                query.AppendLine("  [IDAreaServico][UNIQUEIDENTIFIER]   NULL,   ");
                query.AppendLine("  [gridNull]     [INT]                NULL    ");
                query.AppendLine(")");

                query.AppendLine("INSERT INTO @ResultGrid (IDAreaServico, gridNull)");
                
                /// Esse select vai contar quantos GRIDS existe referente a propriedade e safra, se não encontrar nenhum o valor de retorno na tabela resultado será NULL. 
                query.AppendLine("SELECT                                                                ");
                query.AppendLine("      Grid.IDAreaServico                      ,                       ");
                query.AppendLine("      COUNT(Grid.IDAreaServico) as gridNULL                           ");
                query.AppendLine("FROM AreaServico                                                      ");
                query.AppendLine(" INNER JOIN Area      ON Area.objID           = AreaServico.IDArea    ");
                query.AppendLine(" INNER JOIN Servico   ON Servico.objID        = AreaServico.IDServico ");
                query.AppendLine(" INNER JOIN Grid      ON Grid.IDAreaServico   = AreaServico.objID     ");
                query.AppendLine("WHERE                                                                 ");
                query.AppendLine("      Area.IDPropriedade = '" + IDPropriedade + "'                    ");
                query.AppendLine("      AND AreaServico.IDSafra = '" + IDSafra + "'                     ");
                query.AppendLine("      AND Grid.geo IS NOT NULL                                        ");
                query.AppendLine("GROUP BY Grid.IDAreaServico                                           ");
                #endregion

                #region Adição do parametro área para verificação. 

                query.AppendLine("DECLARE @ParametroArea TABLE(                 "); 
                query.AppendLine("    [IDAreaServico][UNIQUEIDENTIFIER] NULL,   "); 
	            query.AppendLine("    [parametroNull][int]                      "); 
                query.AppendLine(")                                             "); 

                query.AppendLine("INSERT INTO @ParametroArea(IDAreaServico, parametroNull) "); 
                query.AppendLine("SELECT "); 
                query.AppendLine("    IDAreaServico, "); 
                
                query.AppendLine("    COUNT(ParametroArea.IDAreaServico) as parametroNull "); 
                query.AppendLine("FROM ParametroArea "); 
                
                query.AppendLine("INNER JOIN AreaServico ON AreaServico.objID = ParametroArea.IDAreaServico AND AreaServico.IDSafra = '"+IDSafra+"' "); 
                query.AppendLine("INNER JOIN Area ON Area.objID = AreaServico.IDArea  AND Area.IDPropriedade = '"+IDPropriedade+"' "); 
                query.AppendLine("GROUP BY ParametroArea.IDAreaServico ");
                #endregion

                                query.AppendLine("INSERT INTO @Resultado (objID, IDArea, IDSafra, IDServico, nome, area, tamanhoArea, tamanho, numServico, Servico, PossuiArea, PossuiZona, PossuiParametroArea) ");
                query.AppendLine("SELECT                                                                                                                                    ");
                query.AppendLine("      AreaServico.objID                                                                                                                 , ");
                query.AppendLine("      Area.objID                      as IDArea                                                                                         , ");
                query.AppendLine("      AreaServico.IDSafra             as IDSafra                                                                                        , ");
                query.AppendLine("      AreaServico.IDServico                                                                                                             , ");
                query.AppendLine("      'POLI ' + Area.nome +' - ' + convert(varchar(1),AreaServico.numServico)             as nome                                                                                           , ");
                query.AppendLine("      Area.nome                       as area                                                                                           , ");
                query.AppendLine("      Area.tamanho                    as tamanhoArea                                                                                    , ");
                query.AppendLine("      AreaServico.tamanho             as tamanho                                                                                        , ");
                query.AppendLine("      AreaServico.numServico          as numServico                                                                                     , ");
                query.AppendLine("      Servico.nome                    as Servico                                                                                        , ");
                query.AppendLine("      dbo.GetGeoJSON(geo.MakeValid()) as geoJson                                                                                        , ");
                query.AppendLine("      r.gridNull                      as gridNull                                                                                       , ");
                query.AppendLine("      p.parametroNull				    as parametroNull                                                                                    ");
                query.AppendLine("FROM AreaServico                                                                                                                          ");
                query.AppendLine("INNER JOIN Area            ON Area.objID = AreaServico.IDArea                                                                             ");
                query.AppendLine("INNER JOIN Servico         ON Servico.objID = AreaServico.IDServico                                                                       ");
                query.AppendLine("LEFT JOIN @ResultGrid r    ON r.IDAreaServico = AreaServico.objID                                                                         ");
                query.AppendLine("LEFT JOIN @ParametroArea p ON p.IDAreaServico = AreaServico.objID                                                                         "); 

                query.AppendLine("WHERE                                                                                                                                     ");
                query.AppendLine("      AreaServico.IDSafra ='" + IDSafra + "'           AND       ");

                if (IDArea != null)
                    query.AppendLine("      AreaServico.IDArea  ='" + IDArea + "'       AND       ");

                if (IDPropriedade != null)
                    query.AppendLine("      Area.IDPropriedade  ='" + IDPropriedade + "' AND       ");

                if (IDServico != null)
                    query.AppendLine("      Servico.objID  ='" + IDServico + "' AND       ");

                if (IDAreaServico != null)
                    query.AppendLine("      AreaServico.objID = '" + IDAreaServico + "' AND        "); 


                query.AppendLine("  AreaServico.objID IS NOT NULL                                  ");
                query.AppendLine("SELECT * FROM @Resultado ORDER BY area, numServico");
            }

            return Context.Database.SqlQuery<AreaServicoView>(query.ToString());
        }

        public Domain.ViewTables.ImportItensLabView ExistAnaliseByCodigo(int Codigo)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT                                                        ");
            query.AppendLine("CASE                                                    ");
            query.AppendLine("WHEN COUNT(objID) > 0 THEN objID ELSE '00000000-0000-0000-0000-000000000000' END AS IDAreaServico       ,");
            query.AppendLine("CASE                                                    ");
            query.AppendLine("          WHEN COUNT(codigo) > 0 THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END AS AreaServicoAnaliseExiste  ");
            query.AppendLine("FROM AreaServico WHERE codigo = " + Codigo + " GROUP BY objID");
            return (Context.Database.SqlQuery<Domain.ViewTables.ImportItensLabView>(query.ToString()).SingleOrDefault());
        }



        public IEnumerable<AreaServicoView> GetMaxServicoRegister(Guid iDSafra, Guid iDArea)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT numServico                                                               ");
            query.AppendLine("FROM AreaServico                                                                       ");
            query.AppendLine("LEFT JOIN Servico AS F ON AreaServico.IDServico = F.objID");
            query.AppendLine("WHERE IDArea = '" + iDArea + "' AND IDSafra = '" + iDSafra + "' order by numServico   ");

            return Context.Database.SqlQuery<AreaServicoView>(query.ToString()).ToList();
        }

        public bool DeleteAllAreaServico(String objID)
        {
            try
            {
                String query = "";
                query = ("DELETE FROM AreaServico WHERE objID = '" + objID + "'");
                Context.Database.ExecuteSqlCommand(query);
                return true;
            }
            catch (Exception)
            {
                return false; ;
                throw;
            }

        }


        public DbGeography GetGeoAreaOrGrid(string geo, Guid? objID, bool GridOrAreaServico)
        {
            DbGeography q;
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("DECLARE @g geography;                                     ");

                if (geo.Contains(", 4326"))
                    query.AppendLine("SET @g = geography::STPolyFromText(" + geo + ")");
                else
                    query.AppendLine("SET @g = geography::STPolyFromText('" + geo + "' , 4326)  ");
                query.AppendLine("SELECT @g                                                 ");
                q = Context.Database.SqlQuery<DbGeography>(query.ToString()).FirstOrDefault();
            }
            catch (Exception)
            {
                StringBuilder query = new StringBuilder();
                if (geo.Contains(", 4326"))
                {
                    geo = geo.Replace("'", "");
                    geo = geo.Replace(", 4326", "");
                }

                var i = geo.Split(',');
                i[0] = i[0].Replace("POLYGON((", "");
                i[(i.Length - 1)] = i[(i.Length - 1)].Replace("))", "");

                string ultimacoordenada = "";
                string newgeo = "";
                for (int x = (i.Length - 1); x >= 0; x--)
                {
                    if (x == (i.Length - 1))
                    {
                        newgeo = "POLYGON((" + i[x] + " , ";
                        ultimacoordenada = i[x] + "))";
                    }
                    if (x > 0 && x < (i.Length - 1))
                    {
                        newgeo += i[x] + " , ";
                    }
                    if (x == 0)
                        newgeo += i[x] + " , ";
                }
                newgeo += ultimacoordenada;


                query.AppendLine("SET @g = geography::STPolyFromText('" + geo + "' , 4326)  ");
                query.AppendLine("SELECT @g                                                 ");
                //q = Context.Database.SqlQuery<DbGeography>(query.ToString()).FirstOrDefault();

                String updateareaservico = "";

                // True = Area serviço, false = Grid.
                if (GridOrAreaServico)
                {
                    updateareaservico = "UPDATE AreaServico                              " +
                      "SET geo = geography::STPolyFromText('" + newgeo + "' , 4326) " +
                      "WHERE objID = '" + objID + "'                         ";

                    Context.Database.ExecuteSqlCommand(updateareaservico);
                    query.AppendLine("SELECT geo.MakeValid() FROM AreaServico WHERE objID = '" + objID + "'");
                }
                else
                {
                    updateareaservico = "UPDATE Grid                                                    " +
                                        "SET geo = geography::STPolyFromText('" + newgeo + "' , 4326)   " +
                                        "WHERE objID = '" + objID + "'                                  ";

                    Context.Database.ExecuteSqlCommand(updateareaservico);

                    query.AppendLine("SELECT geo.MakeValid() FROM Grid WHERE objID = '" + objID + "'");
                }

                q = Context.Database.SqlQuery<DbGeography>(query.ToString()).SingleOrDefault();
            }


            return q;
        }

        public AreaServicoView FindFilter(Guid? IDSafra, Guid? IDProprietario, Guid? IDPropriedade, Guid? IDArea)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("DECLARE @ID AS CHAR(36) = NEWID()"); 

            query.AppendLine("DECLARE @Safra TABLE(             "); 
            query.AppendLine("    ID UNIQUEIDENTIFIER,          "); 
            query.AppendLine("    IDSafra UNIQUEIDENTIFIER,     "); 
            query.AppendLine("    safra NVARCHAR(MAX)           "); 
            query.AppendLine(")                                 "); 

            query.AppendLine("INSERT INTO @Safra                "); 
            query.AppendLine("SELECT @ID AS ID, objID AS IDSafra, descricao AS safra FROM Safra WHERE objID = '"+IDSafra+"'    "); 

            query.AppendLine("DECLARE @Proprietario TABLE(          "); 
            query.AppendLine("    ID UNIQUEIDENTIFIER,              "); 
            query.AppendLine("    IDProprietario UNIQUEIDENTIFIER,  "); 
            query.AppendLine("    proprietario NVARCHAR(MAX)        "); 
            query.AppendLine(")                                     ");

            query.AppendLine("INSERT INTO @Proprietario");
            query.AppendLine("SELECT @ID AS ID , objID AS IDProprietario, nome AS proprietario FROM Proprietario WHERE objID = '"+IDProprietario+"'");

            query.AppendLine("DECLARE @Propriedade TABLE(           "); 
            query.AppendLine("    ID UNIQUEIDENTIFIER,              "); 
            query.AppendLine("    IDPropriedade UNIQUEIDENTIFIER,   "); 
            query.AppendLine("    propriedade NVARCHAR(MAX)        "); 
            query.AppendLine(")                                     "); 

            query.AppendLine("INSERT INTO @Propriedade              "); 
            query.AppendLine("SELECT @ID AS ID , objID AS IDPropriedade , nome AS propriedade FROM Propriedade WHERE objID = '"+IDPropriedade+"' "); 


            query.AppendLine("SELECT s.IDSafra, s.safra, p.IDProprietario, p.proprietario, pr.IDPropriedade, pr.propriedade FROM @Safra s"); 
            query.AppendLine("INNER JOIN @Proprietario p  ON p.ID = s.ID"); 
            query.AppendLine("INNER JOIN @Propriedade pr ON pr.ID = s.ID"); 


            return Context.Database.SqlQuery<AreaServicoView>(query.ToString()).FirstOrDefault();
        }


    }
}
