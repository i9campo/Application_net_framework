using SharpDX.Win32;
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
    public class GridRepository : RepositoryBase<Grid>, IGridRepository
    {
        #region C.R.U.D
        public bool AddLstGrid(GridViewer obj)
        {
            return Context.Database.SqlQuery<bool>("EXEC add_grid '" + obj.IDAreaServico + "','" + obj.descricao + "', " + obj.tamanho.ToString().Replace(',', '.') + "," + "'" + obj.geoString + "','"+ obj.jsonField +"','" + obj.centerLegend + "'").FirstOrDefault(); 
        }

        public bool UpdateGrid(GridViewer obj)
        {
            return Context.Database.SqlQuery<bool>("EXEC update_grid '" + obj.objID + "','" + obj.descricao + "','" + obj.tamanho.ToString().Replace(',', '.') + ",'" + obj.geoJson + "'").FirstOrDefault();
        }
        public bool DeleteGrid(String IDAreaServico)
        {
            return Context.Database.SqlQuery<bool>("EXEC delete_grid_cascade '" + IDAreaServico + "'").FirstOrDefault();
        }
        #endregion

        #region SEARCH 
        public GridViewer FindGrid(Guid objID)
        {
            return Context.Database.SqlQuery<GridViewer>("EXEC get_grid_by_objid '" + objID + "'").FirstOrDefault();
        }
        public Grid GetByCodigo(int Codigo)
        {
            return Context.Database.SqlQuery<Grid>("SELECT * FROM Grid WHERE codigo = '" + Codigo + "'").FirstOrDefault();
        }
        public IEnumerable<GridViewer> GetByAreaServico(Guid IDAreaServico)
        {
            return Context.Database.SqlQuery<GridViewer>("EXEC get_list_grid_by_areaservico '" + IDAreaServico + "'").ToList();
        }
        #endregion

        public IEnumerable<GridView> GetAllGeoJson()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT TOP(100) objID, IDAreaServico, descricao, tamanho, codigo, dbo.GetGeoJSON(geo) AS geo FROM dbo.Grid ");
            query.AppendLine(" ORDER BY descricao");

            return Context.Database.SqlQuery<GridView>(query.ToString());

        }

        public AnaliseSoloView GetMediaAnaliseSoloGrid(Guid objID, string profundidade)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM fGetMediaAnaliseSoloGrid('" + objID.ToString() + "', '" + profundidade + "')");

                return Context.Database.SqlQuery<AnaliseSoloView>(query.ToString()).SingleOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void RemoveGRID(Guid objID)
        {
            StringBuilder ans = new StringBuilder();
            ans.AppendLine("DELETE AnaliseSolo WHERE IDGrid = '" + objID + "'");
            Context.Database.SqlQuery<AnaliseSolo>(ans.ToString());

            StringBuilder cor = new StringBuilder();
            cor.AppendLine("DELETE Corretivo WHERE IDGrid = '" + objID + "'");
            Context.Database.SqlQuery<Corretivo>(cor.ToString());
        }

        public IEnumerable<GridView> CorrecaoAcidez(Guid IDAreaServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select*, grid.objId as IDgrid from Grid");
            query.AppendLine("INNER JOIN AnaliseSolo ON AnaliseSolo.IDAreaServico=Grid.IDAreaServico");
            query.AppendLine("where Grid.IDAreaServico = '" + IDAreaServico + "'");

            return Context.Database.SqlQuery<GridView>(query.ToString()).ToList();

        }

        public IEnumerable<GridView> GetByAreaServicoFull(Guid IDAreaServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(" DECLARE @MediaAnalise AS TMediaAnaliseSolo  ");
            query.AppendLine(" INSERT INTO @MediaAnalise ");
            query.AppendLine(" SELECT* FROM fbngGetMediaAnaliseSolo('" + IDAreaServico + "', null, '00 - 20', 1, 0, 0) ");
            query.AppendLine("                                                                                        ");
            query.AppendLine(" SELECT                                                                                 ");
            query.AppendLine("     GD.objID															                , ");
            query.AppendLine("     GD.IDAreaServico													                , ");
            query.AppendLine("     GD.descricao                                                                     , ");
            query.AppendLine("     ROUND(GD.tamanho, 2) as tamanho                                                  , ");
            query.AppendLine("     GD.codigo as CODIGO                                                              , ");
            query.AppendLine("     dbo.GetGeoJSON(GD.geo.MakeValid()) as geoJson                                    , ");
            query.AppendLine("     ROUND(GD.tamanho, 2) as AREA                                                     , ");
            query.AppendLine("     GD.codigo as CODIGO_ZM                                                           , ");
            query.AppendLine("     A.nome + ' - ' + CAST(GD.tamanho as varchar) + ' ha' as ZONA_AREA                , ");
            query.AppendLine("     GD.descricao as ZONA                                                             , ");
            query.AppendLine("     GD.descricao + ' - ' + CAST(GD.codigo as varchar) as ZM_CODIGO                   , ");

            #region Retorna a média das analises
            query.AppendLine("     MD.Agua                      AS mAgua         , ");
            query.AppendLine("     MD.Cacl                      AS mCacl         , ");
            query.AppendLine("     MD.MO                        AS mMO           , ");
            query.AppendLine("     MD.P                         AS mP            , ");
            query.AppendLine("     MD.PMehl                     AS mPMehl        , ");
            query.AppendLine("     MD.PRes                      AS mPRes         , ");
            query.AppendLine("     MD.K                         AS mK            , ");
            query.AppendLine("     MD.S                         AS mS            , ");
            query.AppendLine("     MD.SomaBases                 AS mSomaBases    , ");
            query.AppendLine("     MD.Ca                        AS mCa           , ");
            query.AppendLine("     MD.Mg                        AS mMg           , ");
            query.AppendLine("     MD.Al                        AS mAl           , ");
            query.AppendLine("     MD.HAl                       AS mHAl          , ");
            query.AppendLine("     MD.CTC                       AS mCTC          , ");
            query.AppendLine("     MD.V                         AS mV            , ");
            query.AppendLine("     MD.relCaMg                   AS mrelCaMg      , ");
            query.AppendLine("     MD.relCaK                    AS mrelCaK       , ");
            query.AppendLine("     MD.relMgK                    AS mrelMgK       , ");
            query.AppendLine("     MD.CTCCa                     AS mCTCCa        , ");
            query.AppendLine("	   MD.CTCMg                     AS mCTCMg        , ");
            query.AppendLine("     MD.CTCK                      AS mCTCK         , ");
            query.AppendLine("     MD.CTCAl                     AS mCTCAl        , ");
            query.AppendLine("     MD.Argila                    AS mArgila       , ");
            query.AppendLine("     MD.B                         AS mB            , ");
            query.AppendLine("     MD.Zn                        AS mZn           , ");
            query.AppendLine("     MD.Fe                        AS mFe           , ");
            query.AppendLine("     MD.Mn                        AS mMn           , ");
            query.AppendLine("     MD.Cu                        AS mCu           , ");
            query.AppendLine("     MD.Co                        AS mCo           , ");
            query.AppendLine("     MD.momicro                   AS mMomicro      , ");
            #endregion

            #region Área de calculos da média da analise. 

            #region Inicio.
            query.AppendLine("dbo.cGraficoAnaliseMacroP(MD.PMehl, MD.CTC)	 AS cnPMehl     , ");
            query.AppendLine("dbo.cGraficoAnaliseSBNiveis(MD.CTC)            AS cnSomaBases , ");
            query.AppendLine("dbo.cGraficoAnalisePhAgua(MD.Agua)             AS cnAgua      , ");
            query.AppendLine("dbo.cGraficoAnaliseMO(MD.MO)                   AS cnMO        , ");
            query.AppendLine("dbo.cGraficoAnaliseCTC(MD.CTC)                 AS cnCTC       , ");
            query.AppendLine("dbo.cGraficoAnaliseV(MD.V, MD.CTC)             AS cnV         , ");
            query.AppendLine("dbo.cGraficoAnaliseArgila(MD.Argila)           AS cnArgila    , ");
            #endregion

            #region Macronutrientes. 
            query.AppendLine("dbo.cGraficoAnaliseMacroCa(MD.Ca, MD.CTC)          AS cnCa    , ");
            query.AppendLine("dbo.cGraficoAnaliseMacroMg(MD.Mg, MD.CTC)          AS cnMg    , ");
            query.AppendLine("dbo.cGraficoAnaliseMacroK(MD.K, MD.CTC)            AS cnK     , ");
            query.AppendLine("dbo.cGraficoAnaliseMacroP(MD.P, MD.CTC)            AS cnP     , ");
            query.AppendLine("dbo.cGraficoAnaliseMacroPRes(MD.PRes, MD.Argila)   AS cnPRes  , ");
            query.AppendLine("dbo.cGraficoAnaliseMacroS(MD.S)                    AS cnS     , ");
            #endregion

            #region MicroNutrientes.
            query.AppendLine("dbo.cGraficoAnaliseMicroB(MD.B)                    AS cnB         , ");
            query.AppendLine("dbo.cGraficoAnaliseMicroZn(MD.Zn)                  AS cnZn        , ");
            query.AppendLine("dbo.cGraficoAnaliseMicroFe(MD.Fe)                  AS cnFe        , ");
            query.AppendLine("dbo.cGraficoAnaliseMicroMn(MD.Mn)                  AS cnMn        , ");
            query.AppendLine("dbo.cGraficoAnaliseMicroCu(MD.Cu)                  AS cnCu        , ");
            query.AppendLine("dbo.cGraficoAnaliseMicroMo(MD.momicro)             AS cnMomicro   , ");
            #endregion

            #region Participação na CTC. 
            query.AppendLine("dbo.cGraficoAnalisePerCa(MD.CTCCa, MD.CTC)         AS cnCTCCa     , ");
            query.AppendLine("dbo.cGraficoAnalisePerMg(MD.CTCMg, MD.CTC)         AS cnCTCMg     , ");
            query.AppendLine("dbo.cGraficoAnalisePerK(MD.CTCK, MD.CTC)           AS cnCTCK      , ");
            query.AppendLine("dbo.cGraficoAnalisePerAl(MD.CTCAl, MD.CTC)         AS cnCTCAl     , ");
            #endregion

            #region Relações
            query.AppendLine("dbo.cGraficoAnaliseRelCaMG(MD.relCaMg, MD.CTC)     AS cnRelCaMg ,");
            query.AppendLine("dbo.cGraficoAnaliseRelCaK(MD.relCaK, MD.CTC)       AS cnRelCaK  ,");
            query.AppendLine("dbo.cGraficoAnaliseRelMgK(MD.relMgK, MD.CTC)       AS cnRelMgK   ");
            #endregion

            #endregion

            query.AppendLine(" FROM Grid GD                                                                           ");
            query.AppendLine(" INNER JOIN AreaServico ON AreaServico.objID = GD.IDAreaServico                         ");
            query.AppendLine(" INNER JOIN Area A ON A.objID = AreaServico.IDArea                                      ");
            query.AppendLine(" INNER JOIN @MediaAnalise MD ON MD.IDGrid = GD.objID                                    ");
            query.AppendLine(" WHERE(GD.IDAreaServico = '" + IDAreaServico + "') AND(GD.geo IS NOT NULL)              ");
            query.AppendLine(" ORDER BY descricao                                                                     ");

            return Context.Database.SqlQuery<GridView>(query.ToString()).ToList();
        }
        public Domain.ViewTables.ImportItensLabView ExistAnaliseByCodigo(int codigo, Guid idareaservico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("select                                                        ");
            query.AppendLine("      case                                                    ");
            query.AppendLine("          when count(codigo) > 0 then convert(bit, 1) else convert(bit, 0) end as gridanaliseexiste  ");
            query.AppendLine("from grid where codigo = " + codigo + " and idareaservico = '" + idareaservico + "'");
            return Context.Database.SqlQuery<Domain.ViewTables.ImportItensLabView>(query.ToString()).SingleOrDefault();
        }
        public IEnumerable<GridView> GetGrid(Guid objID, Guid IDSafra, Guid? IDAreaServico, int Type)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT                                                ");
            query.AppendLine("  DISTINCT(Grid.IDAreaServico)                      , ");
            query.AppendLine("  ('ZM - ' + Area.nome +' - ' + convert(varchar(1),AreaServico.numServico)) AS descricao                      ");
            query.AppendLine("FROM Grid                                             ");

            query.AppendLine("INNER JOIN AreaServico ON AreaServico.objID = Grid.IDAreaServico AND AreaServico.IDSafra = '" + IDSafra + "'  ");
            query.AppendLine("INNER JOIN Area		 ON Area.objID		  = AreaServico.IDArea                                              ");
            query.AppendLine("INNER JOIN Propriedade ON Propriedade.objID = Area.IDPropriedade                                              ");
            query.AppendLine("WHERE                                                                                                         ");

            /// O retorno aqui será da propriedade
            if (Type == 0)
                query.AppendLine("      Propriedade.objID   = '" + objID + "' AND                                                           ");


            if (Type == 1 || Type == 2)
                query.AppendLine("      AreaServico.IDArea  = '" + objID + "' AND                                                           ");

            if (Type == 2 )
                query.AppendLine("      AreaServico.objID  = '" + IDAreaServico + "' AND                                                    ");

            query.AppendLine("      Grid.geo IS NOT NULL                                                                                    ");
            return Context.Database.SqlQuery<GridView>(query.ToString());
        }
        public GridView GetByGeoAreaServico(Guid IDAreaServico, string geo, string servico)
        {
            GridView o = new GridView();

            string[] coords = geo.Split(']', '[');
            StringBuilder query = new StringBuilder();
            if (servico.Equals("BNG") || servico.Equals("CTV"))
            {
                try
                {
                    o = Context.Database.SqlQuery<GridView>("EXEC get_intersect_grid_by_areaservico '" + geo + "', '" + IDAreaServico + "'").FirstOrDefault();
                }
                catch (Exception ex)
                {
                }
            }
            if (servico.Equals("BN"))
            {
                string[] coordsinvert = coords[1].Split(',');
                query.AppendLine("DECLARE @GridGeo TABLE(                  ");
                query.AppendLine("    [objID]         [CHAR](36)   , ");
                query.AppendLine("  [IDArea] [uniqueidentifier] NOT NULL,");
                query.AppendLine("  [IDSafra] [uniqueidentifier] NOT NULL,");
                query.AppendLine("  [IDServico] [uniqueidentifier] NOT NULL,");
                query.AppendLine("  [IDCultura] [uniqueidentifier] NULL,");
                query.AppendLine("  [IDProprietarioFatura] [uniqueidentifier] NULL,");
                query.AppendLine("  [numServico] [int] NULL,");
                query.AppendLine("  [tamanho] [float] NOT NULL,");
                query.AppendLine("  [parametroTecnico] [text] NULL,");
                query.AppendLine("  [parametroInterno] [text] NULL,");
                query.AppendLine("  [resumoOperacional] [text] NULL,");
                query.AppendLine("  [revisado] [bit] NULL,");
                query.AppendLine("  [dataRevisao] [datetime] NULL,");
                query.AppendLine(" 	[geo] [geography] NULL,");
                query.AppendLine("  [contrato] [int] NULL,");
                query.AppendLine("  [intersection] [int] NULL)");

                query.AppendLine(" DECLARE @h GEOGRAPHY                              ");
                query.AppendLine(" SET @h = GEOGRAPHY::Point(" + coordsinvert[0] + "," + coordsinvert[1] + ", 4326)");

                query.AppendLine(" INSERT INTO @GridGeo(objID, IDArea, IDSafra, IDServico, IDCultura, IDProprietarioFatura, numServico, tamanho, parametroTecnico, parametroInterno, resumoOperacional, revisado, dataRevisao, geo, contrato, intersection) ");
                query.AppendLine("SELECT objID, IDArea, IDSafra, IDServico, IDCultura, IDProprietarioFatura, numServico, tamanho, parametroTecnico, parametroInterno, resumoOperacional, revisado, dataRevisao,geo, contrato, geo.STIntersects(@h) as intersection  FROM AreaServico WHERE objID = '" + IDAreaServico + "'");

                query.AppendLine("SELECT objID AS ID, IDArea, IDSafra, IDServico, IDCultura, IDProprietarioFatura, numServico, tamanho, parametroTecnico, parametroInterno, resumoOperacional, revisado, dataRevisao, geo, contrato FROM @GridGeo WHERE intersection = 1");


                o = Context.Database.SqlQuery<GridView>(query.ToString()).FirstOrDefault();
            }

            if (o == null)
                o = new GridView();


            return o;
        }
        public IEnumerable<Grid> GetGridByAreaServico(Guid IDAreaServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM Grid WHERE IDAreaServico = '" + IDAreaServico + "'"); 
            return Context.Database.SqlQuery<Grid>(query.ToString());
        }

        public IEnumerable<Grid> GetAllGridByAreaServico(Guid IDAreaServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM Grid WHERE IDAreaServico = '" + IDAreaServico + "'");
            return Context.Database.SqlQuery<Grid>(query.ToString()); 
        }

        public bool UpdateFieldList(Guid objID, string newValue)
        {
            try
            {
                string query = "UPDATE G SET G.jsonField = '" + newValue + " FROM Grid G WHERE G.objID = '" + objID  + "'"; 
                Context.Database.ExecuteSqlCommand(query); 
                return true; 
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public GeoJsonSplitPoly ObjSplitPoly(string geo)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @geo GEOMETRY = " + geo + "");
            query.AppendLine("SELECT  dbo.GetGeoJSONGMT(@Geo) AS geoJson, @Geo.ToString() AS geoString");
            return Context.Database.SqlQuery<GeoJsonSplitPoly>(query.ToString()).FirstOrDefault();
        }
    }
}
