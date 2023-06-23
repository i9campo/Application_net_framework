using FluentValidation.Results;
using NetTopologySuite.IO.ShapeFile.Extended;
using Sigma.Domain.Auxiliar;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Context_BNGDB;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using static Sigma.Domain.ViewTables.OpenGeo;

namespace Sigma.Infra.Data.Repositories
{
    public class ShapeRepository : RepositoryBase<Domain.ViewTables.Shape>, IShapeRepository
    {
        public IEnumerable<BNG_Shape> GetListShapeByAreaServico(Guid IDSafra, Guid IDArea)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM BNG.dbo.Shape WHERE IDSafra = '" +IDSafra+ "' AND IDArea = '" +IDArea+ "' ORDER BY nome"); 

            return Context.Database.SqlQuery<BNG_Shape>(query.ToString()).ToList();
        }

        public IEnumerable<FileExt> GetFileByIDShape(Guid IDShape, int orbita)
        {
            List<FileExt> PolygonList = new List<FileExt>();
            FileExt NewFile = new FileExt();
            
            StringBuilder shp_query = new StringBuilder();
            shp_query.AppendLine("SELECT * FROM BNG.dbo.Shape WHERE objID = '" + IDShape + "'");
            BNG_Shp_File_Date shp = Context.Database.SqlQuery<BNG_Shp_File_Date>(shp_query.ToString()).FirstOrDefault();

            var shapeFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Shapes/"), IDShape.ToString());
            if (!Directory.Exists(shapeFilePath))
                Directory.CreateDirectory(shapeFilePath);

            string Path_SHP_FILE  = Path.Combine(HttpContext.Current.Server.MapPath("~/Shapes/"), IDShape.ToString());

            string shp_file = Path.Combine(Path_SHP_FILE, shp.nome);
            shp_file = Path.ChangeExtension(shp_file, "shp");
            File.WriteAllBytes(shp_file, shp.shp);

            string dbf_file = Path.Combine(Path_SHP_FILE, shp.nome);
            dbf_file = Path.ChangeExtension(dbf_file, "dbf");
            File.WriteAllBytes(dbf_file, shp.dbf);

            ShapeDataReader ShpDataReader = new ShapeDataReader(shp_file);

            using (var reader = ShpDataReader.ReadByMBRFilter(ShpDataReader.ShapefileBounds).GetEnumerator())
            {
                try
                {
                    while (reader.MoveNext())
                    {
                        NewFile = new FileExt();
                        NewFile.objID = Guid.NewGuid();
                        NewFile.CoordUtmConvert = false;
                        NewFile.Nome = shp.nome.ToUpper(); 

                        var geoDate = reader.Current.Geometry;
                        var attrib = reader.Current.Attributes;

                        NewFile.Rotulo = new List<string>();
                        NewFile.jsonField = "{\"";
                        foreach (var name in attrib.GetNames())
                        {
                            NewFile.Rotulo.Add(name);

                            if (!name.Equals("LEGENDA"))
                            {
                                string value = "";
                                if (attrib.GetOptionalValue(name).ToString().Contains(","))
                                    value = attrib.GetOptionalValue(name).ToString().Replace(",", ".");
                                else
                                    value = attrib.GetOptionalValue(name).ToString();

                                NewFile.jsonField += name + "\":\"" + value + "\", \"";
                            }
                            else
                            {
                                NewFile.centerLegend = attrib.GetOptionalValue(name).ToString();
                            }
                        }
                        NewFile.jsonField = NewFile.jsonField.Remove(NewFile.jsonField.Length - 4) + "\" }";

                        NewFile.type = geoDate.GeometryType;
                        string StringPolygon = "";
                        if (NewFile.type.Equals("Polygon") || NewFile.type.Equals("LineString"))
                        {
                            StringPolygon = "POLYGON((";
                            NewFile.type = "Polygon";
                        }
                        else
                            StringPolygon = "POINT(";

                        if (orbita == 1)
                        {
                            for (int i = 0; i < geoDate.Coordinates.Length; i++)
                            {
                                if (Double.Parse(geoDate.Coordinates[i].X.ToString()) > 90)
                                {
                                    NewFile.objID = Guid.Empty;
                                    NewFile.CoordUtmConvert = true;
                                    PolygonList.Add(NewFile);
                                    ShpDataReader.Dispose();
                                    reader.Dispose();

                                    //reader.Close();
                                    AuxShape.DeleteFiles(Path_SHP_FILE);
                                    return PolygonList;
                                }

                                if (i >= 0 && i < geoDate.Coordinates.Length - 1)
                                    StringPolygon += geoDate.Coordinates[i].X.ToString().Replace(",", ".") + " " + geoDate.Coordinates[i].Y.ToString().Replace(",", ".") + " , ";
                                else
                                    if (StringPolygon.Contains("POINT"))
                                    StringPolygon += geoDate.Coordinates[i].X.ToString().Replace(",", ".") + " " + geoDate.Coordinates[i].Y.ToString().Replace(",", ".") + " ) ";
                                else
                                    StringPolygon += geoDate.Coordinates[i].X.ToString().Replace(",", ".") + " " + geoDate.Coordinates[i].Y.ToString().Replace(",", ".") + " )) ";

                            }
                            NewFile.GeoString = StringPolygon;
                        }
                        else
                        {
                            for (int i = 0; i < geoDate.Coordinates.Length; i++)
                            {
                                Double X = Double.Parse(geoDate.Coordinates[i].X.ToString());
                                Double Y = Double.Parse(geoDate.Coordinates[i].Y.ToString());
                                double[] pontos = new Double[2];
                                if (X < 0 && Y < 0)
                                {
                                    pontos[0] = X;
                                    pontos[1] = Y;
                                }
                                else
                                    pontos = ConversorCordenadas.ConvertUTMDecimal(X, Y, orbita, 's');

                                if (i >= 0 && i < geoDate.Coordinates.Length - 1)
                                {
                                    StringPolygon += pontos[0].ToString().Replace(',', '.') + " " + pontos[1].ToString().Replace(',', '.') + " , ";
                                }
                                else
                                {
                                    if (StringPolygon.Contains("POINT"))
                                        StringPolygon += pontos[0].ToString().Replace(',', '.') + " " + pontos[1].ToString().Replace(',', '.') + ") ";
                                    else
                                        StringPolygon += pontos[0].ToString().Replace(',', '.') + " " + pontos[1].ToString().Replace(',', '.') + " )) ";

                                    if (!StringPolygon.Contains("POINT"))
                                    {
                                        string FirstStringSplit = StringPolygon.Split('(')[2].Split(',')[0].Trim();
                                        int LengthSplitString = StringPolygon.Split('(')[2].Split(',').Length;
                                        string LastStringSplit = StringPolygon.Split('(')[2].Split(',')[(LengthSplitString - 1)].ToString().Replace(')', ' ').Trim();

                                        if (!FirstStringSplit.Equals(LastStringSplit))
                                            StringPolygon = StringPolygon.Replace(")", "") + " , " + FirstStringSplit + "))";
                                    }
                                }
                            }

                            NewFile.GeoString = StringPolygon;
                        }
     
                        PolygonList.Add(NewFile);
                    }

                }
                catch (Exception error) { }

                reader.Dispose();
                ShpDataReader.Dispose();

                AuxShape.DeleteFiles(Path_SHP_FILE);
                return PolygonList;
            }
        }

