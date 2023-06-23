using FluentValidation.Results;
using Sigma.Domain.Calculate;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;

namespace Sigma.Domain.Services
{
    public class CorretivoService : Service<Corretivo>, ICorretivoService
    {
        private readonly IAnaliseSoloRepository _analiseSoloService     ;
        private readonly IAreaServicoRepository _areaServicoRepository  ;
        private readonly IAreaRepository        _areaRepository         ; 
        private readonly IPropriedadeRepository _propriedaderepository  ; 
        private readonly ICorretivoRepository   _corretivoRepository    ;
        private readonly IGridRepository        _gridRepository         ;
        private readonly ISafraRepository       _safraRepository        ;

        public CorretivoService(
            
            IAreaRepository         arearepository,
            IAreaServicoRepository  areaServicoRepository,
            IAnaliseSoloRepository  analiseSoloRepository,
            ICorretivoRepository    corretivoRepository,
            IGridRepository         gridrepository, 
            IPropriedadeRepository  propriedaderepository, 
            ISafraRepository        safrarepository
        )
            :base(corretivoRepository)
        {
            _analiseSoloService         = analiseSoloRepository ;
            _areaServicoRepository      = areaServicoRepository ;
            _corretivoRepository        = corretivoRepository   ;
            _propriedaderepository      = propriedaderepository ; 
            _gridRepository             = gridrepository        ;
            _areaRepository             = arearepository        ;
            _safraRepository            = safrarepository       ; 
        }


        public new ValidationResult Add(Corretivo corr)
        {
            UpdateAdicaoCorretivo(corr);

            return _corretivoRepository.Add(corr);
        }

        public new ValidationResult Update(Corretivo corr)
        {
            UpdateAdicaoCorretivo(corr);

            return _corretivoRepository.Update(corr);
        }
        /// <summary>
        /// Este método é utilizado para calcular a adição do corretivo em Centimol (mesma unidade da Análise de Solo)
        /// O valor é salvo na tabela, afim de obter a adição de produto sem a necessidade de calcular toda área
        /// Nenhum corretivo deve ser adicionado sem calcular a adição, estes campos são utilizados para realizar o Balanço Nutricional
        /// </summary>
        /// <param name="corr"></param>
        /// <returns></returns>
        public Corretivo UpdateAdicaoCorretivo(Corretivo corr)
        {
            MediaAnalise mediaAnalise = null;

            try
            {
                if (corr.IDGrid != null)
                    if (corr.perfil == 1)
                        mediaAnalise = _analiseSoloService.GetMediaAnaliseSolo(corr.IDAreaServico, corr.IDGrid, 1,0,1,0).ToList().FirstOrDefault();
                    else
                        mediaAnalise = _analiseSoloService.GetMediaAnaliseSolo(corr.IDAreaServico, corr.IDGrid, 0, 0, 1, 0).ToList().FirstOrDefault();
                else
                    mediaAnalise = _analiseSoloService.GetMediaAnaliseSolo(corr.IDAreaServico, null, 1, 0, 2, 0).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null; 
            }

            if (mediaAnalise == null)
                return corr; 

            if (corr.prnt > 0 && corr.perCaO > 0)
                corr.ca = ((corr.qtde * (corr.prnt * corr.perCaO / 10000)) / 560.78) * (corr.eficiencia / 100);
            else
                corr.ca = 0.00;

            if (corr.prnt > 0 && corr.perMgO > 0)
                corr.mg = ((corr.qtde * (corr.prnt * corr.perMgO / 10000)) / 403.06) * (corr.eficiencia / 100);
            else
                corr.mg = 0.00;


            if (corr.perK2O > 0)
                corr.k = ((corr.qtde * corr.perK2O / 100) / 2.40922) * (corr.eficiencia / 100);
            else
                corr.k = 0.00;

            Double KCorr = corr.k + Double.Parse(mediaAnalise.K.ToString());
            corr.k = cCorretivo.CalcularK(KCorr, Double.Parse(mediaAnalise.K.ToString()), Double.Parse(mediaAnalise.Ca.ToString()), Double.Parse(mediaAnalise.Mg.ToString()), 0, Double.Parse(mediaAnalise.CTC.ToString()), 1);

            corr.k = corr.k * 390.98;
            //Fator para dividir a dose quando for perfil
            int div = 10;

            if (corr.perfil == 1)
                div = 40;

            if (corr.perS > 0)
                corr.s = ((corr.qtde * (corr.perS / 100)) / div) * (corr.eficiencia / 100);
            else
                corr.s = 0.00;


            if (corr.perP2O5 > 0)
            {
                corr.p = ((corr.qtde * (corr.perP2O5 / 100)) / 4.5828) * (corr.eficiencia / 100);
                if (mediaAnalise != null)
                {
                    if (mediaAnalise != null)
                    {
                        if (Double.Parse(mediaAnalise.PMeHl.ToString()) > 0)
                        {
                            corr.p = corr.p / ((0.0242 * Math.Pow(Double.Parse(mediaAnalise.Argila.ToString()), 2)) - (0.9298 * Double.Parse(mediaAnalise.Argila.ToString())) + 14);
                        }
                        else if (Double.Parse(mediaAnalise.PRes.ToString()) > 0)
                        {
                            corr.p = corr.p / ((0.0007 * Math.Pow(Double.Parse(mediaAnalise.Argila.ToString()), 2)) + (0.1724 * Double.Parse(mediaAnalise.Argila.ToString())) + 4.2052);
                        }
                    }
                }
            }
            else
            {
                corr.p = 0.00;
            }

            return corr;
        }

