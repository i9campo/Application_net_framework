using FluentValidation.Results;
using SharpDX;
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
    public class AnaliseSoloController : ApiController
    {
        private readonly IAnaliseSoloAppService _AnaliseSoloAppService;
        private readonly IAreaServicoAppService _AreaServicoAppService;
        private readonly IGridAppService _GridAppService;
        private readonly IGeoConfigurationAppService _GeoAppService;
        public AnaliseSoloController(IAnaliseSoloAppService analiseSoloAppService, IAreaServicoAppService areaServicoAppService, IGridAppService gridappservice, IGeoConfigurationAppService geoconfigappservice)
        {
            _AnaliseSoloAppService = analiseSoloAppService;
            _AreaServicoAppService = areaServicoAppService;
            _GridAppService = gridappservice;
            _GeoAppService = geoconfigappservice;
        }

        public IEnumerable<AnaliseSolo> Get()
        {
            return _AnaliseSoloAppService.GetAll();
        }

        [HttpGet]
        [ActionName("GetLstByGrid")]
        [Route("api/analisesolo/GetLstByGrid")]
        public IEnumerable<AnaliseSolo> GetLstByGrid(Guid IDGrid)
        {
            return _AnaliseSoloAppService.GetAll().Where(o => o.IDGrid == IDGrid).ToList(); 
        }


        [HttpGet]
        [ActionName("GetLstByGridss")]
        [Route("api/analisesolo/GetLstByGridss")]
        public IEnumerable<AnaliseSolo> GetLstByGridss(Guid IDGrid)
        {
            /// Retorna uma lista de analise ("Sem sub-amostra"). ss

            return _AnaliseSoloAppService.GetAll().Where(o => o.IDGrid == IDGrid && o.subAmostra == false).ToList();
        }




        [HttpGet]
        [ActionName("getlistbyareaservico")]
        [Route("api/analisesolo/getlistbyareaservico")]
        public IEnumerable<AnaliseSoloViewer> GetListByAreaServico(Guid IDAreaServico, bool retorno)
        {
            return _AnaliseSoloAppService.GetListByAreaServico(IDAreaServico, retorno);
        }

        public AnaliseSoloView Get(Guid objID)
        {
            return _AnaliseSoloAppService.FindAnalise(objID);
        }

        [HttpGet]
        [ActionName("getanalisebypropriedade")]
        [Route("api/analisesolo/getanalisebypropriedade")]
        public IEnumerable<AnaliseSoloView> GetAnaliseByPropriedade(Guid IDPropriedade)
        {
            return _AnaliseSoloAppService.GetAnaliseByPropriedade(IDPropriedade);
        }

        [HttpGet]
        [ActionName("getmedia")]
        [Route("api/analisesolo/getmedia")]
        public IEnumerable<MediaAnalise> GetMedia(Guid IDAreaServico, Guid? IDGrid, int Perfil, int Und, int Tipo, int RetornoP)
        {
            return _AnaliseSoloAppService.GetMediaAnaliseSolo(IDAreaServico, IDGrid, Perfil, Und, Tipo, RetornoP);
        }

        [HttpPost]
        [ActionName("postanalise")]
        [Route("api/analisesolo/postanalise")]
        public ValidationResult PostAnalise([FromBody] IEnumerable<AnaliseSoloView> lstpontos)
        {
            ValidationResult vr = new ValidationResult(); 

            foreach (var item in lstpontos)
            {
                AnaliseSolo objeto      = new AnaliseSolo();
                objeto.IDAreaServico    = Guid.Parse(item.IDAreaServico.ToString());
                AreaServicoView ars     = _AreaServicoAppService.FindFullAreaServico(Guid.Parse(item.IDAreaServico.ToString()), null, null, true);
                if (ars != null)
                {
                    try
                    {
                        objeto.IDGrid = Guid.Parse(_GridAppService.GetByGeoAreaServico(Guid.Parse(item.IDAreaServico.ToString()), item.geoString, ars.Servico).ID.ToString());
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    Double oPonto;

                    objeto.descricao = item.ponto.ToString() + " - " + ars.area;
                    objeto.ponto = item.ponto.ToString().Contains(".") ? int.Parse(item.ponto.ToString().Split('.')[0]) : int.Parse(item.ponto.ToString());
                    objeto.sequenciaSubA = item.ponto.ToString().Contains(".") ? item.ponto.ToString().Split('.')[1] : "";
                    objeto.subAmostra = item.ponto.ToString().Contains(".");

                    objeto.geo = _GeoAppService.GetGeoPoint(item.geoString);
                    objeto.jsonField = item.jsonField; 

                    vr = _AnaliseSoloAppService.Add(objeto);
                }
            }

            return vr; 
        }

        public ValidationResult Post([FromBody] AnaliseSoloView obj)
        {
            AnaliseSolo oAnaliseSolo = new AnaliseSolo(obj);
            return _AnaliseSoloAppService.Add(oAnaliseSolo);
        }

        [HttpPost]
        [ActionName("replicarparamesmaarea")]
        [Route("api/analisesolo/replicarparamesmaarea")]
        public ValidationResult ReplicarPorArea([FromBody] UpdateGridForAnalise item)
        {
            Guid objID = Guid.Parse(item.objID.ToString()); 

            AnaliseSolo dt = _AnaliseSoloAppService.Find(objID);
            AnaliseSolo no = new AnaliseSolo();

            no.IDAreaServico = dt.IDAreaServico;
            no.IDTipoSolo = dt.IDTipoSolo;
            no.IDGrid = item.IDGrid;
            no.descricao = dt.descricao;
            no.data = dt.data;
            no.compactacao = dt.compactacao;
            no.profundidade = dt.profundidade;
            no.ponto = dt.ponto;
            no.subAmostra = dt.subAmostra;
            no.sequenciaSubA = dt.sequenciaSubA; 
            no.agua = dt.agua;
            no.cacl2 = dt.cacl2;
            no.mo = dt.mo;
            no.momicro = dt.momicro;
            no.co = dt.co;
            no.pmehl = dt.pmehl;
            no.pres = dt.pres;
            no.k = dt.k;
            no.s = dt.s;
            no.ca = dt.ca;
            no.mg = dt.mg;
            no.al = dt.al;
            no.hal = dt.hal;
            no.ctc = dt.ctc;
            no.argila = dt.argila;
            no.b = dt.b;
            no.zn = dt.zn;
            no.fe = dt.fe;
            no.mn = dt.mn;
            no.cu = dt.cu;
            no.somaBase = dt.somaBase;
            no.v = dt.v;
            no.relcamg = dt.relcamg;
            no.relcak = dt.relcak;
            no.relmgk = dt.relmgk;
            no.relcamgk = dt.relcamgk;
            no.ctcca = dt.ctcca;
            no.ctck = dt.ctck;
            no.ctcal = dt.ctcal;
            no.jsonField = dt.jsonField;
            no.geo = dt.geo;
            return _AnaliseSoloAppService.Add(no); 
        }

        [HttpPost]
        [ActionName("replicarparaoutraarea")]
        [Route("api/analisesolo/replicarparaoutraarea")]
        public ValidationResult ReplicarParaOutraArea([FromBody] UpdateGridForAnalise item)
        {
            Guid objID = Guid.Parse(item.objID.ToString());

            AnaliseSolo dt = _AnaliseSoloAppService.Find(objID);
            AnaliseSolo no = new AnaliseSolo();

            no.IDAreaServico = Guid.Parse(item.IDAreaServico.ToString());
            no.IDTipoSolo = dt.IDTipoSolo;
            no.IDGrid = item.IDGrid;
            no.descricao = dt.descricao;
            no.data = dt.data;
            no.compactacao = dt.compactacao;
            no.profundidade = dt.profundidade;
            no.ponto = dt.ponto;
            no.subAmostra = dt.subAmostra;
            no.sequenciaSubA = dt.sequenciaSubA;
            no.agua = dt.agua;
            no.cacl2 = dt.cacl2;
            no.mo = dt.mo;
            no.momicro = dt.momicro;
            no.co = dt.co;
            no.pmehl = dt.pmehl;
            no.pres = dt.pres;
            no.k = dt.k;
            no.s = dt.s;
            no.ca = dt.ca;
            no.mg = dt.mg;
            no.al = dt.al;
            no.hal = dt.hal;
            no.ctc = dt.ctc;
            no.argila = dt.argila;
            no.b = dt.b;
            no.zn = dt.zn;
            no.fe = dt.fe;
            no.mn = dt.mn;
            no.cu = dt.cu;
            no.somaBase = dt.somaBase;
            no.v = dt.v;
            no.relcamg = dt.relcamg;
            no.relcak = dt.relcak;
            no.relmgk = dt.relmgk;
            no.relcamgk = dt.relcamgk;
            no.ctcca = dt.ctcca;
            no.ctck = dt.ctck;
            no.ctcal = dt.ctcal;
            no.jsonField = dt.jsonField;
            no.geo = dt.geo;
            return _AnaliseSoloAppService.Add(no);
        }

        [HttpPut]
        [ActionName("transferirparamesmaarea")]
        [Route("api/analisesolo/transferirparamesmaarea/{objID}")]
        public ValidationResult TransferirParaMesmaArea(Guid objID, UpdateGridForAnalise item)
        {
            AnaliseSolo obj = _AnaliseSoloAppService.Find(Guid.Parse(item.objID.ToString()));
            //if (item.tipo == 1)
                obj.IDGrid = item.IDGrid;

            //if (obj == null)
            //    return null;
            //if (item.tipo == 2)
            //{
            //    AnaliseSolo rplc = _AnaliseSoloAppService.Find(objID);
            //    obj = _AnaliseSoloAppService.Find(Guid.Parse(item.objID.ToString()));
            //    obj.IDTipoSolo = rplc.IDTipoSolo;
            //    obj.descricao = rplc.descricao;
            //    obj.compactacao = rplc.compactacao;
            //    obj.profundidade = rplc.profundidade;
            //    obj.data = rplc.data;
            //    obj.ponto = rplc.ponto;
            //    obj.subAmostra = rplc.subAmostra;
            //    obj.sequenciaSubA = rplc.sequenciaSubA;
            //    obj.agua = rplc.agua;
            //    obj.cacl2 = rplc.cacl2;
            //    obj.mo = rplc.mo;
            //    obj.momicro = rplc.momicro;
            //    obj.co = rplc.co;
            //    obj.pmehl = rplc.pmehl;
            //    obj.pres = rplc.pres;
            //    obj.k = rplc.k;
            //    obj.s = rplc.s;
            //    obj.ca = rplc.ca;
            //    obj.s = rplc.s;
            //    obj.mg = rplc.mg;
            //    obj.al = rplc.al;
            //    obj.hal = rplc.hal;
            //    obj.ctc = rplc.ctc;
            //    obj.argila = rplc.argila;
            //    obj.b = rplc.b;
            //    obj.zn = rplc.zn;
            //    obj.fe = rplc.fe;
            //    obj.mn = rplc.mn;
            //    obj.cu = rplc.cu;
            //    obj.somaBase = rplc.somaBase;
            //    obj.v = rplc.v;
            //    obj.relcamg = rplc.relcamg;
            //    obj.relcak = rplc.relcak;
            //    obj.relmgk = rplc.relmgk;
            //    obj.relcamgk = rplc.relcamgk;
            //    obj.ctcca = rplc.ctcca;
            //    obj.ctcmg = rplc.ctcmg;
            //    obj.ctck = rplc.ctck;
            //    obj.ctcal = rplc.ctcal;
            //}
            return _AnaliseSoloAppService.Update(obj);
        }

        [HttpPut]
        [ActionName("transferirparaoutraarea")]
        [Route("api/analisesolo/transferirparaoutraarea/{objID}")]
        public ValidationResult TransferirParaOutraArea(Guid objID, UpdateGridForAnalise item)
        {
            AnaliseSolo obj = _AnaliseSoloAppService.Find(objID);

            /// Tipo: 1 transfere 20-40 de uma área para outra, alterando a 
            if (item.tipo == 1)
            {
                obj.IDAreaServico = Guid.Parse(item.IDAreaServico.ToString());  
                obj.IDGrid          = item.IDGrid;
            }

            /// Tipo: 2 gera uma cópia de uma 
            if (item.tipo == 2)
            {
                AnaliseSolo rplc = _AnaliseSoloAppService.Find(objID); 

                obj = _AnaliseSoloAppService.Find(Guid.Parse(item.objID.ToString()));

                obj.IDTipoSolo = rplc.IDTipoSolo; 
                obj.descricao = rplc.descricao; 
                obj.compactacao = rplc.compactacao;
                obj.profundidade = rplc.profundidade;
                obj.data = rplc.data;
                obj.ponto = rplc.ponto;
                obj.subAmostra = rplc.subAmostra;
                obj.sequenciaSubA = rplc.sequenciaSubA;
                obj.agua = rplc.agua;
                obj.cacl2 = rplc.cacl2;

                obj.mo = rplc.mo;
                obj.momicro = rplc.momicro;
                obj.co = rplc.co;
                obj.pmehl = rplc.pmehl;
                obj.pres = rplc.pres;
                obj.k = rplc.k;
                obj.s = rplc.s;
                obj.ca = rplc.ca;
                obj.s = rplc.s;
                obj.mg = rplc.mg;
                obj.al = rplc.al;
                obj.hal = rplc.hal;
                obj.ctc = rplc.ctc;
                obj.argila = rplc.argila;
                obj.b = rplc.b;
                obj.zn = rplc.zn;
                obj.fe = rplc.fe;
                obj.mn = rplc.mn;
                obj.cu = rplc.cu;
                obj.somaBase = rplc.somaBase;
                obj.v = rplc.v;
                obj.relcamg = rplc.relcamg;
                obj.relcak = rplc.relcak;
                obj.relmgk = rplc.relmgk;
                obj.relcamgk = rplc.relcamgk;
                obj.ctcca = rplc.ctcca;
                obj.ctcmg = rplc.ctcmg;
                obj.ctck = rplc.ctck;
                obj.ctcal = rplc.ctcal; 
            }

            return _AnaliseSoloAppService.Update(obj);
        }

        public ValidationResult Put([FromBody] AnaliseSoloView obj)
        {
            AnaliseSolo oAnaliseSolo = new AnaliseSolo(obj);
            return _AnaliseSoloAppService.Update(oAnaliseSolo);
        }

        [HttpPut]
        [ActionName("UpdateAnalise")]
        [Route("api/analisesolo/UpdateAnalise/{objID}")]
        public ValidationResult UpdateAnalise(Guid objID, [FromBody] AnaliseSoloView obj)
        {
            AnaliseSolo db = _AnaliseSoloAppService.Find(objID);
            double result = 0;

            if (!String.IsNullOrEmpty(obj.IDTipoSolo))
                db.IDTipoSolo = Guid.Parse(obj.IDTipoSolo); 

            db.descricao        = obj.descricao;
            db.compactacao      = obj.compactacao;
            db.profundidade     = obj.profundidade;
            db.data             = obj.data;
            db.ponto            = int.Parse(obj.ponto.ToString());
            db.subAmostra       = bool.Parse(obj.subAmostra == null ? "false" : obj.subAmostra.ToString());
            db.sequenciaSubA    = obj.sequenciaSubA;
            db.agua             = double.Parse(obj.Agua.ToString());
            db.cacl2            = double.Parse(obj.Cacl.ToString());

            db.mo = double.TryParse(obj.MO.ToString(), out result) ? double.Parse(obj.MO.ToString()) : 0;
            db.momicro = double.TryParse(obj.momicro.ToString(), out result) ? double.Parse(obj.momicro.ToString()) : 0;
            db.co = double.TryParse(obj.Co.ToString(), out result) ? double.Parse(obj.Co.ToString()) : 0;
            db.pmehl = double.TryParse(obj.PMehl.ToString(), out result) ? double.Parse(obj.PMehl.ToString()) : 0;
            db.pres = double.TryParse(obj.PRes.ToString(), out result) ? double.Parse(obj.PRes.ToString()) : 0;
            db.k = double.TryParse(obj.K.ToString(), out result) ? double.Parse(obj.K.ToString()) : 0;
            db.s = double.TryParse(obj.S.ToString(), out result) ? double.Parse(obj.S.ToString()) : 0;
            db.ca = double.TryParse(obj.Ca.ToString(), out result) ? double.Parse(obj.Ca.ToString()) : 0;
            db.s = double.TryParse(obj.S.ToString(), out result) ? double.Parse(obj.S.ToString()) : 0;
            db.mg = double.TryParse(obj.Mg.ToString(), out result) ? double.Parse(obj.Mg.ToString()) : 0;
            db.al = double.TryParse(obj.Al.ToString(), out result) ? double.Parse(obj.Al.ToString()) : 0;
            db.hal = double.TryParse(obj.HAl.ToString(), out result) ? double.Parse(obj.HAl.ToString()) : 0;
            db.ctc = double.TryParse(obj.CTC.ToString(), out result) ? double.Parse(obj.CTC.ToString()) : 0;
            db.argila = double.TryParse(obj.Argila.ToString(), out result) ? double.Parse(obj.Argila.ToString()) : 0;
            db.b = double.TryParse(obj.B.ToString(), out result) ? double.Parse(obj.B.ToString()) : 0;
            db.zn = double.TryParse(obj.Zn.ToString(), out result) ? double.Parse(obj.Zn.ToString()) : 0;
            db.fe = double.TryParse(obj.Fe.ToString(), out result) ? double.Parse(obj.Fe.ToString()) : 0;
            db.mn = double.TryParse(obj.Mn.ToString(), out result) ? double.Parse(obj.Mn.ToString()) : 0;
            db.cu = double.TryParse(obj.Cu.ToString(), out result) ? double.Parse(obj.Cu.ToString()) : 0;
            db.somaBase = double.TryParse(obj.SomaBases.ToString(), out result) ? double.Parse(obj.SomaBases.ToString()) : 0;
            db.v = double.TryParse(obj.V.ToString(), out result) ? double.Parse(obj.V.ToString()) : 0;
            db.relcamg = double.TryParse(obj.relCaMg.ToString(), out result) ? double.Parse(obj.relCaMg.ToString()) : 0;
            db.relcak = double.TryParse(obj.relCaK.ToString(), out result) ? double.Parse(obj.relCaK.ToString()) : 0;
            db.relmgk = double.TryParse(obj.relMgK.ToString(), out result) ? double.Parse(obj.relMgK.ToString()) : 0;
            db.relcamgk = double.TryParse(obj.relCaMgK.ToString(), out result) ? double.Parse(obj.relCaMgK.ToString()) : 0;
            db.ctcca = double.TryParse(obj.CTCCa.ToString(), out result) ? double.Parse(obj.CTCCa.ToString()) : 0;
            db.ctcmg = double.TryParse(obj.CTCMg.ToString(), out result) ? double.Parse(obj.CTCMg.ToString()) : 0;
            db.ctck = double.TryParse(obj.CTCK.ToString(), out result) ? double.Parse(obj.CTCK.ToString()) : 0;
            db.ctcal = double.TryParse(obj.CTCAl.ToString(), out result) ? double.Parse(obj.CTCAl.ToString()) : 0;

            return _AnaliseSoloAppService.Update(db);
        }

        [HttpPut]
        public ValidationResult Update(Guid objID, EditedAnalises obj)
        {
            AnaliseSolo ans = _AnaliseSoloAppService.Find(objID);
            if (obj.tipo.Equals("PONTO"))
                ans.ponto = int.Parse(obj.PONTO.ToString());

            if (obj.tipo.Equals("SEQUENCIA"))
                if (String.IsNullOrEmpty(obj.SEQUENCIA))
                {
                    ans.subAmostra = false;
                    ans.sequenciaSubA = "";
                }
                else
                {
                    ans.subAmostra = true;
                    ans.sequenciaSubA = obj.SEQUENCIA.ToString();
                }

            return _AnaliseSoloAppService.Update(ans);
        }

        [HttpDelete]
        public ValidationResult Del(Guid objID)
        {
            AnaliseSolo obj = _AnaliseSoloAppService.Find(objID);
            return _AnaliseSoloAppService.Remove(obj); 
        }

        [HttpDelete]
        [ActionName("deletepoint")]
        [Route("api/analisesolo/deletepoint")]
        public bool Delete(Guid IDAreaServico)
        {
            //IEnumerable<AnaliseSoloView> obj = _AnaliseSoloAppService.GetAnaliseSoloByAreaServico(IDAreaServico, "POINTS");
            try
            {
                //foreach (var item in obj)
                //{
                //    AnaliseSolo o = _AnaliseSoloAppService.Find(Guid.Parse(item.objID.ToString()));
                //    _AnaliseSoloAppService.Remove(o);
                //}
            }
            catch (Exception)
            {
                return false; 
            }
            return true; 
        }
    }
}