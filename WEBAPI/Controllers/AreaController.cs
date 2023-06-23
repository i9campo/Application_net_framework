using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class AreaController : ApiController
    {
        private readonly IAreaAppService _areaAppService;
        private readonly IPropriedadeAppService _propriedadeService;
        private readonly IGeoConfigurationAppService _geoConfiguration; 
        public AreaController(IAreaAppService areaAppService, IPropriedadeAppService propriedadeAppService, IGeoConfigurationAppService geoConfiguration)
        {
            _areaAppService = areaAppService;
            _propriedadeService = propriedadeAppService;
            _geoConfiguration = geoConfiguration; 
        }

        /// <type>HttpGet</type>
        /// <link>'/area/'</link>
        public IEnumerable<Area> Get()
        {
            return _areaAppService.GetAll();
        }

        [HttpGet]
        [ActionName("getarea")]
        [Route("api/area/getarea")]
        public AreaView GetFullArea(Guid objID)
        {
            return _areaAppService.GetFullArea(objID);
        }

        [HttpGet]
        [ActionName("Find")]
        [Route("api/area/Find")]
        public Area Find(Guid objID)
        {
            return _areaAppService.Find(objID); 
        }

        [HttpGet]
        [ActionName("getbypropriedade")]
        [Route("api/area/getbypropriedade")]
        public IEnumerable<Area> GetByPropriedade(Guid IDPropriedade)
        {
            return _areaAppService.GetByPropriedade(IDPropriedade);
        }

        [HttpGet]
        [ActionName("getareaexistedgrid")]
        [Route("api/area/getareaexistedgrid")]
        public IEnumerable<AreaGrid> GetAreaExistedGrid(Guid IDSafra, Guid IDPropriedade)
        {
            return _areaAppService.GetAllAreaExistedGrid(IDSafra, IDPropriedade); 
        }

        [HttpGet]
        [ActionName("VerifyAreaServico")]
        [Route("api/area/verifyareaservico")]
        public IEnumerable<AreaView> VerifyAreaServico(Guid IDPropriedade)
        {
            return _areaAppService.VerifyAreaServico(IDPropriedade);
        }


        [HttpGet]
        [ActionName("FindFullArea")]
        [Route("api/area/FindFullArea")]
        public IEnumerable<AreaView> FindFullAreaByPropriedade(Guid IDPropriedade)
        {
            return _areaAppService.FindFullAreaByPropriedade(IDPropriedade); 
        }


        [HttpGet]
        [ActionName("GetAreaPropriedade")]
        [Route("api/area/GetAreaPropriedade")]
        public IEnumerable<AreaPropriedadeView> GetAreaPropriedade(Guid Area)
        {
            return _areaAppService.GetAreaPropriedade(Area);
        }


        [HttpGet]
        [ActionName("GetAreaBNGByPropriedade")]
        [Route("api/area/GetAreaBNGByPropriedade")]
        public IEnumerable<BNGAreaView> GetAreaBNGByPropriedade(Guid IDSafra,  Guid IDPropriedade)
        {
            return _areaAppService.GetAreaBNGByPropriedade(IDSafra, IDPropriedade); 

        }


        [HttpGet]
        [ActionName("getbypropriedadesafra")]
        [Route("api/area/getbypropriedadesafra")]
        public IEnumerable<Area> GetByPropriedadeSafra(Guid IDSafra, Guid IDPropriedade)
        {
            List<Area> lst = null;

            try
            {
                lst = _areaAppService.GetByPropriedadeSafra(IDPropriedade, IDSafra).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            return lst;
        }

        /// <param name="obj"></param>
        /// <type>HttpPost</type>
        /// <link>'/area/', obj </link>
        public ValidationResult Post([FromBody] Area obj)
        {
            obj.objID = Guid.NewGuid();
            return _areaAppService.Add(obj);
        }

        /// <param name="objID"></param>
        /// <param name="obj"></param>
        /// <type>HttpPut</type>
        /// <link>'/area/' + objID, obj</link>
        public ValidationResult Put(string objID, [FromBody] Area obj)
        {
            Area item = new Area();
            item = _areaAppService.Find(obj.objID);
            item.nome = obj.nome; 


            return _areaAppService.Update(item);
        }

        /// <summary>
        /// Método utilizado para atualizar o geo da área. 
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="obj"></param>
        [HttpPut]
        [ActionName("PutGeoArea")]
        [Route("api/area/PutGeoArea/{objID}")]
        public ValidationResult PutGeoArea(Guid objID, [FromBody]Area_Viewer obj)
        {
            Area oArea = _areaAppService.Find(objID);
            oArea.area_geo = _geoConfiguration.GetGeoPolygon(obj.GeoString); 
            return _areaAppService.Update(oArea); 
        }

        /// <param name="objID"></param>
        /// <type>HttpDelete</type>
        /// <link>'/area/ + objID</link>
        public ValidationResult Delete(string objID)
        {
            Area obj = _areaAppService.Find(Guid.Parse(objID));
            return _areaAppService.Remove(obj);
        }
    }
}