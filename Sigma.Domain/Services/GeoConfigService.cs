using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace Sigma.Domain.Services
{
    public class GeoConfigService : Service<GeoView>, IGeoConfigService
    {
        private readonly IGeoConfigRepository _repository;

        public GeoConfigService(IGeoConfigRepository repository)
            :base(repository)
        {
            _repository = repository; 
        }

        public bool CheckedPoly(string points_poly)
        {
            return _repository.CheckedPoly(points_poly); 
        }

        public List<string> GenerateGeoJsonPoints(List<string> lstpoly)
        {
            return _repository.GenerateGeoJsonPoints(lstpoly); 
        }

        public IEnumerable<string> GenerateZones(SplitPolygon obj)
        {
            LineSplitZones geo = new LineSplitZones();
            geo.LineStringPoly2 = obj.LineStringPoly2;

            List<string> result = new List<string>();
            foreach (string item in obj.LineStringPoly1)
            {
                geo.LineStringPoly1 = item;
                var gj = _repository.GenerateZones(geo); 
                if (gj != null)
                    result.AddRange(gj);

                else if (gj  == null)
                {

                }

            }

            foreach (string item in obj.LineStringPoly1)
            {
                geo.LineStringPoly1 = item;
                var gj = _repository.GenerateZonesInternal(geo); 
                if (gj != null)
                    result.AddRange(gj);
            }

            result = result.Select(x => x).Distinct().Where(x => x != null && !x.Contains("MultiPolygon")).ToList(); 
            return result; 
        }

        public LastFirstPoint GetFirstLastPoint(string coord)
        {
            return _repository.GetFirstLastPoint(coord); 
        }

        public string GetGeoCenter(string strQUERY)
        {
            return _repository.GetGeoCenter(strQUERY); 
        }

        public string GetGeoJson(string stringQUERY)
        {
            return _repository.GetGeoJson(stringQUERY); 
        }

        public DbGeography GetGeoPoint(string geoJson)
        {
            return _repository.GetGeoPoint(geoJson); 
        }

        public string GetGeoPointJSON(string strQUERY)
        {
            return _repository.GetGeoPointJSON(strQUERY);
        }

        public DbGeography GetGeoPolygon(string geoJson)
        {
            return _repository.GetGeoPolygon(geoJson); 
        }

        public double GetSize(string coord)
        {
            return _repository.GetSize(coord); 
        }

        public IEnumerable<string> GetSplitPoly(SplitPolygon obj)
        {
            LineSplitZones geo = new LineSplitZones();
            geo.LineStringPoly2 = obj.LineStringPoly2;

            List<string> result = new List<string>();
            foreach (string item in obj.LineStringPoly1)
            {
                geo.LineStringPoly1 = item;
                result.Add(_repository.GetSplitPoly(geo));
            }
            return result;
        }

        public IEnumerable<string> GetSplitWithinPolygon(SplitPolygon obj)
        {
            LineSplitZones geo = new LineSplitZones();
            geo.LineStringPoly2 = obj.LineStringPoly2;

            List<string> result = new List<string>();
            foreach (string item in obj.LineStringPoly1)
            {
                geo.LineStringPoly1 = item;
                if (_repository.GetSplitWithinPolygon(geo) != null)
                    result.Add(_repository.GetSplitWithinPolygon(geo));
            }
            return result;
        }

        public bool GetWithinPoint(string point, string poly)
        {
            return _repository.GetWithinPoint(point, poly); 
        }

        public IEnumerable<string> OrdePoint(List<string> lstPoint)
        {
            return _repository.OrdePoint(lstPoint); 
        }

        public IEnumerable<string> OrderPoly(List<string> lstPoly)
        {
            return _repository.OrderPoly(lstPoly); 
        }

        public IEnumerable<string> SelectedUnionLines(string Polygon, string MultLine, Guid? IDSelected)
        {
            return _repository.SelectedUnionLines(Polygon, MultLine, IDSelected);
        }

        public IEnumerable<LstPontos> SplitPoly(string poly)
        {
            return _repository.SplitPoly(poly); 
        }

        public IEnumerable<Polygon> SplitPolygon(string LineString, string PolygonString, Guid? IDSelect )
        {
            return _repository.SplitPolygon(LineString, PolygonString, IDSelect); 
        }

        public IEnumerable<string> SplitWitinPoly(SplitPolygon obj)
        {
            LineSplitZones geo = new LineSplitZones();
            geo.LineStringPoly2 = obj.LineStringPoly2;

            List<string> result = new List<string>();  
            foreach (string item in obj.LineStringPoly1)
            {
                geo.LineStringPoly1 = item;
                result.AddRange(_repository.SplitWitinPoly(geo)); 
            }

            return result; 
        }

        public PolyString SplitZones(LineSplitZones obj)
        {
            return _repository.SplitZones(obj); 
        }
    }
}
