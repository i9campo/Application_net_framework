using Sigma.App.Interfaces;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI.Controllers
{
    [AllowedOriginFilter]
    public class GeoConfigurationController : ApiController
    {
        private readonly IGeoConfigurationAppService _geoConfigAppService;

        public GeoConfigurationController(IGeoConfigurationAppService geoConfigAppService)
        {
            _geoConfigAppService = geoConfigAppService;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(string coord)
        {
            return _geoConfigAppService.GetSize(coord).ToString();
        }

        [HttpPut]
        [ActionName("GetFirstLastPoint")]
        [Route("api/GeoConfiguration/GetFirstLastPoint/{linestring}")]
        public LastFirstPoint GetFirstLastPoint(string linestring, LastFirstPoint lfp)
        {
            return _geoConfigAppService.GetFirstLastPoint(lfp.linestring); 
        }


        [HttpPut]
        [ActionName("post")]
        [Route("api/GeoConfiguration/post")]
        public string Post(string coord)
        {
            return "";
        }

        [HttpPut]
        [ActionName("SplitPolygon")]
        [Route("api/GeoConfiguration/SplitPolygon/{linestring}")]
        public IEnumerable<Polygon> SplitPolygon(string linestring, LineToPolygon obj)
        {
            try
            {
                IEnumerable<Polygon> lstPolygon = _geoConfigAppService.SplitPolygon(obj.LineString, obj.PolygonString, obj.IDSelect);
                int cont = 1;
                foreach (var item in lstPolygon)
                {
                    item.Rotulo = new List<string>() { "Nome", "Tamanho" };
                    item.Nome = "AREA - " + cont;
                    item.Field = "{'Nome':'" + item.Nome + "','Tamanho':'" + item.Tamanho + "'}";
                    cont++;
                }

                return lstPolygon;

            }
            catch (Exception ex)
            {
                IEnumerable<Polygon> lstPolygon = new List<Polygon>();
                return lstPolygon; 
            }
        }

        [HttpPost]
        [ActionName("UnionSelectedLine")]
        [Route("api/GeoConfiguration/UnionSelectedLine")]
        public IEnumerable<string> UnionSelectedLine(LineToPolygon obj)
        {
            return _geoConfigAppService.SelectedUnionLines(obj.PolygonString, obj.LineString, Guid.NewGuid()); 
        }

        [HttpPost]
        [ActionName("SplitZones")]
        [Route("api/GeoConfiguration/SplitZones")]
        public PolyString SplitZones(LineSplitZones obj)
        {
            return _geoConfigAppService.SplitZones(obj); 
        }

        [HttpPost]
        [ActionName("SplitWithinPoly")]
        [Route("api/GeoConfiguration/SplitWithinPoly")]
        public IEnumerable<string> SplitWitinPoly(SplitPolygon obj)
        {
            return _geoConfigAppService.SplitWitinPoly(obj);
        }

        [HttpPost]
        [ActionName("GenerateZones")]
        [Route("api/GeoConfiguration/GenerateZones")]
        public IEnumerable<string> GenerateZones(SplitPolygon obj)
        {
            return _geoConfigAppService.GenerateZones(obj);
        }


        [HttpPost]
        [ActionName("GetSplitWithinPolygon")]
        [Route("api/GeoConfiguration/GetSplitWithinPolygon")]
        public IEnumerable<string> GetSplitWithinPolygon(SplitPolygon obj)
        {
            return _geoConfigAppService.GetSplitWithinPolygon(obj); 
        }

        [HttpPost]
        [ActionName("GetSplitPoly")]
        [Route("api/GeoConfiguration/GetSplitPoly")]
        public IEnumerable<string> GetSplitPoly(SplitPolygon obj)
        {
            return _geoConfigAppService.GetSplitPoly(obj); 
        }
    }
}   