using System;
using System.Collections.Generic;
namespace Sigma.Domain.ViewTables
{
    public class Shape
    {
        public Shape()
        {
            objID = Guid.NewGuid();
        }
        public Guid? ID { get; set; }
        public Guid objID { get; set; }
        public string nome { get; set; }
        public string type { get; set; }
        public string descricao { get; set; }
        public string Color { get; set; }
        public int? LastUID { get; set; }
        public string MessageErro { get; set; }
        public string geoJson { get; set; }
        public string geoString { get; set; }
        public string PolygonGeography { get; set; }
        public string geo { get; set; }
        public string Field { get; set; }
        public List<string> Rotulo { get; set; }
        public bool CoordUTM { get; set; }
    }
    public class ShapeFile
    {
        public string data { get; set; }
        public string fileName { get; set; }
        public string extension { get; set; }
    }


    public class ImportShapeToBNG
    {
        public string IDSafra { get; set; }
        public string IDArea { get; set; }
        public string FileName { get; set; }
        public string nome { get; set; }
        public string coord { get; set; }
        public string FieldsJSON { get; set; }
    }

    public class ShapeBNG
    {
        public string objID { get; set; }     
        public string IDPropriedade { get; set; }
        public string IDArea { get; set; }
        public string IDSafra { get; set; }
        public string nome { get; set; }
        public string zona { get; set; }
        public string tipo { get; set; }
        public byte[] shp { get; set; }
        public byte[] dbf { get; set; }
        public int ponto0020 { get; set; }
        public int ponto2040 { get; set; }
        public int amostrasimples { get; set; }
    }


    public class KMLShape
    {
        public string GeoString { get; set; }
        public string jsonField { get; set; }
        public string geoJson   { get; set; }
        public IEnumerable<string> Rotulo { get; set; }
        public IEnumerable<float> centerLegend { get; set; }
        public string center { get; set; }
        public string nome { get; set; }
        public string file { get; set; }
        public string tamanho { get; set; }
        public string type { get; set; }


    }


    public class FieldString
    {
        public List<string> Rotulo { get; set; }
        public string Fields { get; set; }
    }

}