        public IEnumerable<CorretivoView> GetCorretivoGrid(Guid IDPropriedade, Guid IDSafra)
        {
            return _corretivoRepository.GetCorretivoGrid(IDPropriedade, IDSafra);
        }

        public IEnumerable<CorretivoView> GetListAlteracaoCorretivo(Guid IDPropriedade, Guid IDSafra, string corretivo, string prnt, string perCaO, string perMgO, string perP2O5, string perK2O, string S, string perCa)
        {
            return _corretivoRepository.GetListAlteracaoCorretivo(IDPropriedade, IDSafra, corretivo, prnt, perCaO, perMgO, perP2O5, perK2O, S, perCa);
        }
        public IEnumerable<CorretivoView> GetOptionListaCompra(String IDArea, Guid IDSafra)
        {
            return _corretivoRepository.GetOptionListaCompra(IDArea, IDSafra);
        }
        public CorretivoView GetOpcaoValida(Guid objID, int? opcao, int opcaomarcada, int retornoGridOrAreaServico)
        {
            // Este método faz a verificação se o grid e a opção selecionada é válida ou não. 
            return _corretivoRepository.GetOpcaoValida(objID, opcao, opcaomarcada, retornoGridOrAreaServico);
        }
        public IEnumerable<CorretivoView> GetListCorretivoCompra(String IDArea, Guid IDSafra, string optionCorretivo, string optionPerfil, string Tipo, string numServico)
        {
            return _corretivoRepository.GetListCorretivoCompra(IDArea, IDSafra, optionCorretivo, optionPerfil, Tipo, numServico);
        }
        public IEnumerable<CorretivoView> UpdateListCorretivo(double prnt, double perCaO, double Mgo, double P2O5, double K2O5, String Corretivoalt, String Corretivoant, double eficiencia, String IDPropriedade, String IDSafra, double S, double Ca)
        {
            return _corretivoRepository.UpdateListCorretivo(prnt, perCaO, Mgo, P2O5, K2O5, Corretivoalt, Corretivoant, eficiencia, IDPropriedade, IDSafra, S, Ca);
        }
        public IEnumerable<string> GetRelatorioListParcialCorretivos(String IDArea, String IDSafra, String CorretivoSelecionado, String IDCorretivoSelecionado, String IDPropriedade, String numServico, String opcaoPerfil, String opcaoCorretivo, String opcaoFertilizante, String Fertilizante, String IDFertilizante, String Tipo)
        {
            return _corretivoRepository.GetRelatorioListParcialCorretivos(IDArea, IDSafra, CorretivoSelecionado, IDCorretivoSelecionado, IDPropriedade, numServico, opcaoPerfil, opcaoCorretivo, opcaoFertilizante, Fertilizante, IDFertilizante, Tipo);
        }
        public int PostCorretivo(double prnt, double perCaO, double Mgo, double P2O5, double K2O5, String Corretivoalt, String Corretivoant, double eficiencia, double S, double Ca)
        {
            return _corretivoRepository.PostCorretivo(prnt, perCaO, Mgo, P2O5, K2O5, Corretivoalt, Corretivoant, eficiencia, S, Ca);
        }

        #region Recomendação alteração de corretivo. 

