using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sigma.App.AppService
{
    public class CorretivoAppService: AppService<Corretivo>, ICorretivoAppService
    {
        private readonly ICorretivoService _Service; 
        public CorretivoAppService(ICorretivoService service)
            :base(service)
        {
            _Service = service; 
        }


        public IEnumerable<CorretivoView> GetCorretivoGrid(Guid IDPropriedade, Guid IDSafra)
        {
            return _Service.GetCorretivoGrid(IDPropriedade, IDSafra);
        }

        public IEnumerable<CorretivoView> GetListAlteracaoCorretivo(Guid IDPropriedade, Guid IDSafra, string corretivo, string prnt, string perCaO, string perMgO, string perP2O5, string perK2O, string S, string perCa)
        {
            return _Service.GetListAlteracaoCorretivo(IDPropriedade, IDSafra, corretivo, prnt, perCaO, perMgO, perP2O5, perK2O, S, perCa);
        }

        public IEnumerable<CorretivoView> UpdateListCorretivo(double prnt, double perCaO, double Mgo, double P2O5, double K2O5, String Corretivoalt, String Corretivoant, double eficiencia, String IDPropriedade, String IDSafra, double S, double Ca)
        {
            return _Service.UpdateListCorretivo(prnt, perCaO, Mgo, P2O5, K2O5, Corretivoalt, Corretivoant, eficiencia, IDPropriedade, IDSafra, S, Ca);
        }

        public int PostCorretivo(double prnt, double perCaO, double Mgo, double P2O5, double K2O5, String Corretivoalt, String Corretivoant, double eficiencia, double S, double Ca)
        {
            return _Service.PostCorretivo(prnt, perCaO, Mgo, P2O5, K2O5, Corretivoalt, Corretivoant, eficiencia, S, Ca);
        }

        public IEnumerable<CorretivoView> GetOptionListaCompra(String IDArea, Guid IDSafra)
        {
            return _Service.GetOptionListaCompra(IDArea, IDSafra);
        }

        public IEnumerable<CorretivoView> GetListCorretivoCompra(String IDArea, Guid IDSafra, string optionCorretivo, string optionPerfil, string Tipo, string numServico)
        {

            return _Service.GetListCorretivoCompra(IDArea, IDSafra, optionCorretivo, optionPerfil, Tipo, numServico);
        }

        public IEnumerable<string> GetRelatorioListParcialCorretivos(String IDArea, String IDSafra, String CorretivoSelecionado, String IDCorretivoSelecionado, String IDPropriedade, String numServico, String opcaoPerfil, String opcaoCorretivo, String opcaoFertilizante, String Fertilizante, String IDFertilizante, String Tipo)
        {

            return _Service.GetRelatorioListParcialCorretivos(IDArea, IDSafra, CorretivoSelecionado, IDCorretivoSelecionado, IDPropriedade, numServico, opcaoPerfil, opcaoCorretivo, opcaoFertilizante, Fertilizante, IDFertilizante, Tipo);
        }

        public Corretivo UpdateAdicaoCorretivo(Object corretivo)
        {
            return _Service.UpdateAdicaoCorretivo(corretivo);
        }

        #region Recomendação alteração de corretivo. 

        public IEnumerable<CorretivoView> GetAllCorretivoSelectAlteracao(Guid? IDArea, Guid IDPropriedade, Guid IDSafra, int Type)
        {
            return _Service.GetAllCorretivoSelectAlteracao(IDArea, IDPropriedade, IDSafra, Type);
        }

        #endregion

        #region Recomendação tela de correção/ análise/ perfil/ ciclos.

 


        public IEnumerable<Corretivo> GetListCorretivo(Guid? IDAreaServico, Guid? IDGrid, Guid? IDPropriedade, Guid? IDSafra, int opcao, int perfil, int corretivo)
        {
            return _Service.GetListCorretivo(IDAreaServico, IDGrid, IDPropriedade, IDSafra, opcao, perfil, corretivo);
        }
        public IEnumerable<CorretivoView> GetListOpcao(Guid IDAreaServico)
        {
            return _Service.GetListOpcao(IDAreaServico);

        }

        public IEnumerable<CorretivoView> ReplicarCorretivo(IEnumerable<CorretivoView> obj)
        {
            return _Service.ReplicarCorretivo(obj);

        }

        public int GetCountRegistros(Guid objID, int Type)
        {
            return _Service.GetCountRegistros(objID, Type);
        }

        public ResultadoCorrecao RetornaResultadoCorrecao(Guid IDAreaServico, Guid? IDGrid, String Profundidade, int unid, int tipo, int RetornoP,   string Corretivo, int Opcao)
        {
            return _Service.RetornaResultadoCorrecao(IDAreaServico, IDGrid, Profundidade, unid, tipo, RetornoP, Corretivo, Opcao);
        }


        public bool UpdateCorretivoOpcaoMaracada(Guid objID, int opcao, int perfil, bool MarcarOrDesmarcar)
        {

            return _Service.UpdateCorretivoOpcaoMaracada(objID, opcao, perfil, MarcarOrDesmarcar);
        }

        public bool UpdateOpcaoMarcada(Guid objID, bool MarcarOrDesmarcar)
        {
            return _Service.UpdateOpcaoMarcada(objID, MarcarOrDesmarcar);
        }

        public CorretivoView GetOpcaoMarcadaByOpcao(Guid objID, int? opcao, int opcaomarcada, int retornoGridOrAreaServico)
        {
            return _Service.GetOpcaoMarcadaByOpcao(objID, opcao, opcaomarcada, retornoGridOrAreaServico);
        }

        public IEnumerable<CorretivoView> GetListAlteracaoCorretivoNew(string IDAreaServico, string eficiencia, string corretivo, string prnt, string perCaO, string perMgO, string perP2O5, string perK2O, string s, string perCa, string neweficiencia, string newCorretivo, string newprnt, string newpercao, string newpermgo, string newperp2o5, string newperk2o, string newpers, string newperca, string dosefixa, Boolean tipoFosforom, Boolean rbSaturacao, Boolean RBParcial, Boolean rbMagnesio, Boolean rbPotassio, Boolean rbFosforo)
        {
            return _Service.GetListAlteracaoCorretivoNew(IDAreaServico, eficiencia, corretivo, prnt, perCaO, perMgO, perP2O5, perK2O, s, perCa, neweficiencia, newCorretivo, newprnt, newpercao, newpermgo, newperp2o5, newperk2o, newpers, newperca, dosefixa, tipoFosforom, rbSaturacao, RBParcial, rbMagnesio, rbPotassio, rbFosforo);
        }

        public bool UpdateOptionChecked(Guid objID, int Opcao, bool MarcarOrDesmarcar, int type)
        {
            return _Service.UpdateOptionChecked(objID, Opcao, MarcarOrDesmarcar, type); 
        }

        public CorretivoView GetOpcaoValida(Guid objID, int? opcao, int opcaomarcada, int retornoGridOrAreaServico)
        {
            // Este método faz a verificação se o grid e a opção selecionada é válida ou não. 
            return _Service.GetOpcaoValida(objID, opcao, opcaomarcada, retornoGridOrAreaServico);
        }

        public bool DeleteAllCorretivoByOption(Guid objID, int Option, int Type)
        {
            return _Service.DeleteAllCorretivoByOption(objID, Option, Type); 
        }

        public IEnumerable<Options> GetListOpcaoCorretivo(Guid objID, int Type, int perfil)
        {
            return _Service.GetListOpcaoCorretivo(objID, Type, perfil); 
        }

        public bool UpdateMarcado(Guid IDAreaServico, Guid IDGrid, int opcao, bool chk)
        {
            return _Service.UpdateMarcado(IDAreaServico, IDGrid, opcao, chk); 
        }

        public CorretivoView GetMediaCorretivo(Guid IDAreaServico, Guid? IDGrid, string profundidade, int tipo, int opcao, int perfil)
        {
            return _Service.GetMediaCorretivo(IDAreaServico, IDGrid, profundidade, tipo, opcao, perfil); 
        }

        public bool SetDivideDose(SetDivideDoseCorretivo obj)
        {
            return _Service.SetDivideDose(obj);
        }

        public bool CheckedAnaliseSolo2040(Guid IDAreaServico)
        {
            return _Service.CheckedAnaliseSolo2040(IDAreaServico); 
        }
        #endregion
    }
}
