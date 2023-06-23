
using AutoMapper;
using Sigma.Domain.Auxiliar;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using TatukGIS.NDK;

using GeoAPI.Geometries;
using ProjNet.CoordinateSystems;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

using static Sigma.Domain.ViewTables.OpenGeo;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading;
using ProjNet.CoordinateSystems.Transformations;
using NetTopologySuite.IO.ShapeFile.Extended;
using Aspose.Zip.Rar;
using Aspose.Zip;
using System.Linq;
using System.Text.RegularExpressions;
using NetTopologySuite.IO.KML;
using System.Xml.Linq;

namespace Sigma.Domain.Services
{
    public class ShapeService : Service<Shape>, IShapeService
    {
        private readonly IShapeRepository _repository;
        private readonly IAreaServicoRepository _areaServicoRepository;
        private readonly IAnaliseSoloRepository _analiseSoloRepository;
        private readonly IGridRepository _gridRepository;
        private readonly IGeoConfigRepository _geoConfig;
        private TGIS_Shape SHP { get; set; }
        public ShapeService(IShapeRepository repository, IAreaServicoRepository AreaServicoRepository, IAnaliseSoloRepository AnaliseSoloRepository, IGridRepository GridRepository, IGeoConfigRepository geoConfig)
            : base(repository)
        {
            _repository = repository;
            _areaServicoRepository = AreaServicoRepository;
            _analiseSoloRepository = AnaliseSoloRepository;
            _gridRepository = GridRepository;
            _geoConfig = geoConfig;
        }
        public IEnumerable<BNG_Shape> GetListShapeByAreaServico(Guid IDSafra, Guid IDArea)
        {
            return _repository.GetListShapeByAreaServico(IDSafra, IDArea);
        }
        public IEnumerable<IEnumerable<FileExt>> GetFileByIDShape(IEnumerable<string> IDShape, int orbita)
        {
            List<List<FileExt>> New_FileExt = new List<List<FileExt>>();
            foreach (var ID in IDShape)
            {
                bool brk = false;
                IEnumerable<FileExt> item = _repository.GetFileByIDShape(Guid.Parse(ID), orbita);

                List<FileExt> div = new List<FileExt>();
                List<FileExt> lin = new List<FileExt>();

                foreach (var shp_file in item)
                {
                    if (shp_file.CoordUtmConvert == true)
                    {
                        div.Add(shp_file);
                        New_FileExt.Add(div);

                        brk = true;
                        break;
                    }
                    else
                    {
                        shp_file.GeoString = _geoConfig.GetReorientGeoString(shp_file.GeoString);

                        if (shp_file.GeoString.Contains("MULTIPOLYGON"))
                            shp_file.GeoString = _geoConfig.GetReorientGeoString(shp_file.GeoString);

                        if (shp_file.GeoString.ToString().Contains("MULTILINE"))
                        {
                            var reader = new WKTReader();
                            var geometry = reader.Read(shp_file.GeoString);

                            MultiLineString multiLineString = geometry as MultiLineString;
                            if (multiLineString != null)
                            {
                                var lineStrings = multiLineString.Geometries.Select(g => (LineString)g).ToList();
                                foreach (var lns in lineStrings)
                                {
                                    shp_file.geoJson = _geoConfig.GetGeoJson(lns.ToString(), null);
                                    shp_file.type = lns.ToString().Contains("LINESTRING") == true ? "LineString" : shp_file.type;
                                    if (shp_file.type.Equals("LineString"))
                                    {
                                        shp_file.Nome = shp_file.Nome + " LINE ";
                                        List<FileExt> New_Line = new List<FileExt>();
                                        New_Line.Add(shp_file);
                                        if (!New_FileExt.Any(l => l.SequenceEqual(New_Line))) //verifica se New_Line já existe no New_FileExt
                                        {
                                            New_FileExt.Add(New_Line);
                                        }
                                    }
                                    else
                                        if (!div.Exists(f => IsInsideWithNetTopology(f.GeoString, shp_file.GeoString)))
                                        {
                                            div.Add(shp_file);
                                        }
                                }
                            }
                        }
                        else
                        {
                            shp_file.geoJson = _geoConfig.GetGeoJson(shp_file.GeoString, null);
                            shp_file.type = shp_file.GeoString.Contains("LINESTRING") == true ? "LineString" : shp_file.type;

                            if (shp_file.type.Equals("LineString"))
                            {
                                shp_file.Nome = shp_file.Nome + " LINE ";
                                List<FileExt> New_Line = new List<FileExt>();
                                New_Line.Add(shp_file);
                                if (!New_FileExt.Any(l => l.SequenceEqual(New_Line))) //verifica se New_Line já existe no New_FileExt
                                {
                                    New_FileExt.Add(New_Line);
                                }
                            }
                            else
                                if (!div.Exists(f => IsInsideWithNetTopology(f.GeoString, shp_file.GeoString)))
                                {
                                    div.Add(shp_file);
                                }
                        }
                    }
                }

                New_FileExt.Add(div);

                if (brk == true)
                    break;
            }
            return New_FileExt;
        }
        public IEnumerable<GeoOBJ> OpenGeoByAreaServico(Guid? ID, Guid IDAreaServico, string tipo)
        {
            IEnumerable<GeoOBJ> objLST = new List<GeoOBJ>();
            List<GeoOBJ> result = new List<GeoOBJ>();

            //Aqui gera uma lista de dados geograficos referente a zonas. 
            if (tipo.Equals("ZM"))
            {
                objLST = _repository.OpenGeoZonas(Guid.Parse(ID.ToString()), IDAreaServico);
                foreach (var item in objLST)
                {
                    List<string> r = new List<string>();
                    var teste = item.jsonField.Replace("\"obj\" : ", " ").Replace("{\"", "").Replace("\"", "").Replace("}", "").Split(',');
                    foreach (var i in teste)
                    {
                        if (!i.Split(':')[0].ToString().Trim(' ').Equals("IDROTULO"))
                            r.Add(i.Split(':')[0].ToString().Trim(' '));
                    }
                    item.Rotulo = r;
                }
            }

            ///// Aqui gera uma lista de dados geográficos referente a área. 
            if (tipo.Equals("AREA"))
            {
                AreaServicoView ars = _areaServicoRepository.FindFullAreaServico(IDAreaServico, null, null, true);
                GeoOBJ item = Mapper.Map<AreaServicoView, GeoOBJ>(ars);

                item.Rotulo = new List<string>() { "TAMANHO", "NOME_AREA", "SERVICO", "SERVICO_NUM", "CODIGO", "SAFRA" };

                /// Aqui será adicionado a lista de dados apresentado no rotulo. 
                item.area = ars.area;
                item.numServico = int.Parse(ars.numServico.ToString());
                item.CODIGO = int.Parse(ars.codigo.ToString());
                item.safra = ars.safra;

                if (ars.ID != null)
                    item.ID = Guid.Parse(ars.ID.ToString()); /// Aqui será inserido o ID da linha selecionada em arquivos abertos. 

                item.type = "AREA";      /// Aqui será adicionado o tipo de dado aberto. 

                result.Add(item);
                objLST = result;
            }

            ///// Aqui gera uma lista de dados geográficos referente aos pontos com a sub-amostras. 
            if (tipo.Equals("POINTS") || tipo.Equals("POINT"))
            {
                //IEnumerable<AnaliseSoloView> ans = _analiseSoloRepository.GetAnaliseSoloByAreaServico(IDAreaServico, 0);

                AreaServicoView ars = _areaServicoRepository.FindFullAreaServico(IDAreaServico, null, null, false);
                //foreach (var item in ans)
                //{
                //    /// Aqui será feito a união de todos os elementos da classe AnaliseSoloView para classe ZonaOBJ. 
                //    ZonaOBJ o = Mapper.Map<AnaliseSoloView, ZonaOBJ>(item);
                //    o.Rotulo = new List<string>() { "PONTO", "SEQUENCIA" };
                //    o.Field = new FieldOBJZM();
                //    o.SEQUENCIA = item.sequenciaSubA;

                //    o.Field.SEQUENCIA = item.sequenciaSubA;
                //    o.Field.PONTO = int.Parse(item.ponto.ToString());

                //    o.ID = ID.ToString();
                //    o.type = "POINTS";
                //    o.SERVICO_NUM = null;
                //    result.Add(o);
                //}
                //objLST = result;
            }

            return objLST;
        }
        public byte[] ShpCreateFile(IEnumerable<GeoCoordText> obj)
        {
            try
            {
                string FileName = "";

                var geomFactory = new GeometryFactory(new PrecisionModel(), 4326);

                WKTReader reader = new WKTReader(geomFactory);
                var features = new List<IFeature>();

                //// Aqui será realizado a leitura dos dados capturado do front. 
                foreach (var item in obj)
                {
                    if (String.IsNullOrEmpty(item.coord))
                        return null;

                    var geometry = reader.Read(item.coord);
                    var atr = new AttributesTable();
                    string fields = item.jsonField.Replace("{", " ").Replace("}", "");


                    var LstField = fields.Split(',');
                    foreach (var field in LstField)
                    {
                        var spltField = field.Split(':');
                        string v1 = "";
                        string v2 = "";
                        try
                        {
                            Regex regex = new Regex(@"\\");
                            v1 = regex.Replace(spltField[0], "");
                            v1 = regex.Replace(v1, "\"").Replace("\"", "").TrimStart();

                            v2 = regex.Replace(spltField[1], "");
                            v2 = regex.Replace(v2, "\"").Replace("\"", "").TrimStart();

                        }
                        catch (Exception)
                        {

                        }

                        if (v1.Length > 10)
                        {
                            int total = v1.Length;
                            total = v1.Length - 10;

                            v1 = v1.ToString().Remove(v1.Length - total);
                        }
                        atr.Add(v1, v2);
                    }
                    List<float> centerLegend = item.centerLegend.ToList();
                    atr.Add("LEGENDA", "{\"center\": [" + centerLegend[0].ToString().Replace(",", ".") + "," + centerLegend[1].ToString().Replace(",", ".") + "] }");

                    features.Add(new Feature(geometry, atr));
                    FileName = item.FileName;
                }


                var shapeFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Shapes/"), Guid.NewGuid().ToString());
                if (!Directory.Exists(shapeFilePath))
                    Directory.CreateDirectory(shapeFilePath);


                var prjPath = Path.Combine(shapeFilePath, $"{FileName}.prj");

                /// Create File PRJ. 
                using (var streamWriter = new StreamWriter(prjPath))
                {
                    streamWriter.Write(GeographicCoordinateSystem.WGS84.WKT);
                    streamWriter.Close();
                }

                var shpPath = Path.Combine(shapeFilePath, FileName);

                var writer = new ShapefileDataWriter(shpPath, geomFactory);
                //var writer = new ShapefileDataWriter(shpPath, GeometryFactory.Default);
                var outDbaseHeader = ShapefileDataWriter.GetHeader(features[0], features.Count);
                writer.Header = outDbaseHeader;
                writer.Write(features);

                /// Aqui verifica sé o arquivo é valido ou não. 
                var shapeFileReader = new ShapefileDataReader(shpPath, geomFactory);
                //var shapeFileReader = new ShapefileDataReader(shpPath, GeometryFactory.Default);
                var read = shapeFileReader.Read();
                shapeFileReader.Dispose();
                shapeFileReader.Close();


                byte[] FileByte; /// Essa variável será utilizada para carregar os dados do arquivo ZIPADO para download. 

                if (read == true)
                {
                    string startPath = @"" + shapeFilePath;
                    string zipPath = @"" + HttpContext.Current.Server.MapPath("~/Shapes/") + Guid.NewGuid().ToString() + ".zip";
                    ZipFile.CreateFromDirectory(startPath, zipPath);

                    FileByte = File.ReadAllBytes(zipPath);

                    AuxShape.DeleteFiles(startPath);
                    File.Delete(zipPath);
                    return FileByte;
                }
            }
            catch (Exception error)
            {
                return null;
            }
            return null;
        }
        public IEnumerable<FileExt> OpenGeoSHP(int Orbita, File64 file)
        {
            List<FileExt> PolygonList = new List<FileExt>();

            FileExt NewFile = new FileExt();
            NewFile.CoordUtmConvert = false;

            string ZipPath = "";
            string ShapeFilePath = "";

            file.FileString = file.FileString.Split(',')[1];
            Byte[] bytes = Convert.FromBase64String(file.FileString);

            string FilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Shapes/"), file.Codigo.ToString());
            FilePath = Path.ChangeExtension(FilePath, file.Extension);
            File.WriteAllBytes(FilePath, bytes);

            ZipPath = HttpContext.Current.Server.MapPath("~/Shapes/") + file.Codigo.ToString() + "." + file.Extension;
            ShapeFilePath = @"" + Path.Combine(HttpContext.Current.Server.MapPath("~/Shapes/"), file.Codigo.ToString());


            if (file.Extension.Equals("rar"))
            {
                using (Archive zip = new Archive())
                {
                    // Load the RAR archive
                    using (RarArchive rar = new RarArchive(ZipPath))
                    {
                        // Loop through entries of RAR file
                        for (int i = 0; i < rar.Entries.Count; i++)
                        {
                            // Copy each entry from RAR to ZIP
                            if (!rar.Entries[i].IsDirectory)
                            {
                                var ms = new MemoryStream();
                                rar.Entries[i].Extract(ms);
                                ms.Seek(0, SeekOrigin.Begin);
                                zip.CreateEntry(rar.Entries[i].Name, ms);
                            }
                            else
                                zip.CreateEntry(rar.Entries[i].Name + "/", Stream.Null);
                        }

                        rar.Dispose();
                    }
                    ZipPath = @"" + Path.Combine(HttpContext.Current.Server.MapPath("~/Shapes/"), file.Codigo.ToString()) + ".zip";
                    zip.Save(ZipPath);
                    zip.Dispose();
                    File.Delete(ZipPath.Replace("zip", "rar"));
                }
            }

            /// Essa condição vai verificar sé os dados já existe na pasta. 
            if (!Directory.Exists(ShapeFilePath))
            {
                Directory.CreateDirectory(ShapeFilePath);
                ZipFile.ExtractToDirectory(ZipPath, ShapeFilePath);
                File.Delete(ZipPath);
            }

            /// Retorna todos os dados que foram extraidos na pasta. 
            var GetName = Directory.GetFiles(ShapeFilePath);

            string NameFile = "";

            /// Esse laço de repetição pega o nome do arquivo que foi extraido na pasta temporária. 
            foreach (var item in GetName)
            {
                FileInfo f = new FileInfo(item);
                NameFile = f.Name.Split('.')[0];

                break;
            }

            /// Essa string obtém o caminho e o nome do arquivo, para que possa ser feito a leitura dos quatro arquivo gerado ("SHP","DBF","PRJ"). 
            string FileSHP = HttpContext.Current.Server.MapPath("~/Shapes/" + file.Codigo.ToString() + "/") + NameFile;
            var outGeomFactory = GeometryFactory.Default;
            var geomFactory = new GeometryFactory(new PrecisionModel(), 4326);

            ShapeDataReader ShpDataReader = new ShapeDataReader(FileSHP);
            using (var reader = ShpDataReader.ReadByMBRFilter(ShpDataReader.ShapefileBounds).GetEnumerator())
            {
                try
                {
                    while (reader.MoveNext())
                    {
                        NewFile = new FileExt();
                        NewFile.objID = Guid.NewGuid();
                        NewFile.CoordUtmConvert = false;

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


                        if (Orbita == 1)
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
                                    AuxShape.DeleteFiles(ShapeFilePath);
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
                            NewFile.GeoString = _geoConfig.GetReorientGeoString(StringPolygon);
                            NewFile.geoJson = _geoConfig.GetGeoJson(NewFile.GeoString, null);
                        }
                        else
                        {
                            for (int i = 0; i < geoDate.Coordinates.Length; i++)
                            {
                                Double X = Double.Parse(geoDate.Coordinates[i].X.ToString());
                                Double Y = Double.Parse(geoDate.Coordinates[i].Y.ToString());

                                if (i >= 0 && i < geoDate.Coordinates.Length - 1)
                                {
                                    double[] pontos = Auxiliar.ConversorCordenadas.ConvertUTMDecimal(X, Y, Orbita, 's');
                                    StringPolygon += pontos[0].ToString().Replace(',', '.') + " " + pontos[1].ToString().Replace(',', '.') + " , ";
                                }
                                else
                                {
                                    double[] pontos = Auxiliar.ConversorCordenadas.ConvertUTMDecimal(X, Y, Orbita, 's');
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

                            NewFile.GeoString = _geoConfig.GetReorientGeoString(StringPolygon);
                            NewFile.geoJson = _geoConfig.GetGeoJson(NewFile.GeoString, null);
                        }
                        PolygonList.Add(NewFile);
                    }

                }
                catch (Exception error){ }

                try{ File.Delete(ShapeFilePath + ".zip"); }catch (Exception) { }

                reader.Dispose();
                ShpDataReader.Dispose();

                AuxShape.DeleteFiles(ShapeFilePath);




                return PolygonList;
            }
        }
        public bool ExportSHPToBNG(IEnumerable<ImportShapeToBNG> obj)
        {
            string tp = "Polygon"; 
            byte[] fByteSHP;
            byte[] fByteDBF;

            string IDArea = "";
            string IDSafra = ""; 

            string FileName = "";

            var geomFactory = new GeometryFactory(new PrecisionModel(), 4326);
            WKTReader reader = new WKTReader(geomFactory);
            var features = new List<IFeature>();

            //// Aqui será realizado a leitura dos dados capturado do front. 
            foreach (var item in obj)
            {
                if (String.IsNullOrEmpty(item.coord))
                    return false; 

                IDArea = item.IDArea; 
                IDSafra = item.IDSafra;
                
                string newPolyUTM = "";

                //// Aqui será feito uma verificação se contém ou não POLYGON no GeoString. 
                //// Caso tenha os valores retornado da UTM será de um polygon. 
                if (item.coord.Contains("POLYGON"))
                {
                    item.coord = item.coord.Replace("POLYGON((", "").Replace("))", "");
                    tp = "Polygon"; 
                }
                else
                {
                    item.coord = item.coord.Replace("POINT(", "").Replace(")", "");
                    tp = "Point"; 
                }

                var UTMCoord = item.coord.Split(',');

                foreach (var utm in UTMCoord)
                {
                    string UTMString = utm.TrimStart();
                    UTMString = UTMString.TrimEnd(); 

                    double X = double.Parse(UTMString.Split(' ')[0].Replace('.', ',').ToString());
                    double Y = double.Parse(UTMString.Split(' ')[1].Trim().Replace('.', ',').ToString());

                    var UTMConvert = ConversorCoordenadas.ConvertDecimalUTM2(Y, X);
                    newPolyUTM += UTMConvert[1].ToString().Replace(',','.') + " " + UTMConvert[0].ToString().Replace(',', '.') + " , "; 
                }

                if (tp.Equals("Polygon"))
                    newPolyUTM = "POLYGON((" + newPolyUTM.Remove(newPolyUTM.Length - 2) + "))"; 
                else
                    newPolyUTM = "POINT(" + newPolyUTM.Remove(newPolyUTM.Length - 2) + ")";


                var geometry = reader.Read(newPolyUTM);
                var attr = new AttributesTable();
                string fields = item.FieldsJSON.Replace("{", " ").Replace("}", "");

                var LstField = fields.Split(',');
                foreach (var field in LstField)
                {
                    
                    var spltField = field.Split(':');
                    string label = "";
                    string valor = "";
                    try
                    {
                        Regex regex = new Regex(@"\\");
                        label = regex.Replace(spltField[0], "");
                        label = regex.Replace(label, "\"").Replace("\"", "").TrimStart();

                        valor = regex.Replace(spltField[1], "");
                        valor = regex.Replace(valor, "\"").Replace("\"", "").TrimStart();
                    }
                    catch (Exception)
                    {

                    }

                    /// Caso a label tenha mais de 10 caracteres, remova todos os caracteres igualando o total de 10 caracteres.
                    /// Detalhe: O campo Header do NetTopologySuite, só aceita 10 caracteres. 
                    if (label.Length > 10)
                    {
                        int total = label.Length;
                        total = label.Length - 10;
                        label = label.ToString().Remove(label.Length - total);
                    }

                    if (label.Equals("ID_AREA"))
                        attr.Add(label, valor);
                    else if (label.Equals("ID_SAFRA"))
                        attr.Add(label, valor);
                    else if (label.Equals("NUM_PONTO") && tp.Equals("Polygon"))
                        attr.Add(label, (_geoConfig.GetSize(item.coord) / 5));
                    else if (label.Equals("NUM_PONTO") && tp.Equals("Point"))
                        attr.Add(label, valor);
                    else if (label.Equals("AREA"))
                        attr.Add(label, _geoConfig.GetSize(item.coord));
                    else if (label.Equals("NOME"))
                        attr.Add(label, valor);
                    else if (label.Equals("TALHAO"))
                        attr.Add(label, FileName);
                    else if (label.Equals("ZONA"))
                        attr.Add(label, valor);
                    else if (label.Equals("CODIGO_ZM"))
                        attr.Add(label, valor);
                    else if (label.Equals("ZM_CODIGO"))
                        attr.Add(label, valor);
                    else if (label.Equals("ZONA_AREA"))
                        attr.Add(label, valor);
                    else if (label.Equals("TIPO"))
                        attr.Add(label, valor);
                    else if (label.Equals("SOLO"))
                        attr.Add(label, valor);
                    //else 
                    //    atr.Add(label, valor);
                }
                features.Add(new Feature(geometry, attr));
                FileName = item.FileName;
            }

            var shapeFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Shapes/"), FileName);
            if (!Directory.Exists(shapeFilePath))
                Directory.CreateDirectory(shapeFilePath);


            var prjPath = Path.Combine(shapeFilePath, $"{FileName}.prj");
            using (var streamWriter = new StreamWriter(prjPath))
            {
                streamWriter.Write(GeographicCoordinateSystem.WGS84.WKT);
                streamWriter.Close();
            }

            var shpPath = Path.Combine(shapeFilePath, FileName);
            var writer = new ShapefileDataWriter(shpPath, geomFactory);

            var outDbaseHeader = ShapefileDataWriter.GetHeader(features[0], features.Count);
            writer.Header = outDbaseHeader;
            writer.Write(features);

            string fSHP = @"" + HttpContext.Current.Server.MapPath("~/Shapes/"+ FileName + "/") + FileName + ".shp";
            fByteSHP = File.ReadAllBytes(fSHP);

            string fDBF = @"" + HttpContext.Current.Server.MapPath("~/Shapes/" + FileName + "/") + FileName + ".dbf";
            fByteDBF = File.ReadAllBytes(fDBF);

            ShapeBNG shp = new ShapeBNG();
            shp.IDArea      = IDArea;
            shp.IDSafra     = IDSafra;
            shp.nome        = FileName;
            if (tp.Equals("Polygon"))
                shp.tipo = "POLIGONO"; 
            else
                shp.tipo = "PONTO";

            shp.shp = fByteSHP;
            shp.dbf = fByteDBF;

            string FolderPath = @"" + HttpContext.Current.Server.MapPath("~/Shapes/" + FileName + "/"); 
            AuxShape.DeleteFiles(FolderPath);
            return _repository.ExportSHPToBNG(shp); 
        }
        public string GenerateUTMGeoString(List<NetTopologySuite.Geometries.Coordinate> obj)
        {
            List<double[]> NewCoord = new List<double[]>();
            foreach (var item in obj)
            {
                NewCoord.Add(ConversorCoordenadas.ConvertDecimalUTM2(item.Y, item.X));
            }

            string NewGeoCoordString = "POLYGON((";

            for (int i = 0; i < NewCoord.Count; i++)
            {
                if (i == 0)
                    NewGeoCoordString += "" + NewCoord[i][1].ToString().Replace(',', '.') + " " + NewCoord[i][0].ToString().Replace(',', '.') + ", ";
                if (i > 0 && i < (NewCoord.Count - 1))
                    NewGeoCoordString += "" + NewCoord[i][1].ToString().Replace(',', '.') + " " + NewCoord[i][0].ToString().Replace(',', '.') + ", ";
                else if (i == (NewCoord.Count - 1))
                {
                    int lng = (NewCoord.Count - 1), lat = (NewCoord.Count - 1);
                    if (NewCoord[lng][1] != NewCoord[i][1] && NewCoord[lat][0] != NewCoord[i][0])
                        NewGeoCoordString += NewCoord[i][1].ToString().Replace(',', '.') + " " + NewCoord[i][0].ToString().Replace(',', '.') + "))";
                    else
                        NewGeoCoordString = NewGeoCoordString + "" + NewCoord[0][1].ToString().Replace(',', '.') + " " + NewCoord[0][0].ToString().Replace(',', '.') + "))";
                }
            }

            return NewGeoCoordString; 
        }
        public byte[] KMLGenerate(IEnumerable<KMLShape> obj)
        {
            if (obj == null)
                return null;

            


            try
            {

                double z = 0;
                int precision = 5000;
                bool extrude = false;

                string XMLFile = "<?xml version=\"1.0\" encoding=\"UTF-8\"?> \n";
                XMLFile += "<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\">";
                XMLFile += "    <Document> \n";
                XMLFile += "        <name>POLI PV 01</name> \n";
            
                XMLFile += "            <Style id=\"estiloPoligono\">                       \n";
                XMLFile += "                <LineStyle><width> 1.5 </width></LineStyle>     \n";
                XMLFile += "                <PolyStyle>                                     \n";
                XMLFile += "                    <fill> 0 </fill>                            \n";    
                XMLFile += "                    <color>66ff0000</color>                     \n";
                XMLFile += "                </PolyStyle>                                    \n";
                XMLFile += "            </Style>                                            \n";

                XMLFile += "            <Style id=\"estiloMarcador\">                       \n";
                XMLFile += "                <IconStyle>                                     \n";
                XMLFile += "                    <scale> 0 </scale>                          \n";
                XMLFile += "                    <Icon><href>http://maps.google.com/mapfiles/kml/pal4/icon25.png</href></Icon>\n";
                XMLFile += "                </IconStyle>                                    \n";
                XMLFile += "                <LabelStyle><scale> 0.7 </scale></LabelStyle>   \n";
                XMLFile += "            </Style>                                            \n";

                XMLFile += "            <Style id=\"estiloPonto\">                          \n";
                XMLFile += "                <IconStyle>                                     \n";
                XMLFile += "                    <color> ffff0055 </color>                   \n";
                XMLFile += "                    <scale> 0.7 </scale>                        \n";
                XMLFile += "                    <Icon><href>http://maps.google.com/mapfiles/kml/shapes/placemark_circle.png</href></Icon> \n";
                XMLFile += "                </IconStyle>                                    \n";
                XMLFile += "            </Style>                                            \n";

                XMLFile += "            <Style id=\"estiloPoligono0\">                      \n";
                XMLFile += "                <LineStyle><width> 1.5 </width></LineStyle>     \n";
                XMLFile += "                <PolyStyle><fill> 0 </fill>                     \n";
                XMLFile += "                    <color>4dffffff</color>                     \n";
                XMLFile += "                </PolyStyle>                                    \n";
                XMLFile += "            </Style>                                            \n";

                XMLFile += "            <Style id=\"pontopadrao\">                          \n";
                XMLFile += "                <IconStyle><color>ffff0055</color>              \n";
                XMLFile += "                     <scale> 0.7 </scale>                       \n";
                XMLFile += "                    <Icon><href>http://maps.google.com/mapfiles/kml/shapes/placemark_circle.png</href></Icon> \n";
                XMLFile += "                </IconStyle>                                    \n";
                XMLFile += "            </Style>                                            \n";

                XMLFile += "            <Style id=\"estiloLine\">                          \n";
                XMLFile += "                <IconStyle><color>ffff0055</color>              \n";
                XMLFile += "                     <scale> 0.7 </scale>                       \n";
                XMLFile += "                    <Icon><href>http://maps.google.com/mapfiles/kml/shapes/placemark_circle.png</href></Icon> \n";
                XMLFile += "                </IconStyle>                                    \n";
                XMLFile += "            </Style>                                            \n";

                XMLFile += "            <Style id=\"padrao\"><LineStyle><width> 0.00 </width><color> ff000000 </color></LineStyle><PolyStyle><fill> 0 </fill></PolyStyle></Style>                   \n";
                XMLFile += "            <Style id=\"transRedPoly\"><LineStyle><width> 0.00 </width><color> ff000000 </color></LineStyle><PolyStyle><color> ff0000aa </color></PolyStyle></Style>    \n";
                XMLFile += "            <Style id=\"transOrangePoly\"><LineStyle><width> 0.00 </width><color> ff000000 </color></LineStyle><PolyStyle><color> ff0055aa </color></PolyStyle></Style> \n";
                XMLFile += "            <Style id=\"transYellowPoly\"><LineStyle><width> 0.00 </width><color> ff000000 </color></LineStyle><PolyStyle><color> ff00ffff </color></PolyStyle></Style> \n";
                XMLFile += "            <Style id=\"transLimePoly\"><LineStyle><width> 0.00 </width><color> ff000000 </color></LineStyle><PolyStyle><color> ff00ffaa </color></PolyStyle></Style>   \n";
                XMLFile += "            <Style id=\"transGreenPoly\"><LineStyle><width> 0.00 </width><color> ff000000 </color></LineStyle><PolyStyle><color> ff00ff00 </color></PolyStyle></Style>  \n";
                XMLFile += "            <Folder>                                                                                                    \n";

                string GenerateXMLDescription = "";

                string NameFile = ""; 

                foreach (var item in obj)
                {
                    NameFile = item.file; 

                    var center = item.centerLegend.ToList();

                    GenerateXMLDescription += "                <Placemark>                                                                                         \n";
                    if (item.tamanho == null)
                    {
                        GenerateXMLDescription += "                <name></name>                                                                                   \n";
                        GenerateXMLDescription += "                <description></description>                                                                     \n";
                    }
                    else
                    {
                        GenerateXMLDescription += "                <name>" + item.nome + "</name>                                                                 \n";
                        GenerateXMLDescription += "                <description>Área: " + item.tamanho + " Ha.</description>                                       \n";
                    }
                    GenerateXMLDescription += "                    <LookAt>                                                                                        \n";
                    GenerateXMLDescription += "                        <longitude>" + center[1].ToString().Replace(',', '.') + "</longitude>                       \n";
                    GenerateXMLDescription += "                        <latitude>"+ center[0].ToString().Replace(',', '.') + "</latitude>                          \n";
                    GenerateXMLDescription += "                        <altitude>0</altitude>                                                                      \n";
                    GenerateXMLDescription += "                        <heading>0</heading>                                                                        \n";
                    GenerateXMLDescription += "                        <tilt>0</tilt>                                                                              \n";
                    GenerateXMLDescription += "                        <range>5000</range>                                                                         \n";
                    GenerateXMLDescription += "                    </LookAt>                                                                                       \n";
                    if (item.type.Equals("Point"))
                        GenerateXMLDescription += "                    <styleUrl>#pontopadrao</styleUrl>                                                           \n";
                    else if (item.type.Equals("LineString"))
                        GenerateXMLDescription += "                    <styleUrl>#estiloLine</styleUrl>                                                        \n";
                    else
                        GenerateXMLDescription += "                    <styleUrl>#estiloPoligono</styleUrl>                                                        \n";

                    var geomFactory = new GeometryFactory(new PrecisionModel(), 4326);
                    WKTReader reader = new WKTReader(geomFactory);

                    if (item.GeoString == null)
                        return null; 
                           
                    var geometry = reader.Read(item.GeoString);

                    KMLWriter writer = new KMLWriter
                    {
                        Z = z,
                        Precision = precision,
                        Extrude = extrude
                    };

                    GenerateXMLDescription += writer.Write(geometry);

                    GenerateXMLDescription += "                </Placemark>                                                             \n";
                }

                XMLFile += "            <name>" + NameFile + "</name>                                                                   \n";
                XMLFile += "                <Folder>                                                                                    \n";
                XMLFile += "                <name>" + NameFile + "</name>                                                               \n";
                XMLFile += GenerateXMLDescription;
                XMLFile += "                </Folder>                                                                                   \n";

                XMLFile += "            </Folder>                                                                                        \n";
                XMLFile += "            <Folder>                                                                                         \n";
                XMLFile += "                <name>Legendas</name>                                                                        \n";


                foreach (var item in obj)
                {
                    string fields = item.jsonField.Replace("{", " ").Replace("}", "");
                    var LstField = fields.Split(',');

                    var center = item.centerLegend.ToList();
                    XMLFile += "                <Folder>                                                                                \n";
                    XMLFile += "                    <name>" + item.nome + "</name>                                                      \n";

                    foreach (var field in LstField)
                    {
                        var spltField = field.Split(':');
                        string label = "";
                        string valor = "";
                        try
                        {
                            Regex regex = new Regex(@"\\");
                            label = regex.Replace(spltField[0], "");
                            label = regex.Replace(label, "\"").Replace("\"", "").TrimStart();

                            valor = regex.Replace(spltField[1], "");
                            valor = regex.Replace(valor, "\"").Replace("\"", "").TrimStart();
                        }
                        catch (Exception)
                        {

                        }

                        XMLFile += "                    <Placemark>                                                                                         \n";
                        XMLFile += "                        <name>" + valor +  "</name>                                                                     \n";
                        XMLFile += "                        <description>" + label.ToUpper() + "</description>                                              \n";
                        XMLFile += "                        <LookAt>                                                                                        \n";
                        XMLFile += "                            <longitude>" + center[1].ToString().Replace(',', '.') + "</longitude>                       \n";
                        XMLFile += "                            <latitude>" + center[0].ToString().Replace(',', '.') + "</latitude>                         \n";
                        XMLFile += "                            <altitude>0</altitude>                                                                      \n";
                        XMLFile += "                            <heading>0</heading>                                                                        \n";
                        XMLFile += "                            <tilt>0</tilt>                                                                              \n";
                        XMLFile += "                            <range>5000</range>                                                                         \n";
                        XMLFile += "                        </LookAt>                                                                                       \n";
                        XMLFile += "                        <styleUrl>#estiloMarcador</styleUrl>                                                                                        \n";
                        XMLFile += "                        <Point>                                                                                                                     \n";
                        XMLFile += "                            <coordinates>" + center[1].ToString().Replace(',','.') + "," + center[0].ToString().Replace(',', '.') + "</coordinates>                                    \n";
                        XMLFile += "                        </Point>                                                                                        \n";
                        XMLFile += "                    </Placemark>                                                                                        \n";
                    }
                    XMLFile += "                </Folder>                                                                                                      \n";
                }   
                XMLFile += "            </Folder>                                                                                                              \n";
                XMLFile += "    </Document>                                                                                                                    \n";
                XMLFile += "</kml>                                                                                                                             \n";


                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(XMLFile);

                return bytes;
            }catch(Exception ex)
            {
                return null; 
            }
        }
        public IEnumerable<KMLShape> GetKMLFile(string KMLString)
        {
            KMLString = KMLString.Split(',')[1];
            byte[] XMLFile = Convert.FromBase64String(KMLString);

            string XMLPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Shapes/"), "FileXML");
            XMLPath = Path.ChangeExtension(XMLPath, "xml");
            File.WriteAllBytes(XMLPath, XMLFile);


            XDocument doc = XDocument.Load(XMLPath);
            XElement root = doc.Root;
            XNamespace ns = root.GetDefaultNamespace();

            List<XElement> simpleFields;

            List<XElement> coordinatesFile = doc.Descendants(ns + "Polygon").Descendants(ns + "coordinates").ToList();

            List<XElement> LineString = doc.Descendants(ns + "LineString").Descendants(ns + "coordinates").ToList();

            List<KMLShape> oListReturn = new List<KMLShape>();

            var file = doc.Descendants(ns + "Folder").Descendants(ns + "name").FirstOrDefault();

            bool notLegend = false;

            if (coordinatesFile.Count() > 0)
            {
                foreach (XElement extCoordinates in coordinatesFile)
                {
                    KMLShape o = new KMLShape();

                    string coordinates = ((string)extCoordinates);

                    string coordstring = "POLYGON((";

                    if (coordinates.Contains(",0 -"))
                    {
                        coordinates = coordinates.Replace(",0 -", " -");
                        var lstCoord = coordinates.Split(' ');

                        foreach (var cd in lstCoord)
                        {
                            if (!String.IsNullOrEmpty(cd))
                            {
                                if (cd.Contains(",0\n"))
                                {
                                    string item = cd.Replace(",0\n", " ");
                                    coordstring += item.TrimEnd().TrimStart().Replace(",", " ") + " , ";
                                }
                                else
                                    coordstring += cd.TrimEnd().TrimStart().Replace(",", " ") + " , ";

                            }
                        }
                        coordstring = coordstring.Replace(" 0 ,", "))");
                    }
                    else
                    {
                        var lstCoord = coordinates.Split(' ');

                        foreach (var cd in lstCoord)
                        {
                            if (!String.IsNullOrEmpty(cd))
                                coordstring += cd.TrimEnd().TrimStart().Replace(",", " ") + " , ";
                        }
                        coordstring = coordstring.Remove((coordstring.Length - 2), 2) + "))";
                    }

                    o.GeoString = coordstring;

                    try
                    {
                        o.geoJson = _geoConfig.GetGeoJson(coordstring, null);
                        bool convertLine = false; 
                        if (o.geoJson.Contains("MultiPolygon"))
                        {
                            o.geoJson = _geoConfig.GetGeoJson(coordstring, true);
                            o.GeoString = o.GeoString.Replace("POLYGON((", "LINESTRING(").Replace("))", ")");
                            convertLine = true;  
                        }

                        o.file = file.Value;
                        o.nome = file.Value;
                        o.type = convertLine == true ? "LineString" : "Polygon";
                        oListReturn.Add(o);
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
            else if (coordinatesFile.Count() == 0 && LineString.Count() == 0)
            {
                List<XElement> FilePoint = doc.Descendants(ns + "Folder").ToList();
                foreach (var fPoint in FilePoint)
                {
                    KMLShape o = new KMLShape();


                    string name = (fPoint.Descendants(ns + "name").FirstOrDefault()).Value;
                    if (!name.Contains("Legend")) notLegend = true;

                    if (notLegend == true)
                    {
                        var placemark = fPoint.Descendants(ns + "Placemark").ToList();
                        foreach (var pmk in placemark)
                        {
                            KMLShape kmlPoint = new KMLShape();
                            string p = (pmk.Descendants(ns + "Point").Descendants(ns + "coordinates").FirstOrDefault()).Value;
                            if (p.Contains(",0"))
                                kmlPoint.GeoString = "POINT(" + p.Replace(",0", "").Replace(",", " ") + ")";
                            else
                                kmlPoint.GeoString = "POINT(" + p.Replace(",", " ") + ")";

                            kmlPoint.geoJson = _geoConfig.GetGeoJson(kmlPoint.GeoString, null);
                            kmlPoint.file = file.Value;
                            kmlPoint.nome = (pmk.Descendants(ns + "name").FirstOrDefault()).Value;
                            kmlPoint.type = "Point";
                            oListReturn.Add(kmlPoint);
                        }
                        break;
                    }
                }

            }
            else if (LineString.Count() > 0)
            {
                foreach (XElement extCoordinates in LineString)
                {
                    KMLShape o = new KMLShape();
                    string coordinates = ((string)extCoordinates);
                    string coordstring = "LINESTRING(";
                    if (coordinates.Contains(",0 -"))
                    {
                        coordinates = coordinates.Replace(",0 -", " -");
                        var lstCoord = coordinates.Split(' ');

                        foreach (var cd in lstCoord)
                        {
                            if (!String.IsNullOrEmpty(cd))
                            {
                                if (cd.Contains(",0\n"))
                                {
                                    string item = cd.Replace(",0\n", " ");
                                    coordstring += item.TrimEnd().TrimStart().Replace(",", " ") + " , ";
                                }
                                else
                                    coordstring += cd.TrimEnd().TrimStart().Replace(",", " ") + " , ";

                            }
                        }
                        coordstring = coordstring.Replace(" 0 ,", ")");
                    }
                    else
                    {
                        var lstCoord = coordinates.Split(' ');

                        foreach (var cd in lstCoord)
                        {
                            if (!String.IsNullOrEmpty(cd))
                                coordstring += cd.TrimEnd().TrimStart().Replace(",", " ") + " , ";
                        }
                        coordstring = coordstring.Remove((coordstring.Length - 2), 2) + ")";
                    }
                    o.GeoString = coordstring;
                    o.geoJson = _geoConfig.GetGeoJson(coordstring, null);
                    o.file = file.Value;
                    o.nome = file.Value;
                    o.type = "LineString";

                    oListReturn.Add(o);


                }
            }

            List<XElement> fld = doc.Descendants(ns + "Folder").ToList();

            List<FieldString> fields = new List<FieldString>();
            List<string> centerLegend = new List<string>();


            bool existente = false;

            foreach (XElement l in fld)
            {
                string name = (l.Descendants(ns + "name").FirstOrDefault()).Value; 
                if (name.Contains("Legend")) existente = true;

                if (existente == true)
                {
                    var folder = l.Descendants(ns + "Folder").ToList();
                    foreach (var it in folder)
                    {
                        FieldString oFieldString = new FieldString();
                        oFieldString.Rotulo = new List<string>();
                        string field = "{";

                        string NameFolder = "";
                        int ExisteNameFolder = it.Descendants(ns + "name").ToList().Count(); 
                        if (ExisteNameFolder > 0)
                            NameFolder = it.Descendants(ns + "name").FirstOrDefault().Value;

                        var lg = it.Descendants(ns + "Placemark");

                        foreach (var legend in lg)
                        {
                            var LookAt = legend.Descendants(ns + "Point");
                            foreach (var lng_lat in LookAt)
                            {
                                var lg_lt = lng_lat.Descendants(ns + "coordinates").FirstOrDefault().Value.Replace("\n", "");

                                var lt = lg_lt.Split(','); 
                                centerLegend.Add("{\"CENTER\": [" + lt[1].ToString().TrimEnd().TrimStart() + ","+ lt[0].ToString().TrimEnd().TrimStart() + "]}");
                            }

                            if (oListReturn.Count() > folder.Count())
                            {
                                oFieldString = new FieldString();
                                field = "{";
                                oFieldString.Rotulo = new List<string>();
                            }

                            string fd = "";
                            string descricao = "";

                            var vl = legend.Descendants(ns + "description").ToList();

                            if (vl.Count() > 0)
                            {
                                if ((legend.Descendants(ns + "description").FirstOrDefault()).Value.Contains(","))
                                    descricao = (legend.Descendants(ns + "description").FirstOrDefault()).Value.Replace(",", ".");
                                else
                                    descricao = (legend.Descendants(ns + "description").FirstOrDefault()).Value;

                                descricao = descricao.Contains(":") ? descricao.Replace(":", "-") : descricao;

                                oFieldString.Rotulo.Add(descricao);
                            }
                            else
                            {
                                descricao = "DESCRIÇÃO";
                                oFieldString.Rotulo.Add("DESCRIÇÃO");

                                if (legend.Descendants(ns + "name").ToList().Count() > 0)
                                    fd = (legend.Descendants(ns + "name").FirstOrDefault()).Value;
                                else
                                    fd = NameFolder;


                                if (fd.Contains(","))
                                    fd = fd.Replace(",", ".");

                                fd = fd.Contains(":") ? fd.Replace(":", "-") : fd;
                            }


                            if (String.IsNullOrEmpty(fd))
                                field += "\"" + descricao + "\":\"" + (legend.Descendants(ns + "name").FirstOrDefault()).Value + "\" , ";
                            else
                                field += "\"" + descricao + "\":\"" + fd + "\" , ";


                            if (oListReturn.Count() > folder.Count() )
                            {
                                oFieldString.Fields = field.Remove((field.Length - 2), 2) + "}";
                                fields.Add(oFieldString);
                            }

           
                        }
                        if (oListReturn.Count() == folder.Count())
                        {
                            oFieldString.Fields = field.Remove((field.Length - 2), 2) + "}";
                            fields.Add(oFieldString);
                        }
                    }
                }
            }

            centerLegend = centerLegend.Distinct().ToList(); 

            for (int i = 0; i < fields.Count(); i++)
            {
                try
                {
                    oListReturn[i].jsonField = fields[i].Fields;
                    oListReturn[i].Rotulo = fields[i].Rotulo;
                    oListReturn[i].center = centerLegend[i];
                }
                catch(Exception ex)
                {

                }
            }

            File.Delete(XMLPath);
            return oListReturn; 
        }



        // Função para verificar se um polígono está dentro de outro com NetTopologySuite
        private bool IsInsideWithNetTopology(string geoString1, string geoString2)
        {
            var reader = new WKTReader();
            var geom1 = reader.Read(geoString1);
            var geom2 = reader.Read(geoString2);
            var ntsGeom1 = GeometryFactory.Default.CreateGeometry(geom1);
            var ntsGeom2 = GeometryFactory.Default.CreateGeometry(geom2);

            var isInside = ntsGeom1.Within(ntsGeom2);

            return isInside;
        }
    }
}
