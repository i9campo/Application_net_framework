using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Sigma.App.AppService
{
    public class GeoConfigurationAppService : AppService<GeoView>, IGeoConfigurationAppService
    {
        private readonly IGeoConfigService _Service;
        public GeoConfigurationAppService(IGeoConfigService service)
            :base(service)
        {
            _Service = service; 
        }

        public IEnumerable<string> SplitMultiLine(string LineString)
        {
            return null; 
        }

        public DbGeography GetGeoPoint(string geoJson)
        {
            return _Service.GetGeoPoint(geoJson); 
        }

        public DbGeography GetGeoPolygon(string geoJson)
        {
            return _Service.GetGeoPolygon(geoJson); 
        }

        public double GetSize(string coord)
        {
            return _Service.GetSize(coord); 
        }

        public IEnumerable<Polygon> SplitPolygon(string LineString, string PolygonString, Guid? IDSelect)
        {
            return _Service.SplitPolygon(LineString, PolygonString, IDSelect); 
        }

        public IEnumerable<string> SelectedUnionLines(string Polygon, string MultLine, Guid? IDSelected)
        {
            return _Service.SelectedUnionLines(Polygon, MultLine, IDSelected); 
        }

        public LastFirstPoint GetFirstLastPoint(string coord)
        {
            return _Service.GetFirstLastPoint(coord); 
        }

        public PolyString SplitZones(LineSplitZones obj)
        {
            return _Service.SplitZones(obj); 
        }

        public IEnumerable<string> SplitWitinPoly(SplitPolygon obj)
        {
            return _Service.SplitWitinPoly(obj); 
        }

        public IEnumerable<string> GetSplitWithinPolygon(SplitPolygon obj)
        {
            return _Service.GetSplitWithinPolygon(obj); 
        }

        public IEnumerable<string> GetSplitPoly(SplitPolygon obj)
        {
            return _Service.GetSplitPoly(obj); 
        }

        public IEnumerable<string> GenerateZones(SplitPolygon obj)
        {
            return _Service.GenerateZones(obj); 
        }

        public string GetGeoPointJSON(string strQUERY)
        {
            return _Service.GetGeoPointJSON(strQUERY);
        }

        public string GetGeoJson(string stringQUERY)
        {
            return _Service.GetGeoJson(stringQUERY); 
        }

        public string GetGeoCenter(string strQUERY)
        {
            return _Service.GetGeoCenter(strQUERY);
        }

        public List<string> GenerateGeoJsonPoints(List<string> lstpoly)
        {
            return _Service.GenerateGeoJsonPoints(lstpoly); 
        }

        public bool GetWithinPoint(string point, string poly)
        {
            return _Service.GetWithinPoint(point, poly);
        }

        public IEnumerable<LstPontos> SplitPoly(string poly)
        {
            return _Service.SplitPoly(poly); 
        }

        public bool CheckedPoly(string points_poly)
        {
            return _Service.CheckedPoly(points_poly); 
        }

        public IEnumerable<string> OrdePoint(List<string> lstPoint)
        {
            return _Service.OrdePoint(lstPoint); 
        }

        public IEnumerable<string> OrderPoly(List<string> lstPoly)
        {
            return _Service.OrderPoly(lstPoly); 
        }
    }
}