        public IEnumerable<CorretivoView> GetAllCorretivoSelectAlteracao(Guid? IDArea, Guid IDPropriedade, Guid IDSafra, int Type)
        {
            return _corretivoRepository.GetAllCorretivoSelectAlteracao(IDArea, IDPropriedade, IDSafra, Type);
        }

        public IEnumerable<CorretivoView> GetListOpcao(Guid IDAreaServico)
        {
            return _corretivoRepository.GetListOpcao(IDAreaServico);

        }
        public IEnumerable<CorretivoView> ReplicarCorretivo(IEnumerable<CorretivoView> obj)
        {
            return _corretivoRepository.ReplicarCorretivo(obj);

        }
        public IEnumerable<CorretivoView> GetListAlteracaoCorretivoNew(string IDAreaServico, string eficiencia, string corretivo, string prnt, string perCaO, string perMgO, string perP2O5, string perK2O, string s, string perCa, string neweficiencia, string newCorretivo, string newprnt, string newpercao, string newpermgo, string newperp2o5, string newperk2o, string newpers, string newperca, string dosefixa, Boolean tipoFosforom, Boolean rbSaturacao, Boolean RBParcial, Boolean rbMagnesio, Boolean rbPotassio, Boolean rbFosforo)
        {
            Guid IDArea = new Guid(IDAreaServico);
            IEnumerable<CorretivoView> GetListCorretivosByZM = _corretivoRepository.GetListCorretivo(IDArea, corretivo, prnt, perCaO, perMgO, perP2O5, perK2O, s, perCa);

            Double[] pers = new Double[4];

            AreaServicoView oAreaServico = _areaServicoRepository.FindFullAreaServico(IDArea, null, null, false);
            AreaView oArea = _areaRepository.GetFullArea(Guid.Parse(oAreaServico.IDArea.ToString()));
            Safra oSafra = _safraRepository.Find(Guid.Parse(oAreaServico.IDSafra.ToString()));

            Area area = null;
            Safra safra = null;
            GridViewer oGrid = null;

            CorretivoView oNovoCorretivo = new CorretivoView();
            oNovoCorretivo.Corretivo = newCorretivo;
            oNovoCorretivo.eficiencia = Double.Parse(neweficiencia.ToString().Replace('.', ','));
            oNovoCorretivo.prnt = Double.Parse(newprnt.ToString().Replace('.', ','));
            oNovoCorretivo.perCaO = Double.Parse(newpercao.ToString().Replace('.', ','));
            oNovoCorretivo.perMgO = Double.Parse(newpermgo.ToString().Replace('.', ','));
            oNovoCorretivo.perP2O5 = Double.Parse(newperp2o5.ToString().Replace('.', ','));
            oNovoCorretivo.perK2O = Double.Parse(newperk2o.ToString().Replace('.', ','));
            oNovoCorretivo.perS = Double.Parse(newpers.ToString().Replace('.', ','));
            oNovoCorretivo.perCa = Double.Parse(newperca.ToString().Replace('.', ','));


            Boolean tipoFosforo = false;
            IEnumerable<GridViewer> oListGrid = _gridRepository.GetByAreaServico(IDArea);

            oListGrid = oListGrid.OrderBy(o => o.descricao).ToList();

            List<CorretivoView> lstNovoCorretivo = new List<CorretivoView>();
            int elemento = 0;

            foreach (var item in GetListCorretivosByZM)
            {
                CorretivoView oNewCorretivo = new CorretivoView();
                //oNewCorretivo.objID = item.objID;
                oNewCorretivo.IDGrid = item.IDGrid;

                MediaAnalise oMediaAnalise = new MediaAnalise();

                if (item.IDGrid != null)
                    oMediaAnalise = _analiseSoloService.GetMediaAnaliseSolo(IDArea, item.IDGrid, 0, 1, 0, 0).ToList().FirstOrDefault();
                else
                    oMediaAnalise = _analiseSoloService.GetMediaAnaliseSolo(IDArea, null, 0, 0, 0, 0).ToList().FirstOrDefault();

                if (rbSaturacao)
                {
                    elemento = 1;
                    Double perk = 0;
                    if (Double.Parse(oMediaAnalise.CTCK.ToString()) > 0.05)
                        perk = 0.95;
                    else
                        perk = Double.Parse(oMediaAnalise.CTCK.ToString());

                    Double qtde = 0;

                    if (oNovoCorretivo.prnt > 0 && oNovoCorretivo.perMgO > 0)
                    {
                        Double A, B, C, D, E, F, G, H = 0;
                        Double efCorretivo = Double.Parse(eficiencia) / 100;
                        Double efCorretivoNovo = Double.Parse(neweficiencia) / 100;

                        if (corretivo.Contains("CALC"))
                        {
                            A = ((Double)((item.qtde * ((Double.Parse(perMgO) / 100) * (Double.Parse(prnt) / 100) * efCorretivo) * 0.6833) / 403.06));
                            B = ((Double)((item.qtde * ((Double.Parse(perCaO) / 100) * (Double.Parse(prnt) / 100) * efCorretivo) * 0.6833) / 561.58));
                            E = (Double)((Double.Parse(perMgO) / 100) * (Double.Parse(prnt) / 100) * efCorretivo * 0.6833);
                            F = (Double)((Double.Parse(perCaO) / 100) * (Double.Parse(prnt) / 100) * efCorretivo * 0.6833);
                        }
                        else
                        {
                            A = ((Double)((item.qtde * ((Double.Parse(perMgO) / 100) * (Double.Parse(prnt) / 100) * efCorretivo)) / 403.06));
                            B = ((Double)((item.qtde * ((Double.Parse(perCaO) / 100) * (Double.Parse(prnt) / 100) * efCorretivo)) / 561.58));
                            E = (Double)((Double.Parse(perMgO) / 100) * (Double.Parse(prnt) / 100) * efCorretivo);
                            F = (Double)((Double.Parse(perCaO) / 100) * (Double.Parse(prnt) / 100) * efCorretivo);
                        }
                        if (newCorretivo.Contains("CALC"))
                        {
                            C = ((Double)((1000 * ((oNovoCorretivo.perCaO / 100) * (oNovoCorretivo.prnt / 100) * efCorretivoNovo) * 0.6833) / 561.58));
                            D = ((Double)((1000 * ((oNovoCorretivo.perMgO / 100) * (oNovoCorretivo.prnt / 100) * efCorretivoNovo) * 0.6833) / 403.06));
                            G = (Double)((oNovoCorretivo.perCaO / 100) * (oNovoCorretivo.prnt / 100) * efCorretivoNovo * 0.6833);
                            H = (Double)((oNovoCorretivo.perMgO / 100) * (oNovoCorretivo.prnt / 100) * efCorretivoNovo * 0.6833);
                        }
                        else
                        {
                            C = ((Double)((1000 * ((oNovoCorretivo.perCaO / 100) * (oNovoCorretivo.prnt / 100) * efCorretivoNovo)) / 561.58));
                            D = ((Double)((1000 * ((oNovoCorretivo.perMgO / 100) * (oNovoCorretivo.prnt / 100) * efCorretivoNovo)) / 403.06));
                            G = (Double)((oNovoCorretivo.perCaO / 100) * (oNovoCorretivo.prnt / 100) * efCorretivoNovo);
                            H = (Double)((oNovoCorretivo.perMgO / 100) * (oNovoCorretivo.prnt / 100) * efCorretivoNovo);
                        }

                        Double equivale = (E + F) / (G + H);
                        Double DoseAntigo = 0;

                        if (RBParcial)
                        {
                            qtde = double.Parse(dosefixa);
                            DoseAntigo = (Double)(item.qtde - (qtde * equivale));
                            if (DoseAntigo < 250)
                                DoseAntigo = 0;
                            if (DoseAntigo >= 250 && DoseAntigo < 500)
                                DoseAntigo = 500;
                            item.qtde = DoseAntigo;
                        }
                        else
                        {
                            qtde = 1000 * ((A + B) / (C + D));
                        }

                        if (newCorretivo.Contains("CALC"))
                        {
                            if (qtde < 500)
                            {
                                //MessageBox.Show("A dose fixa não pode ser menor que 500.", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                dosefixa = "500";
                                qtde = 500;
                            }
                        }
                        else
                        {
                            if (qtde < 60)
                                qtde = 60;
                        }
                        qtde = Math.Round(qtde);
                    }

                    oNewCorretivo.KgHa = qtde;
                }


                if (rbMagnesio)
                {
                    elemento = 2;
                    Double qtde = 0;
                    Double efCorretivo = Double.Parse(eficiencia) / 100;
                    if (oNovoCorretivo.prnt > 0 && oNovoCorretivo.perMgO > 0)
                    {
                        Double equivale = (Double)((Double.Parse(prnt) / 100 * Double.Parse(perMgO) * efCorretivo) /
                                         (oNovoCorretivo.prnt / 100 * oNovoCorretivo.perMgO / 100 * oNovoCorretivo.eficiencia / 100));
                        Double DoseAntigo = 0;

                        if (RBParcial)
                        {
                            qtde = double.Parse(dosefixa);
                            DoseAntigo = (Double)(item.qtde - (qtde * equivale));
                            if (DoseAntigo < 30)
                                DoseAntigo = 0;
                            if (DoseAntigo >= 30 && DoseAntigo < 60)
                                DoseAntigo = 60;
                            item.qtde = DoseAntigo;
                        }
                        else
                        {
                            qtde = (Double)(((((Double.Parse(prnt) * (Double.Parse(perMgO) * (efCorretivo))) / 100) /
                                             ((oNovoCorretivo.prnt * (oNovoCorretivo.perMgO * (oNovoCorretivo.eficiencia / 100))) / 100)) * qtde)); ;
                        }

                        if (qtde < 60)
                            qtde = 60;

                        qtde = Math.Round(qtde);
                    }
                    oNewCorretivo.KgHa = qtde;
                }

                if (rbPotassio)
                {
                    elemento = 3;
                    Double qtde = 0;
                    Double efCorretivo = Double.Parse(eficiencia) / 100;
                    if (oNovoCorretivo.perK2O > 0)
                    {
                        Double equivale = (Double)((Double.Parse(perK2O) * (Double.Parse(eficiencia) / 100)) / (oNovoCorretivo.perK2O * (oNovoCorretivo.eficiencia / 100)));
                        Double DoseAntigo = 0;

                        if (RBParcial)
                        {
                            qtde = double.Parse(dosefixa);
                            DoseAntigo = (Double)(item.qtde - (qtde * equivale));
                            if (DoseAntigo < 45)
                                DoseAntigo = 0;
                            if (DoseAntigo >= 45 && DoseAntigo < 60)
                                DoseAntigo = 60;
                            item.qtde = DoseAntigo;
                        }
                        else
                        {
                            qtde = (Double)((item.qtde * ((item.perK2O * efCorretivo) / (oNovoCorretivo.perK2O * (oNovoCorretivo.eficiencia / 100)))));
                        }

                        if (qtde < 60)
                            qtde = 60;

                        qtde = Math.Round(qtde);
                    }

                    oNewCorretivo.KgHa = qtde;
                }

                if (rbFosforo)
                {
                    elemento = 4;
                    Double qtde = 0;
                    if (oNovoCorretivo.perP2O5 > 0)
                    {

                        Double equivale = (Double)((item.perP2O5 * (item.eficiencia / 100)) / (oNovoCorretivo.perP2O5 * (oNovoCorretivo.eficiencia / 100)));
                        if (Double.IsNaN(equivale))
                            equivale = 0;

                        Double DoseAntigo = 0;

                        if (RBParcial)
                        {
                            qtde = double.Parse(dosefixa);
                            DoseAntigo = (Double)(item.qtde - (qtde * equivale));
                            if (DoseAntigo < 30)
                                DoseAntigo = 0;
                            if (DoseAntigo >= 30 && DoseAntigo < 60)
                                DoseAntigo = 60;
                            item.qtde = DoseAntigo;
                        }
                        else
                        {
                            qtde = (Double)((item.qtde * ((item.perP2O5 * (item.eficiencia / 100)) / (oNovoCorretivo.perP2O5 * (oNovoCorretivo.eficiencia / 100)))));
                            if (Double.IsNaN(qtde))
                                qtde = 0;

                        }

                        if (qtde < 60)
                            qtde = 60;

                        qtde = Math.Round(qtde);
                    }
                    oNewCorretivo.KgHa = qtde;
                }

                oNewCorretivo.Corretivo = oNovoCorretivo.Corretivo;
                oNewCorretivo.prnt = oNovoCorretivo.prnt;
                oNewCorretivo.perCaO = oNovoCorretivo.perCaO;
                oNewCorretivo.perCa = oNovoCorretivo.perCa;
                oNewCorretivo.perMgO = oNovoCorretivo.perMgO;
                oNewCorretivo.perP2O5 = oNovoCorretivo.perP2O5;
                oNewCorretivo.perK2O = oNovoCorretivo.perK2O;
                oNewCorretivo.perS = oNovoCorretivo.perS;
                oNewCorretivo.eficiencia = oNovoCorretivo.eficiencia;
                lstNovoCorretivo.Add(oNewCorretivo);
            }

            List<CorretivoView> ListEnd = new List<CorretivoView>();
            ListEnd = _corretivoRepository.CorretivoFinal(IDArea, lstNovoCorretivo);
            ListEnd.AddRange(lstNovoCorretivo);

            List<CorretivoView> lst = new List<CorretivoView>();
            for (int i = 0; i < lstNovoCorretivo.Count(); i++)
            {
                CorretivoView oNewProduto = new CorretivoView();

                //oNewProduto.objID = lstNovoCorretivo[i].objID;
                oNewProduto.Area = oArea.nome;
                oNewProduto.KgHa = (Double)lstNovoCorretivo[i].KgHa;
                //oNewProduto.Perfil = (Double)pListCorretivo[i].perfil;

                MediaAnalise oMediaAreaZM = new MediaAnalise();
                CorretivoView oMediaCorretivo = new CorretivoView();
                Corretivo oMediaCorretivoArea = new Corretivo();

                if (lstNovoCorretivo[i].IDGrid != null)
                {

                    oGrid = oListGrid.Where(o => o.objID.Equals(lstNovoCorretivo[i].IDGrid)).FirstOrDefault();

                    oNewProduto.ZM = oGrid.descricao;

                    oNewProduto.TamanhoZM = double.Parse(oGrid.tamanho.ToString());
                    oMediaAreaZM = _analiseSoloService.GetMediaAnaliseSolo(IDArea, oGrid.objID, 0, 1, 0, 0).ToList().FirstOrDefault();

                    oMediaCorretivo = _corretivoRepository.MediaGridAlterado(IDArea, Guid.Parse(oGrid.objID.ToString()));
                    if (oNewProduto.ZM.Equals("ZM 04"))
                    {

                    }
                    //ParametroPropriedade paramP = pParametroPropriedade.ReturnAll(oArea.IDPropriedadeRural, oSafra.objID).FirstOrDefault();


                    //if (paramP != null && !String.IsNullOrEmpty(paramP.tipoFosforo))
                    //    tipoFosforo = (paramP.tipoFosforo.Equals("Res") ? true : false);

                    List<CorretivoView> lstCorretivo = null;
                    if (ListEnd != null)
                    {
                        lstCorretivo = ListEnd.Where(o => o.IDGrid == oGrid.objID).ToList();

                        oNewProduto.P = cCorretivo.NivelP(oMediaAreaZM, cCorretivo.ReturnMediaAlteracao(lstCorretivo), tipoFosforo);
                    }
                    else
                    {
                        oNewProduto.P = cCorretivo.NivelP(oMediaAreaZM, oMediaCorretivo, tipoFosforo);
                    }
                    int ef = 100;

                    if (lstNovoCorretivo[i].eficiencia != null)
                    {
                        ef = (int)lstNovoCorretivo[i].eficiencia;
                    }

                    pers = cCorretivo.ReturnNiveisBasesGrid(oMediaAreaZM, oMediaCorretivo);
                    oNewProduto.Ca = pers[0];
                    oNewProduto.Mg = pers[1];
                    oNewProduto.K = pers[2];
                    oNewProduto.V = pers[3];
                    oNewProduto.S = pers[4];
                }
                else
                {
                    //if (lstNovoCorretivo[i].perfil == 1)
                    //{
                    //    oMediaAreaZM = cAnaliseSolo.ReturnMedia_20_40(oArea, oSafra);
                    //    oMediaCorretivo = cCorretivo.ReturnMediaAreaAlterado(oArea, oSafra, 1);
                    //}
                    //else
                    //{
                    //    oMediaAreaZM = cAnaliseSolo.ReturnMedia(oArea, oSafra);
                    //    oMediaCorretivo = cCorretivo.ReturnMediaAreaAlterado(oArea, oSafra, 0);
                    //}

                    //oNewProduto.ZM = oArea.descricao + " - TX. FIXA";
                    //oNewProduto.Tamanho = pVAreaSafra.Load(oArea, oSafra).tamanho;
                    //pers = cCorretivo.ReturnNiveisBasesGrid(oMediaAreaZM, oMediaCorretivo);
                    //oNewProduto.perCa = pers[0];
                    //oNewProduto.perMg = pers[1];
                    //oNewProduto.perK = pers[2];
                    //oNewProduto.perV = pers[3];
                    //oNewProduto.perS = pers[4];

                    //if (pListFinalCorretivo != null)
                    //{
                    //    oNewProduto.perP = cCorretivo.NivelP(oMediaAreaZM, cCorretivo.ReturnMediaAlteracao(pListFinalCorretivo), tipoFosforo);
                    //}
                    //else
                    //{
                    //    oNewProduto.perP = cCorretivo.NivelP(oMediaAreaZM, oMediaCorretivo, tipoFosforo);
                    //}
                }

                lst.Add(oNewProduto);
            }

            ListEnd = new List<CorretivoView>();
            ListEnd = lst.OrderBy(o => o.ZM).ToList();

            return ListEnd;
        }

