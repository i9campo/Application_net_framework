using AutoMapper;
using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPIhttps
{
    [AllowedOriginFilter]
    public class GridController : ApiController
    {
        private readonly IGridAppService _gridAppService;
        private readonly IAnaliseSoloAppService _analiseSoloAppService;
        private readonly ICorretivoAppService _corretivoAppService;
        private readonly IAreaServicoAppService _areaservicoAppService;
        private readonly IAreaAppService _areaAppService;
        private readonly IGeoConfigurationAppService _geoAppService;

        public GridController(IGeoConfigurationAppService geoAppService, IGridAppService gridAppService, IAnaliseSoloAppService analiseSoloAppService, ICorretivoAppService corretivoAppService, IAreaServicoAppService areaservicoappservice, IAreaAppService areaappservice)
        {
            _analiseSoloAppService = analiseSoloAppService;
            _corretivoAppService = corretivoAppService;
            _gridAppService = gridAppService;
            _areaservicoAppService = areaservicoappservice;
            _areaAppService = areaappservice;
            _geoAppService = geoAppService;
        }

        [HttpPut]
        [ActionName("decode")]
        [Route("api/grid/decode/{Type}")]
        public String[] Decode(String Type, teste obj)
        {
            Double number;
            obj.text = obj.text.Split(',')[1];
            string[] array = new string[] { obj.text };
            List<string> ArrayFinal = new List<string>();
            string converted = obj.text.Replace('-', '+');
            converted = converted.Replace('_', '/');
            byte[] data = Convert.FromBase64String(converted);
            string decodedString = System.Text.Encoding.UTF8.GetString(data);
            if (Type == "GPX")
            {
                ArrayFinal.Add(decodedString);
                array = ArrayFinal.ToArray();
                return array;
            }
            if (Type == "KML")
            {
                ArrayFinal.Add(decodedString);
                array = ArrayFinal.ToArray();
                return array;
            }
            return array;

        }

        #region SEARCH
        public IEnumerable<Grid> Get()
        {
            return _gridAppService.GetAll().ToList().GetRange(0, 50).ToList();
        }

        public GridViewer FindGrid(Guid objID)
        {
            return _gridAppService.FindGrid(objID); 
        }
        #endregion

        [Route("api/grid/byareaservico")]
        public IEnumerable<GridViewer> Get(Guid IDAreaServico)
        {
            return _gridAppService.GetByAreaServico(IDAreaServico);
        }

        [HttpGet]
        [ActionName("getbyareaservicofull")]
        [Route("api/grid/getbyareaservicofull")]
        public IEnumerable<GridView> GetByAreaServicoFull(Guid IDAreaServico)
        {
            return _gridAppService.GetByAreaServicoFull(IDAreaServico);
        }

        [HttpGet]
        [ActionName("getgrid")]
        [Route("api/grid/getgrid")]
        public IEnumerable<GridView> GetGrid(Guid objID, Guid IDSafra, Guid? IDAreaServico, int Type)
        {
            return _gridAppService.GetGrid(objID, IDSafra, IDAreaServico, Type);
        }

        [HttpGet]
        [ActionName("CorrecaoAcidez")]
        [Route("api/grid/CorrecaoAcidez/")]
        public IEnumerable<GridView> CorrecaoAcidez(Guid IDAreaServico)
        {
            return _gridAppService.CorrecaoAcidez(IDAreaServico);
        }

        [HttpPut]
        [ActionName("updatelstgrid")]
        [Route("api/grid/updatelstgrid")]
        public bool UpdateObjectGrid(IEnumerable<GridViewer> obj)
        {
            return _gridAppService.UpdateLstGrid(obj);
        }

        public bool Put(string objID, [FromBody] GridViewer obj)
        {
            return _gridAppService.UpdateGrid(obj);
        }

        [HttpPost]
        [ActionName("postgrid")]
        [Route("api/grid/postgrid")]
        public bool PostGrid(IEnumerable<GridViewer> obj)
        {
            return _gridAppService.AddLstGrid(obj); 
        }

        [HttpPost]
        [ActionName("split_poly")]
        [Route("api/grid/split_poly")]
        public async Task<IEnumerable<GeoJsonSplitPoly>> SplitPoly(SplitPolyViewer obj)
        {
            return await _gridAppService.SplitPoly(obj); 
        }

        public ValidationResult Post([FromBody] GridView obj)
        {
            //WGS84 = 4326. 
            Random rnd = new Random();
            Grid objeto = Mapper.Map<GridView, Grid>(obj);
            DbGeography multipoint = DbGeography.MultiPointFromText(obj.geoJson.ToString(), 4326);
            DbGeometry temp_multipoint = DbGeometry.MultiPointFromBinary(multipoint.AsBinary(), 4326);
            objeto.geo = DbGeography.PolygonFromBinary(temp_multipoint.ConvexHull.AsBinary(), 4326);
            objeto.codigo = rnd.Next(50000, 100000);
            return _gridAppService.Add(objeto);
        }

        // DELETE api/grid/5
        public ValidationResult Delete(Guid objID)
        {
            //IEnumerable<AnaliseSoloView> AnaliseSolo = _analiseSoloAppService.GetAnaliseSoloByAreaServico(objID, "POINTS");
            //foreach (var itemANS in AnaliseSolo)
            //{
            //    AnaliseSolo analise = _analiseSoloAppService.Find(Guid.Parse(itemANS.objID.ToString()));
            //    _analiseSoloAppService.Remove(analise);
            //}

            IEnumerable<Grid> grid = _gridAppService.GetGridByAreaServico(objID);

            foreach (Grid iGrid in grid)
            {
                IEnumerable<Corretivo> Corretivo = _corretivoAppService.GetAll().Where(o => o.IDGrid.Equals(iGrid.objID)).ToList(); 
                foreach (var itemCorretivo in Corretivo)
                {
                    _corretivoAppService.Remove(itemCorretivo);
                }

                //Grid obj = _gridAppService.Find(iGrid.objID);
                _gridAppService.Remove(iGrid); 
            }

            return null;
        }

        [HttpGet]
        [ActionName("deletegrid")]
        [Route("api/grid/deletegrid")]
        public bool DeleteGrid(String IDAreaServico)
        {
            return _gridAppService.DeleteGrid(IDAreaServico);
        }
    }
}