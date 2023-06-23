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
    public class AreaServicoController : ApiController
    {
        private readonly IAreaServicoAppService _areaservicoappservice;
        private readonly IGeoConfigurationAppService _geoAppService;
        private readonly IGridAppService _gridAppService;
        public AreaServicoController(IAreaServicoAppService areaservicoappservice, IGeoConfigurationAppService geoappservice, IGridAppService gridAppService)
        {
            _areaservicoappservice = areaservicoappservice;
            _geoAppService = geoappservice;
            _gridAppService = gridAppService;
        }

        [HttpGet]
        [ActionName("find")]
        [Route("api/areaservico/find")]
        public AreaServicoViewer FindAreaServico(Guid objID)
        {
            return _areaservicoappservice.FindAreaServico(objID); 
        }



        [HttpGet]
        [ActionName("findfullareaservico")]
        [Route("api/areaservico/findfullareaservico")]
        public AreaServicoView FindFullAreaServico(String objID, Guid? IDArea, Guid? IDSafra, bool? returngeo)
        {
            Guid ID = Guid.Parse(objID.Replace("'","")); 
            //Este método será utilizado para retornar uma área serviço contendo informações de outras tabelas, 
            //Como: Safra, Serviço, Área, Propriedade e Proprietário. 

            // Essa consulta contém quatro parâmetro, esses parâmetros podem ser todos nulos. 
            // Para o seguinte objetivo, sé o usuário declarar a requisiçaõ com o objeto ID null, significa que a consulta será realizada a partir da área e safra ou área ou safra. 
            // já o returngeo será utilizado para visualização do geo se for true apresenta as áreas serviço que contém geo, se for false apresenta todas ás áreas serviço
            // que contém ou não contém geo.
            return _areaservicoappservice.FindFullAreaServico(ID, IDArea, IDSafra, returngeo);
        }
        
        [HttpGet]
        [ActionName("findFilter")]
        [Route("api/areaservico/finfilter")]
        public AreaServicoView FindFilter(Guid? IDSafra, Guid? IDProprietario, Guid? IDPropriedade, Guid? IDArea)
        {
            return _areaservicoappservice.FindFilter(IDSafra, IDProprietario, IDPropriedade, IDArea);
        }

        [HttpPut]
        [ActionName("findarea")]
        [Route("api/areaservico/findarea/{coord}")]
        public string FindArea(string coord)
        {
            return _geoAppService.GetSize(coord).ToString(); 
        }

        [HttpGet]
        [ActionName("getlstareaservico")]
        [Route("api/areaservico/getlstareaservico")]
        public IEnumerable<AreaServicoView> GetAreaServico(Guid? IDAreaServico, Guid IDSafra, Guid? IDArea, Guid? IDPropriedade, Guid? IDServico, bool? returngeo, int returnquery)
        {
            // Este método será utilizado de forma dinâmica, para retornar uma lista de área serviço. 
            // Essa consulta contém seis parâmetros, esses parâmetros tem como objetivo retornar a consulta de acordo com a apresentação. 
            // returngeo ele pode retornar uma lista de área serviço que contém geo, se for true ou pode retornar uma lista de área serviço com ou sem geo se for false. 

            // Já o returnquery, será utilizado para carregar o tipo de consulta, sé for utilizado para capturar os valores do Grid onde são especificados a existencia de geo 
            // a consulta é 1 agora sé for 0 retorna somente a área serviço. 


            return _areaservicoappservice.GetAreaServico(IDAreaServico, IDSafra, IDArea, IDPropriedade, IDServico, returngeo, returnquery);
        }

        // GET api/<controller>
        public IEnumerable<AreaServico> Get()
        {
            return _areaservicoappservice.GetAll();
        }

        // GET api/<controller>/5
        public AreaServico Get(Guid objID)
        {
            return _areaservicoappservice.Find(objID);
        }

        [HttpGet]
        [ActionName("getmaxnumbservicebyareaservio")]
        [Route("api/areaservico/getmaxnumbservicebyareaservio")]
        public IEnumerable<AreaServicoView> GetMaxNumberServiceByAreaServico(Guid IDSafra, Guid IDArea)
        {
            return _areaservicoappservice.GetMaxServicoRegister(IDSafra, IDArea);
        }

        [HttpPost]
        [ActionName("registerservico")]
        [Route("api/areaservico/registerservico")]
        public ValidationResult RegisterServico([FromBody] AreaServico obj)
        {
            var objID = Guid.NewGuid();
            obj.objID = objID;
            return _areaservicoappservice.Add(obj);
        }

        // POST api/<controller>
        public ValidationResult Post([FromBody] AreaServico obj)
        {
            return _areaservicoappservice.Add(obj);
        }

        public ValidationResult Put(Guid objID, [FromBody] AreaServico obj)
        {
            return _areaservicoappservice.Update(obj); 
        }

        [HttpPut]
        [ActionName("UpdateGeo")]
        [Route("api/areaservico/updategeo/{objID}")]
        public bool UpdateGeo(Guid objID, [FromBody]AreaServicoGeo geo)
        {
            return _areaservicoappservice.UpdateGeo(objID, geo.coord, geo.jsonField, geo.tamanho); 
        }

        [HttpPut]
        [ActionName("RemoveGeo")]
        [Route("api/areaservico/RemoveGeo")]
        public ValidationResult RemoveGeo(Guid objID)
        {
            AreaServico obj = _areaservicoappservice.Find(Guid.Parse(objID.ToString()));
            return _areaservicoappservice.Update(obj);
        }

        [HttpGet]
        [ActionName("deletearea")]
        [Route("api/areaservico/deletearea")]
        public bool DeleteAllAreaServico(String objID)
        {
            return _areaservicoappservice.DeleteAllAreaServico(objID);
        }


        // DELETE api/<controller>/5
        public ValidationResult Delete(Guid objID)
        {
            AreaServico obj = _areaservicoappservice.Find(objID);
            return _areaservicoappservice.Remove(obj);
        }
    }
}