        #endregion

        #region Recomendação tela de correção/ análise/ perfil/ ciclos.
        public IEnumerable<Corretivo> GetListCorretivo(Guid? IDAreaServico, Guid? IDGrid, Guid? IDPropriedade, Guid? IDSafra, int opcao, int perfil, int corretivo)
        {
            return _corretivoRepository.GetListCorretivo(IDAreaServico, IDGrid, IDPropriedade, IDSafra, opcao, perfil, corretivo);
        }

        public int GetCountRegistros(Guid objID, int Type)
        {
            return _corretivoRepository.GetCountRegistros(objID, Type);
        }
        public ResultadoCorrecao RetornaResultadoCorrecao(Guid IDAreaServico, Guid? IDGrid, String Profundidade, int unid, int tipo, int RetornoP,  string Corretivo, int Opcao)
        {

            return _corretivoRepository.RetornaResultadoCorrecao(IDAreaServico, IDGrid, Profundidade, unid,  tipo, RetornoP, Corretivo, Opcao);
        }
   

        public bool UpdateCorretivoOpcaoMaracada(Guid objID, int opcao, int perfil, bool MarcarOrDesmarcar)
        {

            return _corretivoRepository.UpdateCorretivoOpcaoMaracada(objID, opcao, perfil, MarcarOrDesmarcar);
        }