        public ValidationResult Add(Domain.ViewTables.Shape entity)
        {
            throw new NotImplementedException();
        }

        public bool ExportSHPToBNG(ShapeBNG obj)
        {
            try
            {
                BNGContextDataContext dc = new BNGContextDataContext();
                Acesso oAcesso = new Acesso();
                oAcesso = (from u in dc.GetTable<Acesso>()
                           where u.login.Equals("1") && u.senha.Equals("2311") && u.IDProprietario == null
                           select u).SingleOrDefault();

                Table<Context_BNGDB.Shape> tbShape = dc.GetTable<Context_BNGDB.Shape>(); 

                Context_BNGDB.Shape shp = new Context_BNGDB.Shape();
                shp.objID = Guid.NewGuid().ToString();
                shp.IDPropriedadeRural = obj.IDPropriedade;
                shp.IDArea = obj.IDArea;
                shp.IDSafra = obj.IDSafra;
                shp.nome = obj.nome;
                shp.tipo = obj.tipo; 
                shp.shp = obj.shp;
                shp.dbf = obj.dbf;
                shp.ponto0020 = obj.ponto0020;
                shp.ponto2040 = obj.ponto2040;
                shp.amostrasimples = obj.amostrasimples;

                tbShape.InsertOnSubmit(shp);
                dc.SubmitChanges();

                return true; 
            }
            catch (Exception ex)
            {
                return false; 
            }
        }


