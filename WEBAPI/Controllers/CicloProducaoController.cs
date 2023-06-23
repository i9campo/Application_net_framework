using AutoMapper;
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
    public class CicloProducaoController : ApiController
    {
        private readonly ICicloProducaoAppService _cicloproducaoAppService;
        private readonly IGeoConfigurationAppService _geoConfigurationAppService;
        public CicloProducaoController(ICicloProducaoAppService cicloproducaoAppService, IGeoConfigurationAppService geoConfigurationAppService)
        {
            _cicloproducaoAppService = cicloproducaoAppService;
            _geoConfigurationAppService = geoConfigurationAppService;
        }

        /// <type>HttpGet</type>
        /// <link>'/cicloproducao/'</link>
        public IEnumerable<CicloProducao> Get()
        {
            return _cicloproducaoAppService.GetAll();
        }

        /// <type>HttpGet</type>
        /// <link>'/cicloproducao/', { params: { objID: params.name } }</link>
        public CicloProducao Get(Guid objID)
        {
            return _cicloproducaoAppService.Find(objID);
        }

        [HttpGet]
        [ActionName("getallbyareaservico")]
        [Route("api/cicloproducao/getallbyareaservico")]
        public IEnumerable<CicloViewer> GetAllByAreaServico(Guid IDAreaServico, string Type)
        {
            return _cicloproducaoAppService.GetAllCicloByAreaServico(IDAreaServico, Type);  
        }

        [HttpGet]
        [ActionName("getciclobypropriedadesafra")]
        [Route("api/cicloproducao/getciclobypropriedadesafra")]
        public IEnumerable<CicloProducaoView> GetCicloByPropriedadeSafra(Guid IDSafra, Guid IDPropriedade, string Type)
        {
            return _cicloproducaoAppService.GetCicloByPropriedadeSafra(IDSafra, IDPropriedade, Type); 
        }

        [HttpGet]
        [ActionName("getciclo")]
        [Route("api/cicloproducao/getciclo")]
        public IEnumerable<CicloProducaoView> GetCiclo(Guid objID, int tipoCiclo, int retorno)
        {
            return _cicloproducaoAppService.GetCiclo(objID, tipoCiclo, retorno);
        }

        [HttpPost]
        [ActionName("getcicloandareaservico")]
        [Route("api/cicloproducao/getcicloandareaservico")]
        public IEnumerable<CicloProducaoView> GetCicloAndAreaServico(Guid objID, int tipoCiclo, int retorno)
        {
            return _cicloproducaoAppService.GetCicloAndAreaServico(objID, tipoCiclo, retorno); 
        }

        [HttpPost]
        [ActionName("postciclo")]
        [Route("api/cicloproducao/postciclo")]
        public ValidationResult PostCiclo(IEnumerable<CicloPost> obj)
        {
            ValidationResult vlr =  new ValidationResult(); 
            foreach (var item in obj)
            {
                CicloProducao cp = new CicloProducao();
                cp.objID = Guid.NewGuid();
                cp.IDAreaServico = item.IDAreaServico; 
                cp.IDCultura  = null; 
                cp.IDVariedadeCultura  = null; 
                cp.IDCulturaAnterior = null;
                cp.tipo = item.tipo;
                cp.ciclo = 0;
                cp.identificacao = item.identificacao;
                cp.tamanho = item.tamanho; 
                cp.dataPlantio = null; 
                cp.dataRealPlantio = null; 
                cp.dataColheita = null;
                cp.prodMinima = null; 
                cp.prodMaxima = null; 
                cp.prodReal = null; 
                cp.observacoes      = null; 
                cp.parametroTecnico = null; 
                cp.parametroInterno = null;
                cp.inoculante       = true; 
                cp.codigo           = 0;
                cp.centerLegend     = item.centerLegend;
                cp.jsonField        = item.jsonField; 
                cp.geo = _geoConfigurationAppService.GetGeoPolygon(item.geostring);
                vlr = _cicloproducaoAppService.Add(cp); 
            }

            return vlr; 
        }
        /// <param name="obj"></param>
        /// <type>HttpPost</type>
        /// <link>'/cicloproducao/', obj </link>
        public ValidationResult Post([FromBody] CicloProducaoView obj)
        {
            CicloProducao objeto = Mapper.Map<CicloProducaoView, CicloProducao>(obj);
            objeto.objID = Guid.NewGuid(); 
            objeto.geo = _geoConfigurationAppService.GetGeoPolygon(obj.geoJson);
            return _cicloproducaoAppService.Add(objeto);
        }

        /// <param name="objID"></param>
        /// <param name="obj"></param>
        /// <type>HttpPut</type>
        /// <link>'/cicloproducao/' + objID, obj</link>
        public ValidationResult Put(string objID, [FromBody] CicloProducao obj)
        {
            CicloProducao item = _cicloproducaoAppService.Find(obj.objID);
            item.objID = obj.objID;
            item.IDAreaServico = obj.IDAreaServico;
            item.IDCultura = obj.IDCultura;
            item.IDVariedadeCultura = obj.IDVariedadeCultura;
            item.IDCulturaAnterior = obj.IDCulturaAnterior;
            item.tipo = obj.tipo;
            item.ciclo = obj.ciclo;
            item.jsonField = obj.jsonField; 
            item.identificacao = obj.identificacao;
            item.tamanho = obj.tamanho;
            item.dataPlantio = obj.dataPlantio;
            item.dataRealPlantio = obj.dataRealPlantio;
            item.dataColheita = obj.dataColheita;
            item.prodMinima = obj.prodMinima;
            item.prodMaxima = obj.prodMaxima;
            item.prodReal = obj.prodReal;
            item.observacoes = obj.observacoes;
            item.parametroTecnico = obj.parametroTecnico;
            item.parametroInterno = obj.parametroInterno;
            item.inoculante = obj.inoculante;
            item.codigo = obj.codigo; 

            return _cicloproducaoAppService.Update(item);
        }

        /// <param name="objID"></param>
        /// <type>HttpDelete</type>
        /// <link>'/area/' + objID</link>
        public ValidationResult Delete(string objID)
        {
            CicloProducao obj = _cicloproducaoAppService.Find(Guid.Parse(objID));
            return _cicloproducaoAppService.Remove(obj);
        }
    }
}