        public bool UpdateOpcaoMarcada(Guid objID, bool MarcarOrDesmarcar)
        {
            return _corretivoRepository.UpdateOpcaoMarcada(objID, MarcarOrDesmarcar);
        }

        public CorretivoView GetOpcaoMarcadaByOpcao(Guid objID, int? opcao, int opcaomarcada, int retornoGridOrAreaServico)
        {
            return _corretivoRepository.GetOpcaoMarcadaByOpcao(objID, opcao, opcaomarcada, retornoGridOrAreaServico);
        }

        public Corretivo UpdateAdicaoCorretivo(object corretivo)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOptionChecked(Guid objID, int Opcao, bool MarcarOrDesmarcar, int type)
        {
            return _corretivoRepository.UpdateOptionChecked(objID , Opcao, MarcarOrDesmarcar, type); 
        }

        public bool DeleteAllCorretivoByOption(Guid objID, int Option, int Type)
        {
            return _corretivoRepository.DeleteAllCorretivoByOption(objID, Option, Type);
        }

        public IEnumerable<Options> GetListOpcaoCorretivo(Guid objID, int Type, int perfil)
        {
            return _corretivoRepository.GetListOpcaoCorretivo(objID, Type, perfil); 
        }

        public bool UpdateMarcado(Guid IDAreaServico, Guid IDGrid, int opcao, bool chk)
        {
            return _corretivoRepository.UpdateMarcado(IDAreaServico, IDGrid, opcao, chk);
        }

        public CorretivoView GetMediaCorretivo(Guid IDAreaServico, Guid? IDGrid, string profundidade, int tipo, int opcao, int perfil)
        {
            return _corretivoRepository.GetMediaCorretivo(IDAreaServico, IDGrid, profundidade, tipo, opcao, perfil); 
        }

        public bool SetDivideDose(SetDivideDoseCorretivo obj)
        {
            return _corretivoRepository.SetDivideDose(obj); 
        }

        public bool CheckedAnaliseSolo2040(Guid IDAreaServico)
        {
            return _corretivoRepository.CheckedAnaliseSolo2040(IDAreaServico); 
        }

        #endregion

    }
}
