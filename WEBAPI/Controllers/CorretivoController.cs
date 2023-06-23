using AutoMapper;
using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class CorretivoController : ApiController
    {
        private readonly ICorretivoAppService _corretivoAppService;
        public CorretivoController(ICorretivoAppService corretivoAppService)
        {
            _corretivoAppService = corretivoAppService;
        }

        public IEnumerable<Corretivo> Get()
        {
            // Este método retorna todos os corretivos. 
            // Para executar essa requisição no react.js será assim (api.get('/corretivo')

            return _corretivoAppService.GetAll();
        }

        public Corretivo Get(Guid objID)
        {
            // Este método retorna um corretivo especifico através do objID. 
            // Para executar essa requisição no react.js será assim (api.get('/corretivo/', {params: {objID: item do state ou um ID no formato de texto } })
            return _corretivoAppService.Find(objID);
        }

        [HttpGet]
        [ActionName("getcorretivogrid")]
        [Route("api/corretivo/getcorretivogrid")]
        public IEnumerable<CorretivoView> GetCorretivoGrid(Guid IDPropriedade, Guid IDSafra)
        {
            return _corretivoAppService.GetCorretivoGrid(IDPropriedade, IDSafra);
        }

        [HttpGet]
        [ActionName("getoptionlistacompra")]
        [Route("api/corretivo/getoptionlistacompra")]
        public IEnumerable<CorretivoView> GetOptionListaCompra(String IDArea, Guid IDSafra)
        {
            return _corretivoAppService.GetOptionListaCompra(IDArea, IDSafra);
        }

        [HttpGet]
        [ActionName("getlistcorretivocompra")]
        [Route("api/corretivo/getlistcorretivocompra")]
        public IEnumerable<CorretivoView> GetListCorretivoCompra(String IDArea, Guid IDSafra, string optionCorretivo, string optionPerfil, string Tipo, string numServico)
        {
            if (optionCorretivo == null && optionPerfil != null)
            {
                optionCorretivo = "Sem Opção";
            }
            if (optionPerfil == null && optionCorretivo != null)
            {
                optionPerfil = "Sem Opção";
            }
            return _corretivoAppService.GetListCorretivoCompra(IDArea, IDSafra, optionCorretivo, optionPerfil, Tipo, numServico);
        }

        [HttpGet]
        [ActionName("getrelatoriolistparcialcorretivos")]
        [Route("api/corretivo/getrelatoriolistparcialcorretivos")]
        public IEnumerable<string> GetRelatorioListParcialCorretivos(String IDArea, String IDSafra, String CorretivoSelecionado, String IDCorretivoSelecionado, String IDPropriedade, String numServico, String opcaoPerfil, String opcaoCorretivo, String opcaoFertilizante, String Fertilizante, String IDFertilizante, String Tipo)
        {
            return _corretivoAppService.GetRelatorioListParcialCorretivos(IDArea, IDSafra, CorretivoSelecionado, IDCorretivoSelecionado, IDPropriedade, numServico, opcaoPerfil, opcaoCorretivo, opcaoFertilizante, Fertilizante, IDFertilizante, Tipo);
        }

        [HttpGet]
        [ActionName("updatelistcorretivo")]
        [Route("api/corretivo/updatelistcorretivo")]
        public IEnumerable<CorretivoView> UpdateListCorretivo(double prnt, double perCaO, double Mgo, double P2O5, double K2O5, String Corretivoalt, String Corretivoant, double eficiencia, String IDPropriedade, String IDSafra, double S, double Ca)
        {
            return _corretivoAppService.UpdateListCorretivo(prnt, perCaO, Mgo, P2O5, K2O5, Corretivoalt, Corretivoant, eficiencia, IDPropriedade, IDSafra, S, Ca);
        }

        [HttpGet]
        [ActionName("postcorretivo")]
        [Route("api/corretivo/postcorretivo")]
        public int PostCorretivo(double prnt, double perCaO, double Mgo, double P2O5, double K2O5, String Corretivoalt, String Corretivoant, double eficiencia, String IDPropriedade, String IDSafra, double S, double Ca)
        {
            int Results = _corretivoAppService.PostCorretivo(prnt, perCaO, Mgo, P2O5, K2O5, Corretivoalt, Corretivoant, eficiencia, S, Ca);
            _corretivoAppService.UpdateListCorretivo(prnt, perCaO, Mgo, P2O5, K2O5, Corretivoalt, Corretivoant, eficiencia, IDPropriedade, IDSafra, S, Ca);
            return Results;
        }

        [HttpGet]
        [ActionName("getlistopcao")]
        [Route("api/corretivo/getlistopcao")]
        public IEnumerable<CorretivoView> GetListOpcao(Guid IDAreaServico)
        {
            return _corretivoAppService.GetListOpcao(IDAreaServico);

        }

        [HttpGet]
        [ActionName("GetOptionList")]
        [Route("api/Corretivo/GetOptionList")]
        public IEnumerable<Options> GetOptionList(Guid objID, int Type, int perfil)
        {
            return _corretivoAppService.GetListOpcaoCorretivo(objID, Type, perfil);
        }

        [HttpPut]
        [ActionName("replicarcorretivo")]
        [Route("api/corretivo/replicarcorretivo")]
        public bool ReplicarCorretivo([FromBody] IEnumerable<CorretivoView> obj)
        {
            try
            {
                foreach (var item in obj)
                {
                    Corretivo oCorretivo = Mapper.Map<CorretivoView, Corretivo>(item);
                    Guid objID = Guid.NewGuid();
                    oCorretivo.objID = objID;
                    _corretivoAppService.Add(oCorretivo);
                }
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }

        [HttpGet]
        [ActionName("Checked2040")]
        [Route("api/corretivo/Checked2040")]
        public bool Checked2040(Guid IDAreaServico)
        {
            return _corretivoAppService.CheckedAnaliseSolo2040(IDAreaServico);
        }
        #region Recomendação alteração corretivo. 

        [HttpGet]
        [ActionName("getallcorretivoselectalteracao")]
        [Route("api/corretivo/getallcorretivoselectalteracao")]
        public IEnumerable<CorretivoView> GetAllCorretivoSelectAlteracao(Guid? IDArea, Guid IDPropriedade, Guid IDSafra, int Type)
        {
            return _corretivoAppService.GetAllCorretivoSelectAlteracao(IDArea, IDPropriedade, IDSafra, Type);
        }

        [HttpGet]
        [ActionName("getlistalteracaocorretivo")]
        [Route("api/corretivo/getlistalteracaocorretivo")]
        public IEnumerable<CorretivoView> GetListAlteracaoCorretivo(Guid IDPropriedade, Guid IDSafra, string corretivo, string prnt, string perCaO, string perMgO, string perK2O, string perP2O5, string S, string perCa)
        {
            return _corretivoAppService.GetListAlteracaoCorretivo(IDPropriedade, IDSafra, corretivo, prnt, perCaO, perMgO, perP2O5, perK2O, S, perCa);
        }

        [HttpGet]
        [ActionName("getlistalteracaocorretivonew")]
        [Route("api/corretivo/getlistalteracaocorretivonew")]
        public IEnumerable<CorretivoView> GetListAlteracaoCorretivoNew(string IDAreaServico, string eficiencia, string corretivo, string prnt, string perCaO, string perMgO, string perP2O5, string perK2O, string s, string perCa, string neweficiencia, string newCorretivo, string newprnt, string newpercao, string newpermgo, string newperp2o5, string newperk2o, string newpers, string newperca, string dosefixa, Boolean rbSaturacao, Boolean RBParcial, Boolean rbMagnesio, Boolean rbPotassio, Boolean rbFosforo)
        {
            Boolean tipoFosforom = false;
            return _corretivoAppService.GetListAlteracaoCorretivoNew(IDAreaServico, eficiencia, corretivo, prnt, perCaO, perMgO, perP2O5, perK2O, s, perCa, neweficiencia, newCorretivo, newprnt, newpercao, newpermgo, newperp2o5, newperk2o, newpers, newperca, dosefixa, tipoFosforom, rbSaturacao, RBParcial, rbMagnesio, rbPotassio, rbFosforo);
        }

        #endregion

        #region Recomendação tela de correção/ análise/ perfil/ ciclos. 
        [HttpGet]
        [ActionName("getcorretivo")]
        [Route("api/corretivo/getcorretivo")]
        public IEnumerable<Corretivo> GetListCorretivo(Guid? IDAreaServico, Guid? IDGrid, Guid? IDPropriedade, Guid? IDSafra, int opcao, int perfil, int corretivo)
        {
            return _corretivoAppService.GetListCorretivo(IDAreaServico, IDGrid, IDPropriedade, IDSafra, opcao, perfil, corretivo);
        }

        [HttpGet]
        [ActionName("getcountregistros")]
        [Route("api/corretivo/getcountregistros")]
        public int GetCountRegistros(Guid objID, int Type)
        {
            // Este método será utilizado para contar quantos registros de corretivos contém em uma área. 
            // O type vai definir o tipo de retorno se a contagem é referente a área ou zonas. 
            return _corretivoAppService.GetCountRegistros(objID, Type);
        }

        [HttpGet]
        [ActionName("retornaresultadocorrecao")]
        [Route("api/corretivo/retornaresultadocorrecao")]
        public ResultadoCorrecao RetornaResultadoCorrecao(Guid IDAreaServico, Guid? IDGrid, String Profundidade, int unid, int tipo, int RetornoP, string Corretivo, int Opcao)
        {
            return _corretivoAppService.RetornaResultadoCorrecao(IDAreaServico, IDGrid, Profundidade, unid, tipo, RetornoP, Corretivo, Opcao);
        }

        [HttpGet]
        [ActionName("getmediacorretivo")]
        [Route("api/corretivo/getmediacorretivo")]
        public CorretivoView GetMediaCorretivo(Guid IDAreaServico, Guid? IDGrid, string profundidade, int tipo, int opcao, int perfil)
        {
            return _corretivoAppService.GetMediaCorretivo(IDAreaServico, IDGrid, profundidade, tipo, opcao, perfil);
        }


        [HttpGet]
        [ActionName("checkedoptionmarcado")]
        [Route("api/corretivo/checkedoptionmarcado")]
        public CorretivoView CheckedOptionMarcado(Guid objID, int? opcao, int opcaomarcada, int retornoGridOrAreaServico)
        {
            // Este método faz a verificação se o grid e a opção selecionada é válida ou não. 
            return _corretivoAppService.GetOpcaoMarcadaByOpcao(objID, opcao, opcaomarcada, retornoGridOrAreaServico);
        }

        [HttpGet]
        [ActionName("getopcaovalida")]
        [Route("api/corretivo/getopcaovalida")]
        public CorretivoView GetOpcaoValida(Guid objID, int? opcao, int opcaomarcada, int retornoGridOrAreaServico)
        {
            // Este método faz a verificação se o grid e a opção selecionada é válida ou não. 
            return _corretivoAppService.GetOpcaoValida(objID, opcao, opcaomarcada, retornoGridOrAreaServico);
        }
        #endregion

        #region C.R.U.D 
        [HttpPut]
        [ActionName("updatechecked")]
        [Route("api/corretivo/updatechecked/{objID}")]
        public ValidationResult UpdateChecked(Guid objID, UpdateChecked obj)
        {
            Corretivo ctv = _corretivoAppService.Find(objID);
            ctv.marcado = obj.chk == true ?  1 : 0;
            return _corretivoAppService.Update(ctv); 
        }

        [HttpPut]
        [ActionName("updatemarcado")]
        [Route("api/corretivo/updatemarcado/{objID}")]
        public bool  UpdateMarcado(Guid? objID, UpdateChecked obj)
        {
            return _corretivoAppService.UpdateMarcado(obj.IDAreaServico, obj.IDGrid, int.Parse(obj.opcao.ToString()), obj.chk);
        }

        [HttpPut]
        [ActionName("updateopcaovalida")]
        [Route("api/corretivo/updateopcaovalida")]
        public bool UpdateOpcaoValida([FromBody] CorretivoView obj)
        {
            // Este método será utilizado para marcar e desmarcar a opção válida do corretivo. 
            // A opção marcar será responsavel por marcar e desmarcar o corretivo selecionado pelo grid e opção. 
            Guid objID = Guid.Parse((obj.IDGrid != null ? obj.IDGrid.ToString() : obj.IDAreaServico.ToString()));
            int opcao = int.Parse(obj.opcao.ToString());
            int perfil = int.Parse(obj.perfil == null ? "0" : obj.perfil.ToString());
            bool marcar = bool.Parse(obj.marcar.ToString());

            return _corretivoAppService.UpdateCorretivoOpcaoMaracada(objID, opcao, perfil, marcar);
        }

        [HttpPut]
        [ActionName("setdividedose")]
        [Route("api/corretivo/setdividedose/{objID}")]
        public bool SetDivideDose(Guid? objID, [FromBody]SetDivideDoseCorretivo obj)
        {
            return _corretivoAppService.SetDivideDose(obj); 
        }

        public ValidationResult Post(Corretivo obj)
        {
            if (obj.IDGrid != null)
                obj.opcaoMarcado = _corretivoAppService.GetOpcaoMarcadaByOpcao(Guid.Parse(obj.IDGrid.ToString()), obj.opcao, 1, 0) == null ? 0 : 1;
            else
                obj.opcaoMarcado = _corretivoAppService.GetOpcaoMarcadaByOpcao(Guid.Parse(obj.IDAreaServico.ToString()), obj.opcao, 1, 1) == null ? 0 : 1;

            return _corretivoAppService.Add(obj);
        }

        //PUT api/corretivo/+objID/{objeto}
        public ValidationResult Put(string objID, [FromBody] Corretivo obj)
        {
            return _corretivoAppService.Update(obj);
        }

        //DELETE api/corretivo/5
        public ValidationResult Delete(string objID)
        {
            Corretivo obj = _corretivoAppService.Find(Guid.Parse(objID));
            return _corretivoAppService.Remove(obj);
        }
        #endregion
        [HttpGet]
        [ActionName("UpdateOptionChecked")]
        [Route("api/Corretivo/UpdateOptionChecked")]
        public bool UpdateOptionChecked(Guid objID, int Opcao, bool MarcarOrDesmarcar, int type)
        {
            /// Este método será utilizado de uma forma dinâmica, ou seja o objID pode ser referente ao Grid ou área serviço. 
            /// Mas ele vai depender do Type, que pode ser o 1 ou 0. 
            /// 0 atualiza os dados da área serviço, 1 atualiza os dados da zona. 
            return _corretivoAppService.UpdateOptionChecked(objID, Opcao, MarcarOrDesmarcar, type); 
        }

        [HttpGet]
        [ActionName("DeleteAllCorretivoByOption")]
        [Route("api/Corretivo/DeleteAllCorretivoByOption")]
        public bool DeleteAllCorretivoByOption(Guid objID, int Option, int Type)
        {
            return _corretivoAppService.DeleteAllCorretivoByOption(objID, Option, Type); 
        }
    }
}