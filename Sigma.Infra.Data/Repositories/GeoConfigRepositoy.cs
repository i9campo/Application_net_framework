using NetTopologySuite.Geometries;
using SharpDX.Direct3D9;
using SharpDX;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using NetTopologySuite.Operation.Distance;
using Newtonsoft.Json.Linq;
using System.Web.UI.WebControls;

namespace Sigma.Infra.Data.Repositories
{
    public class GeoConfigRepositoy : RepositoryBase<GeoView>, IGeoConfigRepository
    {
        public string GetCreateGeoJson(Guid IDCiclo, string GeoString)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("DECLARE @ciclo GEOGRAPHY;");
            query.AppendLine("DECLARE @area GEOGRAPHY;");
            query.AppendLine("SELECT @ciclo = geo FROM CicloProducao WHERE objID = '" + IDCiclo + "'");
            query.AppendLine("SET @ciclo = @ciclo.STBuffer(1)");
            query.AppendLine("SET @area = GEOGRAPHY::STPolyFromText('" + GeoString + "' , 4326 ) ");
            query.AppendLine("SELECT dbo.GetGeoJSON(@area.STDifference(@ciclo))");
            return Context.Database.SqlQuery<String>(query.ToString()).FirstOrDefault();
        }
        public string GetCreateGeoString(Guid IDCiclo, string GeoString)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("DECLARE @ciclo GEOGRAPHY;");
            query.AppendLine("DECLARE @area GEOGRAPHY;");
            query.AppendLine("SELECT @ciclo = geo FROM CicloProducao WHERE objID = '" + IDCiclo + "'");
            query.AppendLine("SET @ciclo = @ciclo.STBuffer(1)");
            query.AppendLine("SET @area = GEOGRAPHY::STPolyFromText('" + GeoString + "' , 4326 ) ");
            query.AppendLine("SELECT @area.STDifference(@ciclo).ToString()");
            return Context.Database.SqlQuery<String>(query.ToString()).FirstOrDefault();
        }
        public string GetGeoAreaByIDAreaServico(Guid IDAreaServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT geo.ToString() FROM AreaServico WHERE objID = '" + IDAreaServico + "'");
            return Context.Database.SqlQuery<String>(query.ToString()).FirstOrDefault();
        }
        public string GetGeoJson(string GeoJson, bool? MultLine)
        {

            if (MultLine != null)
                if (MultLine == true)
                    GeoJson = GeoJson.Replace("POLYGON((", "LINESTRING(").Replace("))", ")");


            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @g GEOMETRY ");

            if (GeoJson.Contains("POLYGON(( , "))
                GeoJson = GeoJson.Replace("POLYGON(( , ", "POLYGON((").Replace("))  , ", "))");


            if (GeoJson.Contains("POINT"))
                query.AppendLine("SET @g = GEOMETRY::STPointFromText('" + GeoJson + "', 4326)");
            else if (GeoJson.Contains("MULTIPOLYGON "))
            {
                string json_geo = GeoJson.Replace("MULTIPOLYGON (((", "LINESTRING(");
                json_geo = json_geo.Replace(")), ((", ", ");
                json_geo = json_geo.Replace(")))", ")");
                GeoJson = json_geo;
                query.AppendLine("SET @g = GEOMETRY::STLineFromText('" + GeoJson + "', 4326)");

            }
            else if (GeoJson.Contains("POLYGON"))
            {
                if (GeoJson.Contains(")  ,"))
                    GeoJson = GeoJson.Replace(")  ,", ")");

                query.AppendLine("SET @g = GEOMETRY::STPolyFromText('" + GeoJson + "', 4326)");
            }
            else if (GeoJson.Contains("LINESTRING"))
            {
                if (GeoJson.Contains(")  ,"))
                    GeoJson = GeoJson.Replace(")  ,", ")");

                query.AppendLine("SET @g = GEOMETRY::STLineFromText('" + GeoJson + "', 4326)");
            }

            query.AppendLine("SELECT dbo.GetGeoJSONGMT(@g.MakeValid())");
            return Context.Database.SqlQuery<string>(query.ToString()).FirstOrDefault();
        }
        public string GetGeoJsonByIDAreaServico(Guid IDAreaServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT dbo.GetGeoJSON(geo) FROM AreaServico WHERE objID = '" + IDAreaServico + "'");
            return Context.Database.SqlQuery<String>(query.ToString()).FirstOrDefault();
        }
        public DbGeography GetGeoPoint(string geoJson)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("  DECLARE @g geography;                                           ");
            query.AppendLine("  SET @g = geography::STPointFromText('" + geoJson + "', 4326)    ");
            query.AppendLine("  SELECT @g                                                       ");

            return Context.Database.SqlQuery<DbGeography>(query.ToString()).FirstOrDefault();
        }
        public DbGeography GetGeoPolygon(string geo)
        {
            // Modelo.
            //SET @g = geography::STPolyFromText('POLYGON((-47.88186 -19.45547 , -47.88181 -19.45535 , -47.88162 -19.45541 , -47.88155 -19.45543 , -47.88134 -19.45565 , -47.88117 -19.45586 , -47.88097 -19.45608 , -47.88082 -19.45624 , -47.88063 -19.45645 , -47.88043 -19.45669 , -47.88027 -19.45692 , -47.88013 -19.45713 , -47.8801 -19.45716 , -47.88008 -19.45716 , -47.87999 -19.45733 , -47.87981 -19.45775 , -47.8799 -19.45781 , -47.88052 -19.45823 , -47.88139 -19.45674 , -47.88229 -19.45616 , -47.88186 -19.45547 , -47.88186 -19.45547 )) ', 4326)

            geo = geo.Replace("'", "");
            geo = geo.Replace(", 4326", "");
            DbGeography q;

            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("  DECLARE @g geography;                                   ");
                query.AppendLine("  SET @g = geography::STPolyFromText('" + geo + "', 4326) ");
                query.AppendLine("  IF(@g.MakeValid().STArea() > 400000000000)              ");
                query.AppendLine("      BEGIN                                               ");
                query.AppendLine("          SELECT @g.MakeValid().ReorientObject();         ");
                query.AppendLine("      END                                                 ");
                query.AppendLine("  ELSE                                                    ");
                query.AppendLine("      BEGIN                                               ");
                query.AppendLine("          SELECT @g.MakeValid();                          ");
                query.AppendLine("      END                                                 ");

                q = Context.Database.SqlQuery<DbGeography>(query.ToString()).FirstOrDefault();

            }
            catch (Exception ex)
            {
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
                geo = newgeo;

                StringBuilder query = new StringBuilder();
                query.AppendLine("  DECLARE @g geography; ");
                query.AppendLine("  SET @g = geography::STPolyFromText('" + geo + "', 4326) ");
                query.AppendLine("  IF(@g.MakeValid().STArea() > 400000000000)              ");
                query.AppendLine("      BEGIN                                               ");
                query.AppendLine("          SELECT @g.MakeValid().ReorientObject();         ");
                query.AppendLine("      END                                                 ");
                query.AppendLine("  ELSE                                                    ");
                query.AppendLine("      BEGIN                                               ");
                query.AppendLine("          SELECT @g.MakeValid();                          ");
                query.AppendLine("      END                                                 ");

                q = Context.Database.SqlQuery<DbGeography>(query.ToString()).FirstOrDefault();

            }

            return q;

        }
        public double GetSize(string coord)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("DECLARE @geo GEOGRAPHY = GEOGRAPHY::STGeomFromText('" + coord + "', 4326).MakeValid()     ");
                query.AppendLine("DECLARE @Tamanho FLOAT                                                                    ");
                query.AppendLine("SELECT @Tamanho = @geo.STArea()                                                           ");
                query.AppendLine("IF(@Tamanho > 10000000)                                                                   ");
                query.AppendLine("	SET @geo = @geo.ReorientObject();                                                       ");
                query.AppendLine("                                                                                          ");
                query.AppendLine("SELECT ROUND((@geo.STArea() / 10000),2)                                                   ");
                return Context.Database.SqlQuery<Double>(query.ToString()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                /// Quando ocorrer algum error, o valor que será retornado será zero. 
                return 0;
            }
        }
        public IEnumerable<GeoDate> GetSplitMultGeoCiclo(Guid IDAreaServico, int? Index, int ciclo)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @MultiPolygon NVARCHAR(MAX)   ");
            query.AppendLine("SET @MultiPolygon = ( SELECT DISTINCT stuff((SELECT ',' +  geo.ToString() FROM CicloProducao WHERE IDAreaServico = '" + IDAreaServico + "' AND tipo = " + (ciclo == 1 ? "'CI'" : "'CP'") + " FOR XML PATH('')),1,1, '') FROM CicloProducao WHERE IDAreaServico = '" + IDAreaServico + "'  AND tipo  = " + (ciclo == 1 ? "'CI'" : "'CP'") + ")");
            query.AppendLine("SET @MultiPolygon = ( SELECT REPLACE(@MultiPolygon,'POLYGON',''))");
            query.AppendLine("SET @MultiPolygon = ( SELECT CONCAT( 'MULTIPOLYGON(', @MultiPolygon, ')' ) )");

            query.AppendLine("DECLARE @Ciclo  GEOGRAPHY = @MultiPolygon");
            query.AppendLine("SET @Ciclo = @ciclo.STBuffer(0.01)");

            query.AppendLine("DECLARE @Area GEOGRAPHY;");
            query.AppendLine("SELECT @Area = geo FROM AreaServico WHERE objID = '" + IDAreaServico + "'");

            query.AppendLine("DECLARE @Resultado GEOGRAPHY;");
            query.AppendLine("SELECT @Resultado = @ciclo.STSymDifference(@Area)");



            query.AppendLine("SELECT * FROM SplitMultGeoCiclo(@Resultado, " + (Index.ToString() == "0" ? "NULL" : Index.ToString()) + ") ");


            return Context.Database.SqlQuery<GeoDate>(query.ToString()).ToList();
        }
        public IEnumerable<string> SelectedUnionLines(string Polygon, string MultLine, Guid? IDSelected)
        {
            StringBuilder query = new StringBuilder();

            /// Essa tabela será utilizada para armazenar os dados das linhas. 
            query.AppendLine("DECLARE @tab TABLE(             ");
            query.AppendLine("  [ID][INT]                   , ");
            query.AppendLine("  [GEO][GEOGRAPHY]            , ");
            query.AppendLine("  [StartLine][VARCHAR] (MAX)  , ");
            query.AppendLine("  [EndLine][VARCHAR] (MAX)      ");
            query.AppendLine(")                               ");

            query.AppendLine("                                ");

            query.AppendLine("DECLARE @LineColection TABLE( GEO GEOGRAPHY )                             ");
            query.AppendLine("DECLARE @Polygon GEOGRAPHY = GEOGRAPHY::STPolyFromText(" + Polygon + ")   ");
            query.AppendLine("DECLARE @Line    GEOGRAPHY = " + MultLine + "                              ");

            query.AppendLine("                                                                          ");

            /// Essa condição tem como objetivo fazer uma verificação no tamanho do polygon, 
            /// caso seja maior que o valor determinado, as coordenadas do polygon será reorientada. 
            query.AppendLine("IF (@Polygon.STArea() > 4500.00000000000)                                 ");
            query.AppendLine("BEGIN                                                                     ");
            query.AppendLine("  SET @Polygon = @Polygon.ReorientObject()                                ");
            query.AppendLine("END                                                                       ");

            query.AppendLine("                                                                          ");

            query.AppendLine("DECLARE @index INT = 1                                                    ");
            query.AppendLine("WHILE(@index <= @Line.STNumGeometries())                                  ");
            query.AppendLine("BEGIN                                                                     ");
            query.AppendLine("      INSERT INTO @LineColection VALUES(GEOGRAPHY::STLineFromText(@Line.STGeometryN(@index).ToString(),4326))  ");
            query.AppendLine("      SET @index = @index + 1                                             ");
            query.AppendLine("END                                                                       ");

            query.AppendLine("                                                                          ");

            query.AppendLine("DECLARE @LineIntersection GEOGRAPHY                                       ");
            query.AppendLine("DECLARE @CheckedListLineString Varchar(MAX)                               ");
            query.AppendLine("SELECT @CheckedListLineString = COALESCE(@CheckedListLineString +         ");
            query.AppendLine("  CASE                                                                    ");
            query.AppendLine("      WHEN Geo.STIntersection(@Polygon).ToString() LIKE '%LINESTRING%' THEN REPLACE(Geo.STIntersection(@Polygon).ToString(), 'LINESTRING', '')  ");
            query.AppendLine("      ELSE  '' END, REPLACE(Geo.STIntersection(@Polygon).ToString(), 'LINESTRING', '')                                                          ");
            query.AppendLine(") From @LineColection WHERE  Geo.STIntersection(@Polygon).ToString() LIKE '%LINESTRING%'                                                        ");
            query.AppendLine("IF(@CheckedListLineString IS NOT NULL AND @CheckedListLineString NOT LIKE 'MULTI ((%')                                                          ");

            query.AppendLine("                                                                                  ");

            query.AppendLine("BEGIN                                                                             ");
            query.AppendLine("  SET @CheckedListLineString = REPLACE(@CheckedListLineString, ') (', '),(')      ");
            query.AppendLine("  SET @CheckedListLineString = 'MULTILINESTRING(' + @CheckedListLineString + ')'  ");
            query.AppendLine("  SET @LineIntersection = @CheckedListLineString                                  ");
            query.AppendLine("END                                                                               ");
            query.AppendLine("ELSE                                                                              ");
            query.AppendLine("BEGIN                                                                             ");
            query.AppendLine("  SELECT @LineIntersection = GEOGRAPHY::UnionAggregate(Geo.STIntersection(@Polygon))  FROM @LineColection");
            query.AppendLine("END                                                                               ");
            query.AppendLine("SET @index = 1;                                                                   ");
            query.AppendLine("WHILE(@index <= @LineIntersection.STNumGeometries())                              ");
            query.AppendLine("BEGIN                                                                             ");
            query.AppendLine("  INSERT INTO @tab VALUES(@index, @LineIntersection.STGeometryN(@index), @LineIntersection.STGeometryN(@index).STStartPoint().ToString(), @LineIntersection.STGeometryN(@index).STEndPoint().ToString());");
            query.AppendLine("  SET @index = @index + 1;                                                        ");
            query.AppendLine("END                                                                               ");
            query.AppendLine("DECLARE @ConvertPolyToLine NVARCHAR(MAX) = REPLACE(REPLACE(@Polygon.ToString(), 'POLYGON ((', 'LINESTRING ('), '))', ')')");
            query.AppendLine("DECLARE @PolyToLine GEOGRAPHY = GEOGRAPHY::STLineFromText(@ConvertPolyToLine , 4326 )");

            query.AppendLine("");

            query.AppendLine("DECLARE @Point1 NVARCHAR(MAX)");
            query.AppendLine("DECLARE @Point2 NVARCHAR(MAX)");

            query.AppendLine("");

            query.AppendLine("DECLARE @StartLineID1 INT = (SELECT @PolyToLine.STIntersects(GEOGRAPHY::STGeomFromText(StartLine,4326).STBuffer(0.01)) FROM @tab WHERE ID = 1)");
            query.AppendLine("DECLARE @EndLineID1   INT = (SELECT @PolyToLine.STIntersects(GEOGRAPHY::STGeomFromText(EndLine,4326).STBuffer(0.01)) FROM @tab WHERE ID = 1)");

            query.AppendLine("");

            query.AppendLine("DECLARE @StartLineID2 INT = (SELECT @PolyToLine.STIntersects(GEOGRAPHY::STGeomFromText(StartLine,4326).STBuffer(0.01)) FROM @tab WHERE ID = 2)");
            query.AppendLine("DECLARE @EndLineID2   INT = (SELECT @PolyToLine.STIntersects(GEOGRAPHY::STGeomFromText(EndLine,4326).STBuffer(0.01)) FROM @tab WHERE ID = 2)");

            query.AppendLine("");

            query.AppendLine("IF (@StartLineID1 = 0)                                    ");
            query.AppendLine("BEGIN                                                     ");
            query.AppendLine("	SET @Point1 = (SELECT StartLine FROM @tab WHERE ID = 1) ");
            query.AppendLine("END");

            query.AppendLine("IF (@EndLineID1 = 0)");
            query.AppendLine("BEGIN ");
            query.AppendLine("	SET @Point1 = (SELECT EndLine FROM @tab WHERE ID = 1)");
            query.AppendLine("END");

            query.AppendLine("IF (@StartLineID2 = 0)");
            query.AppendLine("BEGIN                                                                             ");
            query.AppendLine("	SET @Point2 = (SELECT StartLine FROM @tab WHERE ID = 2)                         ");
            query.AppendLine("END                                                                               ");

            query.AppendLine("IF (@EndLineID2 = 0)                                                              ");
            query.AppendLine("BEGIN                                                                             ");
            query.AppendLine("	SET @Point2 = (SELECT EndLine FROM @tab WHERE ID = 2)                           ");
            query.AppendLine("END                                                                               ");

            query.AppendLine("DECLARE @PointToLine TABLE(ID INT,  seg GEOGRAPHY);                               ");
            query.AppendLine("INSERT INTO @PointToLine VALUES(1, GEOGRAPHY::STGeomFromText(@Point1  , 4326))    ");
            query.AppendLine("INSERT INTO @PointToLine VALUES(2, GEOGRAPHY::STGeomFromText(@Point2  , 4326))    ");
            query.AppendLine("DECLARE @unionPoint GEOGRAPHY                                                     ");
            query.AppendLine("SELECT @unionPoint = (SELECT seg FROM @PointToLine WHERE ID = 1);                 ");
            query.AppendLine("SELECT @unionPoint = @unionPoint.STUnion(seg) FROM @PointToLine WHERE ID = 2      ");
            query.AppendLine("SET @unionPoint = GEOGRAPHY::STGeomFromText(REPLACE(REPLACE(dbo.GetGeoJSON(@unionPoint), '{', ''), '}', '') + '', 4326)");
            query.AppendLine("DECLARE @NewLineColection TABLE( GEO GEOGRAPHY )                                  ");
            query.AppendLine("INSERT INTO @NewLineColection                                                     ");
            query.AppendLine("SELECT GEO FROM  @LineColection                                                   ");
            query.AppendLine("Declare @val Varchar(MAX) = 'MULTILINESTRING( '                                   ");
            query.AppendLine("SELECT @val = COALESCE(@val + '(' + REPLACE(GEO.ToString(), 'LINESTRING (', '(') + ',',',' )");
            query.AppendLine("        From @NewLineColection ");
            query.AppendLine("SET @val = REPLACE(REPLACE(REPLACE(@val, '(),','') + ')', '((', '('), '),)', '))')");
            query.AppendLine("DECLARE @UnionLines GEOGRAPHY = @val");
            query.AppendLine("SET @UnionLines = dbo.UnionLine(@val, @unionPoint.ToString())");
            query.AppendLine("SET @index = 1                                    ");
            query.AppendLine("DECLARE @Return TABLE( GEO GEOGRAPHY  )           ");
            query.AppendLine("WHILE (@index <= @UnionLines.STNumGeometries())   ");
            query.AppendLine("BEGIN                                             ");
            query.AppendLine("DECLARE @ConvertLineToPoly NVARCHAR(MAX)          ");
            query.AppendLine("BEGIN TRY   ");
            query.AppendLine("SET @ConvertLineToPoly = @UnionLines.STGeometryN(@index).ToString();     ");
            query.AppendLine("SET @ConvertLineToPoly = REPLACE(@ConvertLineToPoly, 'LINESTRING (', 'POLYGON((')");
            query.AppendLine("SET @ConvertLineToPoly = REPLACE(@ConvertLineToPoly, ')' , '))')  ");
            query.AppendLine("DECLARE @CheckedPolygon GEOGRAPHY; ");
            query.AppendLine("SET @CheckedPolygon = GEOGRAPHY::STGeomFromText(@ConvertLineToPoly, 4326);");
            query.AppendLine("IF(@CheckedPolygon.STIsValid() = 1)     ");
            query.AppendLine("    BEGIN ");
            query.AppendLine(" IF(GEOGRAPHY::STPolyFromText(@ConvertLineToPoly, 4326).STArea() >= 1) ");
            query.AppendLine("     BEGIN");
            query.AppendLine("INSERT INTO @Return VALUES(GEOGRAPHY::STPolyFromText(@ConvertLineToPoly, 4326)) ");
            query.AppendLine("    END ");
            query.AppendLine(" END   ");
            query.AppendLine("ELSE");
            query.AppendLine("BEGIN");
            query.AppendLine("INSERT INTO @Return VALUES(GEOGRAPHY::STLineFromText(@UnionLines.STGeometryN(@index).ToString(), 4326)) ");
            query.AppendLine("END ");
            query.AppendLine("  END TRY ");
            query.AppendLine("  BEGIN CATCH ");
            query.AppendLine(" INSERT INTO @Return VALUES(GEOGRAPHY::STLineFromText(@UnionLines.STGeometryN(@index).ToString(), 4326)) ");
            query.AppendLine(" END CATCH;    ");
            query.AppendLine("  SET @index = @index + 1;  ");
            query.AppendLine("  END");
            query.AppendLine("SELECT dbo.GetGeoJSON(GEO) FROM @Return                                                           ");


            try
            {
                return Context.Database.SqlQuery<string>(query.ToString()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IEnumerable<Domain.ViewTables.Polygon> SplitPolygon(string LineString, string PolygonString, Guid? IDCiclo)
        {
            StringBuilder query = new StringBuilder();

            if (PolygonString.Contains("MULTIPOLYGON"))
                query.AppendLine("  DECLARE @Polygon GEOGRAPHY = GEOGRAPHY::STMPolyFromText(" + PolygonString + ")   ");
            else
                query.AppendLine("  DECLARE @Polygon GEOGRAPHY = GEOGRAPHY::STPolyFromText(" + PolygonString + ")   ");


            query.AppendLine("  DECLARE @Line GEOGRAPHY    = GEOGRAPHY::STLineFromText(" + LineString + ")       ");
            query.AppendLine("  DECLARE @result GEOGRAPHY  = @Polygon.STDifference(@Line.STBuffer(0.1))            ");

            query.AppendLine("  SELECT                                                                             ");
            query.AppendLine("         objID                                                                     , ");
            query.AppendLine("         ROUND((@result.STGeometryN(n.rownum).STArea()/ 10000) ,2 )   AS Tamanho   , ");
            query.AppendLine("         dbo.GetGeoJSON(@result.STGeometryN(n.rownum))                AS GeoJson   , ");
            query.AppendLine("         @result.STGeometryN(n.rownum).ToString()                     AS GeoString , ");
            query.AppendLine("         'POLYGON'                                                    AS Type        ");
            query.AppendLine("  FROM                        ");
            query.AppendLine("  (                           ");
            query.AppendLine("      SELECT  TOP(@result.STNumGeometries()) row_number() over(order by @@spid) as rownum , NEWID() AS objID");
            query.AppendLine("      FROM sys.all_objects    ");
            query.AppendLine("  ) AS N                      ");

            return Context.Database.SqlQuery<Domain.ViewTables.Polygon>(query.ToString()).ToList();
        }
        public LastFirstPoint GetFirstLastPoint(string coord)
        {

            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @LineString GEOGRAPHY = GEOGRAPHY::STLineFromText(" + coord + ")  ");
            query.AppendLine("DECLARE @Return TABLE(                                                    ");
            query.AppendLine("      [FirstPoint][NVARCHAR](MAX)                                        ,");
            query.AppendLine("      [LastPoint][NVARCHAR](MAX)                                         ");
            query.AppendLine(")                                                                         ");

            query.AppendLine("INSERT INTO @Return(FirstPoint, LastPoint)                               ");
            query.AppendLine("SELECT REPLACE(REPLACE(REPLACE(@LineString.STStartPoint().ToString(), 'POINT (', '[') , ')',']'), ' ', ','), REPLACE(REPLACE(REPLACE(@LineString.STEndPoint().ToString(), 'POINT (', '['), ')', ']'), ' ',',')");
            query.AppendLine("SELECT * FROM @Return                                                     ");
            return Context.Database.SqlQuery<LastFirstPoint>(query.ToString()).FirstOrDefault();
        }
        public string GetReorientGeoString(string GeoString)
        {
            StringBuilder query = new StringBuilder();
            if (GeoString.Contains("MULTIPOLYGON (((") == true)
            {
                string json_geo = GeoString.Replace("MULTIPOLYGON (((", "LINESTRING(");
                json_geo = json_geo.Replace(")), ((", ", ");
                json_geo = json_geo.Replace(")))", ")");
                GeoString = json_geo;

                query.AppendLine("DECLARE @geo GEOGRAPHY = GEOGRAPHY::STGeomFromText('" + GeoString + "', 4326).MakeValid() ");
                query.AppendLine("                                                                                          ");
                query.AppendLine("SELECT @geo.ToString()                                                                    ");
            }
            else
            {
                query.AppendLine("DECLARE @geo GEOGRAPHY = GEOGRAPHY::STGeomFromText('" + GeoString + "', 4326).MakeValid() ");
                query.AppendLine("DECLARE @Tamanho FLOAT                                                                    ");
                query.AppendLine("SELECT @Tamanho = @geo.STArea()                                                           ");
                query.AppendLine("IF(@Tamanho > 10000000)                                                                   ");
                query.AppendLine("	SET @geo = @geo.ReorientObject();                                                       ");
                query.AppendLine("                                                                                          ");
                query.AppendLine("SELECT @geo.ToString()                                                                    ");
            }

            return Context.Database.SqlQuery<string>(query.ToString()).FirstOrDefault();
        }
        public PolyString SplitZones(LineSplitZones obj)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @PolyToLine1 GEOGRAPHY = GEOGRAPHY::STLineFromText('" + obj.LineStringPoly1 + "',4326).MakeValid()");
            query.AppendLine("DECLARE @PolyToLine2 GEOGRAPHY = GEOGRAPHY::STLineFromText('" + obj.LineStringPoly2 + "',4326).MakeValid()");
            query.AppendLine("SELECT * FROM SplitLineZone(@PolyToLine1.ToString(), @PolyToLine2.ToString())");
            return Context.Database.SqlQuery<PolyString>(query.ToString()).FirstOrDefault();
        }

        public IEnumerable<string> SplitWitinPoly(LineSplitZones obj)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM GenerateZones('" + obj.LineStringPoly1 + "','" + obj.LineStringPoly2 + "') WHERE Geo IS NOT NULL");
            return Context.Database.SqlQuery<string>(query.ToString()).ToList();
        }
        public string GetSplitWithinPolygon(LineSplitZones obj)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @Geo          GEOGRAPHY = GEOGRAPHY::STPolyFromText('" + obj.LineStringPoly1 + "',4326).MakeValid()    ");
            query.AppendLine("DECLARE @LineSplit    GEOGRAPHY = GEOGRAPHY::STPolyFromText('" + obj.LineStringPoly2 + "',4326).MakeValid()    ");
            query.AppendLine("DECLARE @Result NVARCHAR(MAX)                                                                                 ");
            query.AppendLine("DECLARE @Geo_GEOMETRY			    GEOMETRY                                                                    ");
            query.AppendLine("DECLARE @LineSplit_GEOMETRY		GEOMETRY                                                                    ");
            query.AppendLine("DECLARE @SizePoly                 INT = 0                                                                     ");

            query.AppendLine("                                                                                                              ");
            query.AppendLine("IF (@Geo.STArea() > 40000000000000)                                                                           ");
            query.AppendLine("  BEGIN                                                                                                       ");
            query.AppendLine("	    SET @SizePoly = 1                                                                                       ");
            query.AppendLine("  END                                                                                                         ");

            query.AppendLine("                                                                                                              ");
            query.AppendLine("IF (@LineSplit.STArea() > 40000000000000)                                                                           ");
            query.AppendLine("  BEGIN                                                                                                       ");
            query.AppendLine("	    SET @SizePoly = 1                                                                                       ");
            query.AppendLine("  END                                                                                                         ");

            query.AppendLine("                                                                                                              ");
            query.AppendLine("IF (@SizePoly = 0)                                                                                            ");
            query.AppendLine("  BEGIN                                                                                                       ");
            query.AppendLine("      SET @Result = dbo.GetGeoJSON(@LineSplit.STIntersection(@Geo))                            ");
            query.AppendLine("  END                                                                                                         ");


            query.AppendLine("                                                                                                              ");
            query.AppendLine("IF (@SizePoly > 0)                                                                                            ");
            query.AppendLine("  BEGIN                                                                                                       ");
            query.AppendLine("	    SET @Result = dbo.GetGeoJSONGMT( GEOMETRY::STPolyFromText(@Geo.ToString(),4326).STIntersection(GEOMETRY::STPolyFromText(@LineSplit.ToString(),4326)))");
            query.AppendLine("  END                                                                                                         ");
            query.AppendLine("                                                                                                              ");
            query.AppendLine("SELECT  @Result                                                                                               ");

            return Context.Database.SqlQuery<string>(query.ToString()).FirstOrDefault();
        }
        public string GetSplitPoly(LineSplitZones obj)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @Geo          GEOGRAPHY = GEOGRAPHY::STPolyFromText('" + obj.LineStringPoly1 + "',4326)   ");
            query.AppendLine("DECLARE @LineSplit    GEOGRAPHY = GEOGRAPHY::STPolyFromText('" + obj.LineStringPoly2 + "',4326)   ");

            query.AppendLine("DECLARE @Alter_Geography_Geometry INT = 0                                                         ");

            query.AppendLine("IF (@Geo.STArea() > 40000000000000)                                                               ");
            query.AppendLine("BEGIN                                                                                             ");
            query.AppendLine("	SET @Alter_Geography_Geometry = 1	                                                            ");
            query.AppendLine("END                                                                                               ");

            query.AppendLine("IF (@LineSplit.STArea() > 40000000000000)                                                         ");
            query.AppendLine("BEGIN                                                                                             ");
            query.AppendLine("	SET @Alter_Geography_Geometry = 1	                                                            ");
            query.AppendLine("END                                                                                               ");

            query.AppendLine("IF (@Alter_Geography_Geometry = 0)                                                                ");
            query.AppendLine("BEGIN                                                                                             ");
            query.AppendLine("  	SELECT  dbo.GetGeoJSON(@Geo.STDifference(@LineSplit))                                       ");
            query.AppendLine("END                                                                                               ");



            query.AppendLine("IF (@Alter_Geography_Geometry = 1)                                                                     ");
            query.AppendLine("BEGIN                                                                                                  ");
            query.AppendLine("  DECLARE @Geo_GEOM          GEOMETRY = GEOMETRY::STPolyFromText('" + obj.LineStringPoly1 + "',4326)   ");
            query.AppendLine("  DECLARE @LineSplit_GEOM    GEOMETRY = GEOMETRY::STPolyFromText('" + obj.LineStringPoly2 + "',4326)   ");
            query.AppendLine("  SELECT dbo.GetGeoJSONGMT(@Geo_GEOM.STDifference(@LineSplit_GEOM))                                  ");
            query.AppendLine("END                                                                                                    ");


            return Context.Database.SqlQuery<string>(query.ToString()).FirstOrDefault();

        }
        public IEnumerable<string> GenerateZones(LineSplitZones obj)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @Geo          GEOGRAPHY = GEOGRAPHY::STPolyFromText('" + obj.LineStringPoly1 + "',4326)   ");
            query.AppendLine("DECLARE @LineSplit    GEOGRAPHY = GEOGRAPHY::STPolyFromText('" + obj.LineStringPoly2 + "',4326)   ");
            query.AppendLine("                                                                                                  ");
            query.AppendLine("DECLARE @Result                       NVARCHAR(MAX)                                               ");
            query.AppendLine("DECLARE @Geo_GEOMETRY                 GEOMETRY                                                    ");
            query.AppendLine("DECLARE @LineSplit_GEOMETRY           GEOMETRY                                                    ");
            query.AppendLine("DECLARE @SizePoly                     INT = 0                                                     ");

            query.AppendLine("                                                                                                  ");
            query.AppendLine("IF(@Geo.STArea() > 40000000000000)                                                                ");
            query.AppendLine("BEGIN                                                                                             ");
            query.AppendLine("  SET @SizePoly = 1                                                                               ");
            query.AppendLine("END                                                                                               ");

            query.AppendLine("                                                                                                  ");
            query.AppendLine("IF(@LineSplit.STArea() > 40000000000000)                                                          ");
            query.AppendLine("BEGIN                                                                                             ");
            query.AppendLine("  SET @SizePoly = 1                                                                               ");
            query.AppendLine("END                                                                                               ");

            query.AppendLine("                                                                                                  ");
            query.AppendLine("IF(@SizePoly = 0)                                                                                 ");
            query.AppendLine("BEGIN                                                                                             ");
            query.AppendLine("  DECLARE @Update_Geo1 GEOGRAPHY                                                                  ");
            query.AppendLine("  SET @Update_Geo1 = (@Geo.STIntersection(@LineSplit))                                            ");
            query.AppendLine("  SET @Result = dbo.GetGeoJSON(@LineSplit.STDifference(@Update_Geo1))                             ");
            query.AppendLine("END                                                                                               ");

            query.AppendLine("                                                                                                  ");
            query.AppendLine("IF(@SizePoly > 0)                                                                                 ");
            query.AppendLine("BEGIN                                                                                             ");
            query.AppendLine("  DECLARE @Update_Geo2 GEOMETRY                                                                   ");
            query.AppendLine("  SET @Update_Geo2    = ( GEOMETRY::STPolyFromText(@Geo.ToString(), 4326).STIntersection(GEOMETRY::STPolyFromText(@LineSplit.ToString(), 4326)) ) ");
            query.AppendLine("  SET @Result			= dbo.GetGeoJSONGMT(GEOMETRY::STPolyFromText(@LineSplit.ToString(),4326).STDifference(@Update_Geo2))                        ");
            query.AppendLine("END");

            query.AppendLine("                                                                                                  ");
            query.AppendLine("SELECT @Result                                                                                    ");

            return Context.Database.SqlQuery<string>(query.ToString()).ToList();
        }
        public IEnumerable<string> GenerateZonesInternal(LineSplitZones obj)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @Geo          GEOGRAPHY = GEOGRAPHY::STPolyFromText('" + obj.LineStringPoly1 + "',4326)   ");
            query.AppendLine("DECLARE @LineSplit    GEOGRAPHY = GEOGRAPHY::STPolyFromText('" + obj.LineStringPoly2 + "',4326)   ");
            query.AppendLine("                                                                                                  ");
            query.AppendLine("DECLARE @Result                       NVARCHAR(MAX)                                               ");
            query.AppendLine("DECLARE @Geo_GEOMETRY                 GEOMETRY                                                    ");
            query.AppendLine("DECLARE @LineSplit_GEOMETRY           GEOMETRY                                                    ");
            query.AppendLine("DECLARE @SizePoly                     INT = 0                                                     ");

            query.AppendLine("                                                                                                  ");
            query.AppendLine("IF(@Geo.STArea() > 40000000000000)                                                                ");
            query.AppendLine("BEGIN                                                                                             ");
            query.AppendLine("  SET @SizePoly = 1                                                                               ");
            query.AppendLine("END                                                                                               ");

            query.AppendLine("                                                                                                  ");
            query.AppendLine("IF(@LineSplit.STArea() > 40000000000000)                                                          ");
            query.AppendLine("BEGIN                                                                                             ");
            query.AppendLine("  SET @SizePoly = 1                                                                               ");
            query.AppendLine("END                                                                                               ");

            query.AppendLine("                                                                                                  ");
            query.AppendLine("IF(@SizePoly = 0)                                                                                 ");
            query.AppendLine("BEGIN                                                                                             ");
            query.AppendLine("  SET @Result = dbo.GetGeoJSON(@Geo.STIntersection(@LineSplit))                                   ");
            query.AppendLine("END                                                                                               ");

            query.AppendLine("                                                                                                  ");
            query.AppendLine("IF(@SizePoly > 0)                                                                                 ");
            query.AppendLine("BEGIN                                                                                             ");
            query.AppendLine("  DECLARE @Update_Geo2 GEOMETRY                                                                   ");
            query.AppendLine("  SET @Result = dbo.GetGeoJSONGMT((GEOMETRY::STPolyFromText(@Geo.ToString(), 4326).STIntersection(GEOMETRY::STPolyFromText(@LineSplit.ToString(), 4326)))) ");
            query.AppendLine("END");

            query.AppendLine("                                                                                                  ");
            query.AppendLine("SELECT @Result                                                                                    ");

            return Context.Database.SqlQuery<string>(query.ToString()).ToList();
        }

        public string GetGeoPointJSON(string strQUERY)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @g2 geometry = " + strQUERY);
            query.AppendLine("SELECT dbo.GetGeoJSONGMT(@g2)");
            return Context.Database.SqlQuery<string>(query.ToString()).FirstOrDefault();
        }

        public string GetGeoJson(string stringQUERY)
        {
            StringBuilder query = new StringBuilder();
            if (stringQUERY.Contains("\""))
                stringQUERY = stringQUERY.Replace("\"", "");

            query.AppendLine("DECLARE @g2 geometry = " + stringQUERY);
            query.AppendLine("SELECT dbo.GetGeoJSONGMT(@g2)");
            return Context.Database.SqlQuery<string>(query.ToString()).FirstOrDefault();
        }

        public string GetGeoCenter(string strQUERY)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @g2 geometry = " + strQUERY);
            query.AppendLine("SELECT dbo.GetGeoJSONGMT(@g2.STCentroid())");
            return Context.Database.SqlQuery<string>(query.ToString()).FirstOrDefault();
        }

        public List<string> GenerateGeoJsonPoints(List<string> lstpoly)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @ID INT");

            query.AppendLine("DECLARE @lstPoly TABLE (");
            query.AppendLine("  ID INT IDENTITY(1,1) PRIMARY KEY, ");
            query.AppendLine("  geo GEOMETRY ");
            query.AppendLine(")");
            foreach (var poly in lstpoly)
            {
                query.AppendLine("INSERT INTO @lstPoly VALUES(GEOMETRY::STPolyFromText('" + poly + ")");
            }
            query.AppendLine("DECLARE @lstMenor TABLE(");

            query.AppendLine("  ID INT IDENTITY(1, 1) PRIMARY KEY,    ");
            query.AppendLine("  geo GEOMETRY, ");
            query.AppendLine("  tamanho FLOAT ");
            query.AppendLine(")");
            query.AppendLine("INSERT INTO @lstMenor(geo, tamanho)");
            query.AppendLine("SELECT geo, geo.STArea() FROM @lstPoly");
            query.AppendLine("SELECT TOP(1) @ID = ID FROM @lstMenor ORDER BY tamanho ASC");
            query.AppendLine("SELECT 'GEOMETRY::STPolyFromText(''' + geo.ToString() + '''\",4326)' FROM @lstMenor WHERE ID<> @ID");


            return Context.Database.SqlQuery<string>(query.ToString()).ToList();
        }

        public bool GetWithinPoint(string point, string poly)
        {
            StringBuilder query = new StringBuilder();

            if (poly.Contains(", 0)"))
                poly = poly.Replace(", 0)", ", 4326)");

            query.AppendLine("DECLARE @POLY GEOMETRY = " + poly.Replace("\"", ""));
            query.AppendLine("DECLARE @POINT GEOMETRY = " + point.Replace(", 0)", ", 4326)"));
            query.AppendLine("SELECT @POINT.STWithin(@POLY)");
            return Context.Database.SqlQuery<bool>(query.ToString()).FirstOrDefault();
        }

        public IEnumerable<LstPontos> SplitPoly(string poly)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT pontos.ToString() AS pontos FROM fConvertPolyToListPoint('" + poly + "')");
            return Context.Database.SqlQuery<LstPontos>(query.ToString()).ToList();
        }

        public bool CheckedPoly(string points_poly)
        {
            bool result = true;
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("DECLARE @poligono geography;");
                query.AppendLine("SET @poligono = geography::STPolyFromText('" + points_poly + "',4326)");
                query.AppendLine("SELECT @poligono.STIsValid()");
                result = Context.Database.SqlQuery<bool>(query.ToString()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public IEnumerable<string> OrdePoint(List<string> lstPoint)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @T TABLE( geo GEOMETRY)");
            for (int i = 0; i < lstPoint.Count; i++)
            {
                query.AppendLine("INSERT INTO @T VALUES(" + lstPoint[i] + ")");
            }

            query.AppendLine("DECLARE @Sequencia TABLE(ID int IDENTITY(1,1) PRIMARY KEY, geo GEOMETRY)");
            query.AppendLine("INSERT INTO @Sequencia(geo)");
            query.AppendLine("SELECT geo FROM @T ORDER BY geo.STEnvelope().STPointN(1).STX, geo.STEnvelope().STPointN(1).STY;");
            query.AppendLine("SELECT 'GEOMETRY::STPointFromText(''' + geo.ToString() +''',4326)' FROM @Sequencia\r\n");
            return Context.Database.SqlQuery<string>(query.ToString()).ToList();
        }

        public IEnumerable<string> OrderPoly(List<string> lstPoly)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @T TABLE( geo GEOMETRY)");
            string first_poly = "geometry::STPolyFromText('POLYGON" + lstPoly[0].Replace(",-", " -");
            first_poly = first_poly.Replace("[[", "((").Replace("],[", ",").Replace("]]", "))',4326)"); 
            for (int i = 0; i < lstPoly.Count; i++)
            {
                string poly = "geometry::STPolyFromText('POLYGON" + lstPoly[i].Replace(",-", " -");
                poly = poly.Replace("[[", "((").Replace("],[",",").Replace("]]", "))',4326)");
                query.AppendLine("INSERT INTO @T VALUES(" + poly + ")");
            }
            query.AppendLine("DECLARE @Sequencia TABLE( geo GEOMETRY)");
            query.AppendLine("INSERT INTO @Sequencia(geo)");
            query.AppendLine("SELECT geo FROM @T ORDER BY geo.STEnvelope().STPointN(1).STX, geo.STEnvelope().STPointN(1).STY;");
            query.AppendLine("SELECT REPLACE(REPLACE(dbo.GetGeoJSONGMT(geo), '{\"type\": \"Polygon\",\"coordinates\":',''), '}','') FROM @Sequencia");

            //query.AppendLine("DECLARE @polys geometry;");
            //query.AppendLine("DECLARE @bounding_box geometry;");
            //query.AppendLine("DECLARE @geo_first geometry;");

            //query.AppendLine("SELECT @polys = geometry::UnionAggregate(geo) FROM @T;");
            //query.AppendLine("SELECT @bounding_box = @polys.STEnvelope();");

            //query.AppendLine("SELECT TOP 1 @geo_first = geo FROM @T ORDER BY ((SELECT MIN(distance) FROM (VALUES (geo.STDistance(@bounding_box.STPointN(1))), (geo.STDistance(@bounding_box.STPointN(2))), (geo.STDistance(@bounding_box.STPointN(3))), (geo.STDistance(@bounding_box.STPointN(4)))) AS value(distance))) ASC;");
            //query.AppendLine("WITH OrderedPolys AS (");
            //query.AppendLine("    SELECT *,");
            //query.AppendLine("           ROW_NUMBER() OVER (ORDER BY geo.STDistance(@geo_first.STCentroid())) AS RowNumber,");
            //query.AppendLine("           LEAD(geo) OVER (ORDER BY geo.STDistance(@geo_first.STCentroid())) AS NextPolygon");
            //query.AppendLine("    FROM @T");
            //query.AppendLine("),");
            //query.AppendLine("IntersectingPolys AS (");
            //query.AppendLine("    SELECT *,");
            //query.AppendLine("           CASE WHEN geo.STIntersects(NextPolygon) = 1 THEN 1 ELSE 0 END AS Intersection");
            //query.AppendLine("    FROM OrderedPolys");
            //query.AppendLine(")");
            //
            //query.AppendLine("ORDER BY Intersection DESC, geo.STDistance(@geo_first.STCentroid())");

            return Context.Database.SqlQuery<string>(query.ToString()).ToList();
        }
    }
}
