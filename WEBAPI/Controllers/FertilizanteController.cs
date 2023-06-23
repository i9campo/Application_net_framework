using FluentValidation.Results;
using Sigma.App.AppService;
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
    public class FertilizanteController : ApiController
    {
        private readonly IFertilizanteAppService _fertilizanteAppService;

        public FertilizanteController(IFertilizanteAppService fertilizanteAppService)
        {
            _fertilizanteAppService = fertilizanteAppService;
        }

        // GET api/<controller>
        public IEnumerable<Fertilizante> Get()
        {
            return _fertilizanteAppService.GetAll();
        }

        [HttpGet]
        [ActionName("find_fertilizante")]
        [Route("api/fertilizante/find_fertilizante")]
        public Fertilizante FindFertilizante(Guid objID)
        {
            return _fertilizanteAppService.Find(objID); 
        }


        [HttpGet]
        [ActionName("getbyciclointermediario")]
        [Route("api/fertilizante/getbyciclointermediario")]
        public IEnumerable<Fertilizante> GetByCicloIntermediario(Guid IDCiclo)
        {
            return _fertilizanteAppService.GetAll().Where(o => o.IDCicloProducao == IDCiclo).ToList();
        }


        [HttpGet]
        [ActionName("getbycp")]
        [Route("api/fertilizante/getbycp")]
        public IEnumerable<FertilizanteView> GetByCP(Guid IDCiclo)
        {
            return _fertilizanteAppService.GetByCP(IDCiclo);
        }

        [HttpGet]
        [ActionName("getMediaCiclo")]
        [Route("api/fertilizante/getMediaCiclo")]
        public FertilizanteView GetMediaCiclo(Guid IDCiclo, int opcao)
        {
            return _fertilizanteAppService.GetMediaCiclo(IDCiclo, opcao);
        }

        [HttpGet]
        [ActionName("getoptionsbycp")]
        [Route("api/fertilizante/getoptionsbycp")]
        public IEnumerable<Options> GetOptionsByCP(Guid IDCiclo)
        {
            return _fertilizanteAppService.GetOptionsByCP(IDCiclo); 
        }

        [HttpGet]
        [ActionName("getcountbyciclo")]
        [Route("api/fertilizante/getcountbyciclo")]
        public int GetCountByCiclo(Guid IDCiclo)
        {
            return _fertilizanteAppService.GetCountByCiclo(IDCiclo);
        }

        [HttpGet]
        [ActionName("getbyopcao")]
        [Route("api/fertilizante/getbyopcao")]
        public IEnumerable<FertilizanteView> GetByOpcao(int opcao, Guid IDCiclo)
        {
            return _fertilizanteAppService.GetByOpcao(opcao, IDCiclo);
        }

        [HttpGet]
        [ActionName("getbyidcultura")]
        [Route("api/fertilizante/getbyidcultura")]
        public IEnumerable<FertilizanteView> GetByIDCultura(Guid IDCultura)
        {
            return _fertilizanteAppService.GetByIDCultura(IDCultura);
        }

        [HttpGet]
        [ActionName("getfertilizante")]
        [Route("api/fertilizante/getfertilizante")]
        public Fertilizante Get(Guid objID)
        {
            return _fertilizanteAppService.Find(objID);
        }

        public ValidationResult Post([FromBody] Fertilizante obj)
        {
            obj.objID = Guid.NewGuid();
            return _fertilizanteAppService.Add(obj);
        }

        // PUT api/fertilizante/5
        public ValidationResult Put(string objID, [FromBody] Fertilizante obj)
        {
            Fertilizante ft = _fertilizanteAppService.Find(Guid.Parse(objID));
            ft.IDCicloProducao = obj.IDCicloProducao;
            ft.IDFornecedor = obj.IDFornecedor;
            ft.IDEstagioCultura = obj.IDEstagioCultura;
            ft.foliar = obj.foliar;
            ft.nome = obj.nome;
            ft.daedap = obj.daedap;
            ft.marcado = obj.marcado;
            ft.opcao = obj.opcao;
            ft.qtde = obj.qtde;
            ft.eficiencia = obj.eficiencia;
            ft.densidade = obj.densidade;
            ft.custo = obj.custo;
            ft.n = obj.n;
            ft.p2o5 = obj.p2o5;
            ft.k2o = obj.k2o;
            ft.ca = obj.ca;
            ft.mg = obj.mg;
            ft.s = obj.s; 
            ft.b  = obj.b;
            ft.zn = obj.zn;
            ft.cu = obj.cu;
            ft.mn = obj.mn;
            ft.co = obj.co;
            ft.mo = obj.mo;  
            return _fertilizanteAppService.Update(ft);
        }


        [HttpPut]
        [ActionName("updatechecked")]
        [Route("api/fertilizante/updatechecked/{objID}")]
        public ValidationResult UpdateChecked(Guid objID, UpdateChecked obj)
        {
            Fertilizante ctv = _fertilizanteAppService.Find(objID);
            ctv.marcado = obj.chk == true ? 1 : 0;
            return _fertilizanteAppService.Update(ctv);
        }


        [HttpPut]
        [ActionName("putopcaomarcada")]
        [Route("api/fertilizante/putopcaomaracada/{objID}")]
        public bool PutOpcaoMaracada(Guid objID, UpdateFertilizanteMarcado obj)
        {
            return _fertilizanteAppService.UpdateOptionChecked(obj);
        }

        [HttpPut]
        [ActionName("putopcaodesmarcar")]
        [Route("api/fertilizante/putopcaodesmarcar")]
        public ValidationResult PutOpcaoDesmarcar(object obj)
        {
            //var serializer = new JavaScriptSerializer();
            //dynamic objeto = serializer.DeserializeObject(obj.ToString());
            //Guid IDCiclo = Guid.Parse(objeto["IDCicloProducao"]);
            //int opcao = int.Parse(objeto["opcao"].ToString());
            //var opcaoMarcado = _fertilizanteAppService.GetByCP(IDCiclo);
            //foreach (var item in opcaoMarcado)
            //{
            //    item.opcaoMarcada = 0;
            //    Fertilizante ob = Mapper.Map<FertilizanteView, Fertilizante>(item);
            //    _fertilizanteAppService.Update(ob);
            //}
            return null;
        }

        // DELETE api/fertilizante/5
        public ValidationResult Delete(Guid objID)
        {
            Fertilizante obj = _fertilizanteAppService.Find(objID);
            return _fertilizanteAppService.Remove(obj);
        }

        [HttpGet]
        [ActionName("delete_all_fertilizante")]
        [Route("api/fertilizante/delete_all_fertilizante")]
        public ValidationResult DeleteAllCiclos(int opcao, Guid IDCiclo)
        {
            ValidationResult vr = new ValidationResult();

            List<FertilizanteView> lst = _fertilizanteAppService.GetByOpcao(opcao, IDCiclo).ToList();
            foreach (var item in lst)
            {
                Fertilizante obj = _fertilizanteAppService.Find(Guid.Parse(item.objID.ToString()));
                vr = _fertilizanteAppService.Remove(obj); 
            }

            return vr; 
        }
    }
}