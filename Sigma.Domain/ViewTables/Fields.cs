using System;
using System.Collections.Generic;

namespace Sigma.Domain.ViewTables
{
    public class Fields
    {
        public Fields()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid? ID { get; set; }
        public string nome { get; set; }
        public string type { get; set; }
        public string descricao { get; set; }
        public string Color { get; set; }
        public int? LastUID { get; set; }
        public string MessageErro { get; set; }
        public string geoJson { get; set; }
        public string PolygonGeography { get; set; }
        public string geo { get; set; }
        public string Field { get; set; }
        public List<string> Rotulo { get; set; }
        public bool? CoordUTM { get; set; }
        public string Path { get; set; }
        public string FilePath { get; set; }
        public int ponto { get; set; }
        public string subSequenciaA { get; set; }
        public bool subAmostra { get; set; }
        public string data { get; set; }
        public string fileName { get; set; }
        public string extension { get; set; }
    }
    public class GeoCoordText
    {
        public string coord { get; set; }
        public string jsonField { get; set; }
        public string FileName { get; set; }
        public string nome { get; set; }
        public IEnumerable<float> centerLegend { get; set; }
    }
    public class GeoCoordinates
    {
        public IEnumerable<ListCoord> coord { get; set; }
    }
    public class ListCoord
    {
        public IEnumerable<Coordenadas> Coordenadas { get; set; }
    }
    public class Coordenadas
    {
        public decimal x { get; set; }
        public decimal y { get; set; }
    }
    public class GpxShp
    {
        /// <summary>
        /// Recebe o valor do arquivo GPX ou KML em texto. 
        /// </summary>
        public string fileText { get; set; }
        public string pointCoordText { get; set; }
    }

    /// <summary>
    /// Classe utilizada para carregar os dados dos arquivos em base 64. 
    /// </summary>
    public class File64
    {
        public string Type { get; set;  }
        public string FileString { get; set; }
        public string Extension { get; set; }
        public Guid Codigo { get; set; }
    }
    public class FileExt
    {
        public Guid objID { get; set; }
        public string centerLegend { get; set; }
        public string Nome { get; set; }
        public string GeoString { get; set; }
        public string geoJson { get; set; }
        public string jsonField { get; set; }
        public List<string> Rotulo { get; set; }
        public string type { get; set; }
        public bool CoordUtmConvert { get; set; }

    }
    public class BNG_Shp_File_Date
    {
        public string objID { get; set; }
        public string IDPropriedadeRural { get; set; }
        public string IDArea { get; set; }
        public string IDSafra { get; set; }
        public string nome { get; set; }
        public int? zona { get; set; }
        public string tipo { get; set; }
        public byte[] shp { get; set; }
        public byte[] dbf { get; set; }
        public int? ponto0020 { get; set; }
        public int? ponto2040 { get; set; }
        public int? amostrasimples { get; set; } 
    }
}