        public string GetGeoJson(string CoordString)
        {
            string opengeojson = "DECLARE @g geography; " +
                   "SET @g = geography::STPolyFromText(" + CoordString + ") " +
                   "SELECT dbo.GetGeoJSON(@g.MakeValid())";

            return Context.Database.SqlQuery<String>(opengeojson).FirstOrDefault();
        }

        public string GetGeoJsonPonto(string CoordString)
        {
            string opengeojson = "DECLARE @g geography; " +
            "SET @g = geography::STPointFromText(" + CoordString + ") " +
            "SELECT dbo.GetGeoJSON(@g.MakeValid())";

            return Context.Database.SqlQuery<String>(opengeojson).FirstOrDefault();
        }

        public IEnumerable<GeoOBJ> OpenGeoZonas(Guid ID, Guid IDAreaServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @Result TABLE(                                                      ");
            query.AppendLine("                         [ID]			    [UNIQUEIDENTIFIER]			NULL, ");
            query.AppendLine("                         [objID]			[UNIQUEIDENTIFIER]			NULL, ");
            query.AppendLine("                         [IDAreaServico]  [UNIQUEIDENTIFIER]			NULL, "); 
            query.AppendLine("                         [ZONA]		    [NVARCHAR]			  (MAX)	NULL, ");
            query.AppendLine("                         [ZN_AREA]	    [NVARCHAR]			  (MAX) NULL, ");
            query.AppendLine("                         [ZN_CODIGO]		[NVARCHAR]			  (MAX) NULL, ");
            query.AppendLine("                         [TAMANHO]		[FLOAT]						NULL, "); 
            query.AppendLine("                         [CODIGO]		    [INT]						NULL, ");
            query.AppendLine("                         [geoJson]		[NVARCHAR]			  (MAX) NULL, ");
            query.AppendLine("                         [jsonField]		[NVARCHAR]			  (MAX) NULL, ");
            query.AppendLine("                         [type]		    [NVARCHAR]			  (MAX) NULL  ");
            query.AppendLine(")                                                                           ");

            query.AppendLine("INSERT INTO @Result(ID, objID, IDAreaServico, ZONA, ZN_AREA, ZN_CODIGO, TAMANHO, CODIGO, geoJson, jsonField, type)");
            query.AppendLine("SELECT                                                                                                           ");
            query.AppendLine("       '"+ID+"'                                                                                                , ");
            query.AppendLine("       G.objID                                                                                                 , ");
            query.AppendLine("       G.IDAreaServico                                                                                         , ");
            query.AppendLine("       G.descricao                                                                                             , ");
            query.AppendLine("      (G.descricao + ' - ' + A.nome )                                                                          , ");
            query.AppendLine("      (G.descricao + ' - ' + CONVERT(NVARCHAR, G.codigo))                                                      , ");
            query.AppendLine("       G.tamanho                                                                                               , ");
            query.AppendLine("       G.codigo                                                                                                , ");
            query.AppendLine("       dbo.GetGeoJSON(G.geo.MakeValid())                                                                       , ");
            query.AppendLine("       G.jsonField                                                                                             , ");
            query.AppendLine("       'ZM'                                                                                                      "); 
            query.AppendLine("  FROM Grid G                                                                                     ");
            query.AppendLine("  INNER JOIN AreaServico ARS ON G.IDAreaServico = ARS.objID                                       ");
            query.AppendLine("  INNER JOIN Area A   ON A.objID = ARS.IDArea                                                     ");
            query.AppendLine("  WHERE IDAreaServico = '" + IDAreaServico + "'                                                   ");
            query.AppendLine(" SELECT * FROM @Result "); 

            return Context.Database.SqlQuery<GeoOBJ>(query.ToString()).ToList();
        }

        public ValidationResult Remove(Domain.ViewTables.Shape entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Domain.ViewTables.Shape entity)
        {
            throw new NotImplementedException();
        }

        Domain.ViewTables.Shape IRepository<Domain.ViewTables.Shape>.Find(Guid objID)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Domain.ViewTables.Shape> IRepository<Domain.ViewTables.Shape>.GetAll()
        {
            throw new NotImplementedException();
        }

  
    }
}
