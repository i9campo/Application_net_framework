using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
namespace Sigma.Domain.ViewTables
{
    public class GeoView
    {
        public DbGeography Geo { get; set; }
    }
    public class GeoDate
    {
        public int ID { get; set; }
        public DbGeography Geo { get; set; }
        public double Tamanho { get; set;  }
        public string GeoString { get; set; }
        public string GeoJson { get; set; }
    }
    public class Polygon
    {
        public Guid objID { get; set; }
        public Guid? ID { get; set; }
        public string Nome { get; set; }
        public string Geo { get; set; }
        public string GeoString { get; set; }
        public string GeoJson { get; set; }
        public string Field { get; set; }
        public List<string> Rotulo { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public double Tamanho { get; set; }
    }
    public class PolyString
    {
        public string GeoJson { get; set; }
        public string GeoString { get; set; }
    }
    public class LineToPolygon
    {
        public string LineString { get; set; }
        public string PolygonString { get; set; }
        public Guid? IDSelect { get; set; }
    }
    public class LastFirstPoint
    {
        public string linestring { get; set; }
        public string FirstPoint { get; set; }
        public string LastPoint { get; set; }
    }
    public class LineSplitZones
    {
        public string LineStringPoly1 { get; set; }
        public string LineStringPoly2 { get; set; }

    }
    public class SplitPolygon
    {
        public IEnumerable<string> LineStringPoly1 { get; set; }
        public string LineStringPoly2 { get; set; }
    }
}