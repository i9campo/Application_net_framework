using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sigma.Domain.Services
{
    public class AmostraFoliarService : Service<AmostraFoliar>, IAmostraFoliarService
    {
        #region Properties
        private NivelSolo[] Niveis { get; set; }
        #endregion

        #region Services
        private readonly IFaixaTeorService _faixaTeorService;
        private readonly INivelSoloService _nivelSoloService;
        private readonly ITeorFoliarService _teorFoliarService;
        private readonly ITeorSoloService _teorSoloService;
        private readonly IRecomendacaoFoliarService _recomendacaoFoliarService;
        private readonly IAmostraFoliarRepository _repository;
        #endregion

        #region Ctor
        public AmostraFoliarService(IAmostraFoliarRepository _repository, IFaixaTeorService faixaTeorService, INivelSoloService nivelSoloService,
            ITeorFoliarService teorFoliarService, ITeorSoloService teorSoloService, IRecomendacaoFoliarService recomendacaoFoliarService)
            : base(_repository)
        {
            _faixaTeorService = faixaTeorService;
            _nivelSoloService = nivelSoloService;
            _teorFoliarService = teorFoliarService;
            _teorSoloService = teorSoloService;
            _recomendacaoFoliarService = recomendacaoFoliarService;
        }
        #endregion

        //#region InterfaceMethods

        //public IEnumerable<RecomendacaoFoliar> GetRecomendacao(AmostraFoliar amostraF)
        //{
        //    List<RecomendacaoFoliar> lst = new List<RecomendacaoFoliar>();
        //    List<AuxCodigoBalancoFoliar> codes = GetMatrix(amostraF);

        //    RecomendacaoFoliar n = _recomendacaoFoliarService.LoadBy(codes[0].codigo, "N ", false, amostraF.EstagioCultura.IDCultura);

        //    if (n == null)
        //    {
        //        string newCode = "" + codes[0].N0 + codes[0].N1 + "000000000000000000000000";
        //        n = _recomendacaoFoliarService.LoadBy(newCode, "N ", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(n);

        //    RecomendacaoFoliar p = _recomendacaoFoliarService.LoadBy(codes[1].codigo, "P ", false, amostraF.EstagioCultura.IDCultura);
        //    if (p == null)
        //    {
        //        string newCode = "00" + codes[1].P0 + codes[1].P1 + "0000000000000000000000";
        //        p = _recomendacaoFoliarService.LoadBy(newCode, "P ", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(p);

        //    RecomendacaoFoliar k = _recomendacaoFoliarService.LoadBy(codes[2].codigo, "K ", false, amostraF.EstagioCultura.IDCultura);
        //    if (k == null)
        //    {
        //        string newCode = "0000" + codes[2].K0 + codes[2].K1 + "00000000000000000000";
        //        k = _recomendacaoFoliarService.LoadBy(newCode, "K ", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(k);

        //    RecomendacaoFoliar s = _recomendacaoFoliarService.LoadBy(codes[5].codigo, "S ", false, amostraF.EstagioCultura.IDCultura);
        //    if (s == null)
        //    {
        //        string newCode = "0000000000" + codes[5].S0 + codes[5].S1 + "00000000000000";
        //        s = _recomendacaoFoliarService.LoadBy(newCode, "S ", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(s);

        //    RecomendacaoFoliar ca = _recomendacaoFoliarService.LoadBy(codes[3].codigo, "Ca", false, amostraF.EstagioCultura.IDCultura);
        //    if (ca == null)
        //    {
        //        string newCode = "000000" + codes[3].Ca0 + codes[3].Ca1 + "000000000000000000";
        //        ca = _recomendacaoFoliarService.LoadBy(newCode, "Ca", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(ca);

        //    RecomendacaoFoliar mg = _recomendacaoFoliarService.LoadBy(codes[4].codigo, "Mg", false, amostraF.EstagioCultura.IDCultura);
        //    if (mg == null)
        //    {
        //        string newCode = "00000000" + codes[4].Mg0 + codes[4].Mg1 + "0000000000000000";
        //        mg = _recomendacaoFoliarService.LoadBy(newCode, "Mg", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(mg);

        //    RecomendacaoFoliar b = _recomendacaoFoliarService.LoadBy(codes[6].codigo, "B ", false, amostraF.EstagioCultura.IDCultura);
        //    if (b == null)
        //    {
        //        string newCode = "000000000000" + codes[6].B0 + codes[6].B1 + "000000000000";
        //        b = _recomendacaoFoliarService.LoadBy(newCode, "B ", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(b);

        //    RecomendacaoFoliar zn = _recomendacaoFoliarService.LoadBy(codes[7].codigo, "Zn", false, amostraF.EstagioCultura.IDCultura);
        //    if (zn == null)
        //    {
        //        string newCode = "00000000000000" + codes[7].Zn0 + codes[7].Zn1 + "0000000000";
        //        zn = _recomendacaoFoliarService.LoadBy(newCode, "Zn", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(zn);

        //    RecomendacaoFoliar fe = _recomendacaoFoliarService.LoadBy(codes[9].codigo, "Fe", false, amostraF.EstagioCultura.IDCultura);
        //    if (fe == null)
        //    {
        //        string newCode = "000000000000000000" + codes[9].Fe0 + codes[9].Fe1 + "000000";
        //        fe = _recomendacaoFoliarService.LoadBy(newCode, "Fe", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(fe);

        //    RecomendacaoFoliar cu = _recomendacaoFoliarService.LoadBy(codes[10].codigo, "Cu", false, amostraF.EstagioCultura.IDCultura);
        //    if (cu == null)
        //    {
        //        string newCode = "00000000000000000000" + codes[10].Cu0 + codes[10].Cu1 + "0000";
        //        cu = _recomendacaoFoliarService.LoadBy(newCode, "Cu", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(cu);

        //    RecomendacaoFoliar mn = _recomendacaoFoliarService.LoadBy(codes[8].codigo, "Mn", false, amostraF.EstagioCultura.IDCultura);
        //    if (mn == null)
        //    {
        //        string newCode = "0000000000000000" + codes[8].Mn0 + codes[8].Mn1 + "00000000";
        //        mn = _recomendacaoFoliarService.LoadBy(newCode, "Mn", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(mn);

        //    RecomendacaoFoliar mo = _recomendacaoFoliarService.LoadBy(codes[11].codigo, "Mo", false, amostraF.EstagioCultura.IDCultura);
        //    if (mo == null)
        //    {
        //        string newCode = "0000000000000000000000" + codes[11].Mo0 + codes[11].Mo1 + "00";
        //        mo = _recomendacaoFoliarService.LoadBy(newCode, "Mo", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(mo);

        //    RecomendacaoFoliar co = _recomendacaoFoliarService.LoadBy(codes[12].codigo, "Co", false, amostraF.EstagioCultura.IDCultura);
        //    if (co == null)
        //    {
        //        string newCode = "000000000000000000000000" + codes[12].Co0 + codes[12].Co1;
        //        co = _recomendacaoFoliarService.LoadBy(newCode, "Co", true, amostraF.EstagioCultura.IDCultura);
        //    }
        //    lst.Add(co);

        //    lst.RemoveAll(o => o == null);


        //    if (lst.Count == 0)
        //    {
        //        RecomendacaoFoliar todos = new RecomendacaoFoliar();
        //        todos.elemento = "Todos Nutrientes";
        //        todos.descritivo = "Todos nutrientes tiveram suprimento adequado às plantas, recomendando-se manter o atual manejo adotado. Exceto para aqueles que estiverem altos ou baixos no solo.";
        //        lst.Add(todos);
        //    }
        //    else if (lst.Count != 12)
        //    {
        //        RecomendacaoFoliar demais = new RecomendacaoFoliar();
        //        demais.elemento = "Demais Nutrientes";
        //        demais.descritivo = "Os demais nutrientes tiveram suprimento adequado às plantas, apesar dos teores altos ou baixos no solo, recomenda-se manter o atual manejo adotado.";
        //        lst.Add(demais);
        //    }

        //    return lst;
        //}

        //public double IndiceBF(AmostraFoliar amostraF)
        //{
        //    TeorFoliar TeorF = _teorFoliarService.GetMedia(amostraF);

        //    Double soma = 0;
        //    EstagioCultura oEstagioCultura = amostraF.EstagioCultura;
        //    PartePlanta oPartePlanta = amostraF.PartePlanta;

        //    soma += CalcNivel("N", (Double)TeorF.n, oEstagioCultura, oPartePlanta);
        //    soma += CalcNivel("P", (Double)TeorF.p, oEstagioCultura, oPartePlanta);
        //    soma += CalcNivel("K", (Double)TeorF.k, oEstagioCultura, oPartePlanta);
        //    soma += CalcNivel("S", (Double)TeorF.s, oEstagioCultura, oPartePlanta);
        //    soma += CalcNivel("Ca", (Double)TeorF.ca, oEstagioCultura, oPartePlanta);
        //    soma += CalcNivel("Mg", (Double)TeorF.mg, oEstagioCultura, oPartePlanta);
        //    soma += CalcNivel("B", (Double)TeorF.b, oEstagioCultura, oPartePlanta);
        //    soma += CalcNivel("Zn", (Double)TeorF.zn, oEstagioCultura, oPartePlanta);
        //    soma += CalcNivel("Fe", (Double)TeorF.fe, oEstagioCultura, oPartePlanta);
        //    soma += CalcNivel("Cu", (Double)TeorF.cu, oEstagioCultura, oPartePlanta);
        //    soma += CalcNivel("Mn", (Double)TeorF.mn, oEstagioCultura, oPartePlanta);
        //    soma += CalcNivel("Mo", (Double)TeorF.mo, oEstagioCultura, oPartePlanta);

        //    return soma;
        //}

        //public IEnumerable<AuxNivelElemento> GetNiveisNutrigrama(AmostraFoliar amostraF)
        //{
        //    TeorFoliar TeorF = _teorFoliarService.GetMedia(amostraF);
        //    EstagioCultura oEstagioCultura = amostraF.EstagioCultura;
        //    PartePlanta oPartePlanta = amostraF.PartePlanta;

        //    List<AuxNivelElemento> lst = new List<AuxNivelElemento>();
        //    lst.Add(new AuxNivelElemento("N", CalcNutrigrama("N", (Double)TeorF.n, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("P", CalcNutrigrama("P", (Double)TeorF.p, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("K", CalcNutrigrama("K", (Double)TeorF.k, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("S", CalcNutrigrama("S", (Double)TeorF.s, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Ca", CalcNutrigrama("Ca", (Double)TeorF.ca, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Mg", CalcNutrigrama("Mg", (Double)TeorF.mg, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("B", CalcNutrigrama("B", (Double)TeorF.b, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Zn", CalcNutrigrama("Zn", (Double)TeorF.zn, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Fe", CalcNutrigrama("Fe", (Double)TeorF.fe, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Cu", CalcNutrigrama("Cu", (Double)TeorF.cu, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Mn", CalcNutrigrama("Mn", (Double)TeorF.mn, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Mo", CalcNutrigrama("Mo", (Double)TeorF.mo, oEstagioCultura, oPartePlanta)));

        //    return lst;
        //}

        //public IEnumerable<AuxNivelElemento> GetIndicadorBF(AmostraFoliar amostraF)
        //{
        //    TeorFoliar TeorF = _teorFoliarService.GetMedia(amostraF);
        //    EstagioCultura oEstagioCultura = amostraF.EstagioCultura;
        //    PartePlanta oPartePlanta = amostraF.PartePlanta;

        //    List<AuxNivelElemento> lst = new List<AuxNivelElemento>();
        //    lst.Add(new AuxNivelElemento("N", CalcIndice("N", (Double)TeorF.n, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("P", CalcIndice("P", (Double)TeorF.p, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("K", CalcIndice("K", (Double)TeorF.k, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("S", CalcIndice("S", (Double)TeorF.s, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Ca", CalcIndice("Ca", (Double)TeorF.ca, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Mg", CalcIndice("Mg", (Double)TeorF.mg, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("B", CalcIndice("B", (Double)TeorF.b, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Zn", CalcIndice("Zn", (Double)TeorF.zn, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Fe", CalcIndice("Fe", (Double)TeorF.fe, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Cu", CalcIndice("Cu", (Double)TeorF.cu, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Mn", CalcIndice("Mn", (Double)TeorF.mn, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Mo", CalcIndice("Mo", (Double)TeorF.mo, oEstagioCultura, oPartePlanta)));

        //    return lst;
        //}

        //public IEnumerable<AuxNivelElemento> GetBalancoSolo(AmostraFoliar amostraF)
        //{
        //    TeorSolo teorSolo = _teorSoloService.GetMedia(amostraF);
        //    EstagioCultura oEstagioCultura = amostraF.EstagioCultura;
        //    PartePlanta oPartePlanta = amostraF.PartePlanta;

        //    List<AuxNivelElemento> lst = new List<AuxNivelElemento>();
        //    lst.Add(new AuxNivelElemento("N", CalcIndice("N", (Double)teorSolo.n, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("P", CalcIndice("P", (Double)teorSolo.p, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("K", CalcIndice("K", (Double)teorSolo.k, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("S", CalcIndice("S", (Double)teorSolo.s, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Ca", CalcIndice("Ca", (Double)teorSolo.ca, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Mg", CalcIndice("Mg", (Double)teorSolo.mg, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("B", CalcIndice("B", (Double)teorSolo.b, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Zn", CalcIndice("Zn", (Double)teorSolo.zn, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Fe", CalcIndice("Fe", (Double)teorSolo.fe, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Cu", CalcIndice("Cu", (Double)teorSolo.cu, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Mn", CalcIndice("Mn", (Double)teorSolo.mn, oEstagioCultura, oPartePlanta)));
        //    lst.Add(new AuxNivelElemento("Mo", CalcIndice("Mo", (Double)teorSolo.mo, oEstagioCultura, oPartePlanta)));

        //    return lst;
        //}

        //public IEnumerable<AuxNivelElemento> GetNiveisSolo(AmostraFoliar amostraF)
        //{
        //    TeorSolo bn = _teorSoloService.GetMedia(amostraF);
        //    NivelSolo[] arrayN = _nivelSoloService.GetSolo(amostraF.EstagioCultura.Cultura);

        //    List<AuxNivelElemento> lst = new List<AuxNivelElemento>();

        //    for (int i = 0; i < arrayN.Length; i++)
        //    {
        //        if (arrayN[i] == null)
        //        {
        //            return new List<AuxNivelElemento>();
        //        }
        //        lst.Add(GetNivelSolo(bn, arrayN[i]));
        //    }

        //    return lst;
        //}

        //public IEnumerable<AuxNivelElemento> GetIndiceSolo(AmostraFoliar amostraF)
        //{
        //    TeorSolo bn = _teorSoloService.GetMedia(amostraF);
        //    NivelSolo[] arrayN = _nivelSoloService.GetSolo(amostraF.EstagioCultura.Cultura);

        //    List<AuxNivelElemento> lst = new List<AuxNivelElemento>();

        //    for (int i = 0; i < arrayN.Length; i++)
        //    {
        //        if (arrayN[i] == null)
        //        {
        //            return new List<AuxNivelElemento>();
        //        }
        //        lst.Add(GetIndiceSolo(arrayN[i], bn));
        //    }

        //    return lst;
        //}

        //#endregion

        //#region Nutrigrama
        //public double CalcNutrigrama(string nutriente, double valor, EstagioCultura ec, PartePlanta pp)
        //{
        //    List<FaixaTeor> faixaTeores = _faixaTeorService.GetBy(nutriente, ec, pp).ToList();

        //    if (faixaTeores.Count() > 0)
        //    {
        //        if (valor == 0)
        //            return 0;
        //        else if (valor < faixaTeores[0].nivel1)
        //            return (valor / (Double)faixaTeores[0].nivel1);
        //        else if (valor < faixaTeores[0].nivel2)
        //            return 1 + (valor - (Double)faixaTeores[0].nivel1) / ((Double)faixaTeores[0].nivel2 - (Double)faixaTeores[0].nivel1);
        //        else if (valor < faixaTeores[0].nivel3)
        //            return 2 + (valor - (Double)faixaTeores[0].nivel2) / ((Double)faixaTeores[0].nivel3 - (Double)faixaTeores[0].nivel2);
        //        else if (valor < faixaTeores[0].nivel4)
        //            return 3 + (valor - (Double)faixaTeores[0].nivel3) / ((Double)faixaTeores[0].nivel4 - (Double)faixaTeores[0].nivel3);
        //        else
        //            return 4 + (valor - (Double)faixaTeores[0].nivel4) / ((Double)faixaTeores[0].nivel4);
        //    }
        //    return 0;
        //}

        //public double CalcIndice(string nutriente, double valor, EstagioCultura ec, PartePlanta pp)
        //{
        //    List<FaixaTeor> faixaTeores = _faixaTeorService.GetBy(nutriente, ec, pp).ToList();

        //    if (faixaTeores.Count > 0)
        //    {
        //        if (valor == 0 || (Double)faixaTeores[0].nivel2 == 0 || (Double)faixaTeores[0].nivel3 == 0)
        //            return 0;
        //        else if (valor > (Double)faixaTeores[0].nivel3)
        //        {
        //            if ((valor - (Double)faixaTeores[0].nivel3) / (Double)faixaTeores[0].nivel3 > 1)
        //                return 100;
        //            else
        //                return ((valor - (Double)faixaTeores[0].nivel3) / (Double)faixaTeores[0].nivel3) * 100;
        //        }
        //        else if (valor == (Double)faixaTeores[0].nivel3)
        //            return 0;
        //        else if (valor > (Double)faixaTeores[0].nivel2)
        //            return 0;
        //        else
        //            return ((valor - (Double)faixaTeores[0].nivel2) / (Double)faixaTeores[0].nivel2) * 100;
        //    }
        //    return 0;
        //}

        //public Double CalcNivel(string nutriente, double valor, EstagioCultura ec, PartePlanta pp)
        //{
        //    List<FaixaTeor> faixaTeores = _faixaTeorService.GetBy(nutriente, ec, pp).ToList();
        //    Double var;

        //    if (faixaTeores.Count > 0)
        //    {
        //        if (valor == 0 || (Double)faixaTeores[0].nivel3 == 0)
        //            var = 0.01;
        //        else if (valor > (Double)faixaTeores[0].nivel3)
        //            var = (((Double)faixaTeores[0].nivel2 + (Double)faixaTeores[0].nivel3) / 2) / valor;
        //        else if (valor < (Double)faixaTeores[0].nivel2)
        //            var = valor / (((Double)faixaTeores[0].nivel2 + (Double)faixaTeores[0].nivel3) / 2);
        //        else
        //            var = ((CalcNutrigrama(nutriente, valor, ec, pp) - 2) * 0.2) + 0.8;

        //        return var * 0.833333;
        //    }
        //    return 0;
        //}
        //#endregion

        //#region CoreBalancoFoliar

        //private List<AuxNivelElemento> GetFoliar(AmostraFoliar amostraF)
        //{
        //    EstagioCultura oEstagioCultura = amostraF.EstagioCultura;
        //    PartePlanta oPartePlanta = amostraF.PartePlanta;

        //    TeorFoliar oTeorFoliar = _teorFoliarService.GetMedia(amostraF);

        //    List<AuxNivelElemento> lst = new List<AuxNivelElemento>();
        //    if (oTeorFoliar != null && oEstagioCultura != null && oPartePlanta != null)
        //    {
        //        lst.Add(new AuxNivelElemento("N", CalcNutrigrama("N", (Double)oTeorFoliar.n, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("P", CalcNutrigrama("P", (Double)oTeorFoliar.p, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("K", CalcNutrigrama("K", (Double)oTeorFoliar.k, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("Ca", CalcNutrigrama("Ca", (Double)oTeorFoliar.ca, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("Mg", CalcNutrigrama("Mg", (Double)oTeorFoliar.mg, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("S", CalcNutrigrama("S", (Double)oTeorFoliar.s, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("B", CalcNutrigrama("B", (Double)oTeorFoliar.b, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("Zn", CalcNutrigrama("Zn", (Double)oTeorFoliar.zn, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("Mn", CalcNutrigrama("Mn", (Double)oTeorFoliar.mn, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("Fe", CalcNutrigrama("Fe", (Double)oTeorFoliar.fe, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("Cu", CalcNutrigrama("Cu", (Double)oTeorFoliar.cu, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("Mo", CalcNutrigrama("Mo", (Double)oTeorFoliar.mo, oEstagioCultura, oPartePlanta)));
        //        lst.Add(new AuxNivelElemento("Co", 0));
        //    }

        //    return lst;
        //}

        //private int[,] PreencherSolo(AmostraFoliar amostraF)
        //{
        //    List<TeorSolo> lstBN = amostraF.TeorSolo.ToList();
        //    EstagioCultura ec = amostraF.EstagioCultura;
        //    NivelSolo[] Niveis = _nivelSoloService.GetSolo(ec.Cultura);

        //    TeorSolo bn = _teorSoloService.GetMedia(amostraF);

        //    List<AuxNivelElemento> lst = GetFoliar(amostraF);

        //    int[,] values = new int[13, 26];

        //    for (int x = 0; x < 13; x++)
        //    {
        //        if (Niveis[x] != null)
        //        {
        //            int[] sol = GetNivelSolo(Niveis[x], bn);
        //            int[] fol = GetNivelFoliar(Niveis[x], lst);

        //            int ct = 0;
        //            for (int j = 0; j < 26; j += 2)
        //            {
        //                values[x, j] = sol[ct];
        //                values[x, j + 1] = fol[ct++];
        //            }
        //        }
        //    }
        //    return values;
        //}

        //private int[,] PreencherFoliar(AmostraFoliar amostraF, int[,] m)
        //{
        //    NivelSolo[] arrayN = _nivelSoloService.GetSolo(amostraF.EstagioCultura.Cultura);

        //    List<AuxNivelElemento> lst = GetFoliar(amostraF);
        //    for (int i = 0; i < 13; i++)
        //    {
        //        if (lst[i] != null)
        //        {
        //            int[] res = GetNivelFoliar(arrayN[i], lst);
        //            int ct = 0;
        //            for (int j = 1; j < 26; j += 2)
        //            {
        //                m[i, j] = res[ct++];
        //            }
        //        }
        //    }
        //    return m;
        //}

        //private int GetNivel(Double? value, Double? minimo, Double? maximo)
        //{
        //    if (value >= minimo && value <= maximo)
        //    {
        //        return 0;
        //    }
        //    else if (value < minimo)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return 2;
        //    }
        //}

        //private int[] GetNivelFoliar(NivelSolo nv, List<AuxNivelElemento> lst)
        //{
        //    int[] values = new int[13];

        //    if (lst != null && nv != null)
        //    {
        //        // N
        //        if ((bool)nv.n || nv.elemento.Equals("N "))
        //        {
        //            if (lst[0].Nivel >= 2 && lst[0].Nivel <= 3)
        //            {
        //                values[0] = 0;
        //            }
        //            else if (lst[0].Nivel < 2)
        //            {
        //                values[0] = 1;
        //            }
        //            else
        //            {
        //                values[0] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[0] = 0;
        //        }

        //        //P
        //        if ((bool)nv.p || nv.elemento.Equals("P "))
        //        {
        //            if (lst[1].Nivel >= 2 && lst[1].Nivel <= 3)
        //            {
        //                values[1] = 0;
        //            }
        //            else if (lst[1].Nivel < 2)
        //            {
        //                values[1] = 1;
        //            }
        //            else
        //            {
        //                values[1] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[1] = 0;
        //        }

        //        //K
        //        if ((bool)nv.k || nv.elemento.Equals("K "))
        //        {
        //            if (lst[2].Nivel >= 2 && lst[2].Nivel <= 3)
        //            {
        //                values[2] = 0;
        //            }
        //            else if (lst[2].Nivel < 2)
        //            {
        //                values[2] = 1;
        //            }
        //            else
        //            {
        //                values[2] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[2] = 0;
        //        }

        //        //Ca
        //        if ((bool)nv.ca || nv.elemento.Equals("Ca"))
        //        {
        //            if (lst[3].Nivel >= 2 && lst[3].Nivel <= 3)
        //            {
        //                values[3] = 0;
        //            }
        //            else if (lst[3].Nivel < 2)
        //            {
        //                values[3] = 1;
        //            }
        //            else
        //            {
        //                values[3] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[3] = 0;
        //        }

        //        //Mg
        //        if ((bool)nv.mg || nv.elemento.Equals("Mg"))
        //        {
        //            if (lst[4].Nivel >= 2 && lst[4].Nivel <= 3)
        //            {
        //                values[4] = 0;
        //            }
        //            else if (lst[4].Nivel < 2)
        //            {
        //                values[4] = 1;
        //            }
        //            else
        //            {
        //                values[4] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[4] = 0;
        //        }

        //        //S
        //        if ((bool)nv.s || nv.elemento.Equals("S "))
        //        {
        //            if (lst[5].Nivel >= 2 && lst[5].Nivel <= 3)
        //            {
        //                values[5] = 0;
        //            }
        //            else if (lst[5].Nivel < 2)
        //            {
        //                values[5] = 1;
        //            }
        //            else
        //            {
        //                values[5] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[5] = 0;
        //        }

        //        //B
        //        if ((bool)nv.b || nv.elemento.Equals("B "))
        //        {
        //            if (lst[6].Nivel >= 2 && lst[6].Nivel <= 3)
        //            {
        //                values[6] = 0;
        //            }
        //            else if (lst[6].Nivel < 2)
        //            {
        //                values[6] = 1;
        //            }
        //            else
        //            {
        //                values[6] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[6] = 0;
        //        }

        //        //Zn
        //        if ((bool)nv.zn || nv.elemento.Equals("Zn"))
        //        {
        //            if (lst[7].Nivel >= 2 && lst[7].Nivel <= 3)
        //            {
        //                values[7] = 0;
        //            }
        //            else if (lst[7].Nivel < 2)
        //            {
        //                values[7] = 1;
        //            }
        //            else
        //            {
        //                values[7] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[7] = 0;
        //        }

        //        //Mn
        //        if ((bool)nv.mn || nv.elemento.Equals("Mn"))
        //        {
        //            if (lst[8].Nivel >= 2 && lst[8].Nivel <= 3)
        //            {
        //                values[8] = 0;
        //            }
        //            else if (lst[8].Nivel < 2)
        //            {
        //                values[8] = 1;
        //            }
        //            else
        //            {
        //                values[8] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[8] = 0;
        //        }

        //        //Fe
        //        if ((bool)nv.fe || nv.elemento.Equals("Fe"))
        //        {
        //            if (lst[9].Nivel >= 2 && lst[9].Nivel <= 3)
        //            {
        //                values[9] = 0;
        //            }
        //            else if (lst[9].Nivel < 2)
        //            {
        //                values[9] = 1;
        //            }
        //            else
        //            {
        //                values[9] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[9] = 0;
        //        }

        //        //Cu
        //        if ((bool)nv.cu || nv.elemento.Equals("Cu"))
        //        {
        //            if (lst[10].Nivel >= 2 && lst[10].Nivel <= 3)
        //            {
        //                values[10] = 0;
        //            }
        //            else if (lst[10].Nivel < 2)
        //            {
        //                values[10] = 1;
        //            }
        //            else
        //            {
        //                values[10] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[10] = 0;
        //        }


        //        //Mo
        //        if ((bool)nv.mo || nv.elemento.Equals("Mo"))
        //        {
        //            if (lst[11].Nivel >= 2 && lst[11].Nivel <= 3)
        //            {
        //                values[11] = 0;
        //            }
        //            else if (lst[11].Nivel < 2)
        //            {
        //                values[11] = 1;
        //            }
        //            else
        //            {
        //                values[11] = 2;
        //            }
        //        }
        //        else
        //        {
        //            values[11] = 0;
        //        }

        //        //Co
        //        values[12] = 0;

        //    }
        //    return values;
        //}

        //private List<AuxCodigoBalancoFoliar> GetMatrix(AmostraFoliar amostraF)
        //{
        //    int[,] result = PreencherSolo(amostraF);

        //    List<AuxCodigoBalancoFoliar> lst = new List<AuxCodigoBalancoFoliar>();

        //    for (int x = 0; x < result.GetLength(0); x++)
        //    {
        //        int[] array = new int[26];
        //        for (int y = 0; y < result.GetLength(1); y++)
        //        {
        //            array[y] = result[x, y];
        //        }

        //        AuxCodigoBalancoFoliar aux = new AuxCodigoBalancoFoliar();
        //        aux.SetCodigo(array);
        //        lst.Add(aux);
        //    }
        //    return lst;
        //}

        //#endregion

        //#region CoreBalancoNutricionalSolo

        //private int[] GetNivelSolo(NivelSolo nv, TeorSolo bn)
        //{
        //    int[] values = new int[13];

        //    if (bn != null && nv != null)
        //    {
        //        // N
        //        if ((bool)nv.n || nv.elemento.Equals("N "))
        //        {
        //            values[0] = GetNivel(bn.n, Niveis[0].minimo, Niveis[0].maximo);
        //        }

        //        //P
        //        if ((bool)nv.p || nv.elemento.Equals("P "))
        //        {
        //            values[1] = GetNivel(bn.p, Niveis[1].minimo, Niveis[1].maximo);
        //        }

        //        //K
        //        if ((bool)nv.k || nv.elemento.Equals("K "))
        //        {
        //            values[2] = GetNivel(bn.k, Niveis[2].minimo, Niveis[2].maximo);
        //        }

        //        //Ca
        //        if ((bool)nv.ca || nv.elemento.Equals("Ca"))
        //        {
        //            values[3] = GetNivel(bn.ca, Niveis[3].minimo, Niveis[3].maximo);
        //        }

        //        //Mg
        //        if ((bool)nv.mg || nv.elemento.Equals("Mg"))
        //        {
        //            values[4] = GetNivel(bn.mg, Niveis[4].minimo, Niveis[4].maximo);
        //        }

        //        //S
        //        if ((bool)nv.s || nv.elemento.Equals("S "))
        //        {
        //            values[5] = GetNivel(bn.s, Niveis[5].minimo, Niveis[5].maximo);
        //        }

        //        //B
        //        if ((bool)nv.b || nv.elemento.Equals("B "))
        //        {
        //            values[6] = GetNivel(bn.b, Niveis[6].minimo, Niveis[6].maximo);
        //        }

        //        //Zn
        //        if ((bool)nv.zn || nv.elemento.Equals("Zn"))
        //        {
        //            values[7] = GetNivel(bn.zn, Niveis[7].minimo, Niveis[7].maximo);
        //        }

        //        //Mn
        //        if ((bool)nv.mn || nv.elemento.Equals("Mn"))
        //        {
        //            values[8] = GetNivel(bn.mn, Niveis[8].minimo, Niveis[8].maximo);
        //        }

        //        //Fe
        //        if ((bool)nv.fe || nv.elemento.Equals("Fe"))
        //        {
        //            values[9] = GetNivel(bn.fe, Niveis[9].minimo, Niveis[9].maximo);
        //        }

        //        //Cu
        //        if ((bool)nv.cu || nv.elemento.Equals("Cu"))
        //        {
        //            values[10] = GetNivel(bn.cu, Niveis[10].minimo, Niveis[10].maximo);
        //        }

        //        //Mo
        //        if ((bool)nv.mo || nv.elemento.Equals("Mo"))
        //        {
        //            values[11] = GetNivel(bn.mo, Niveis[11].minimo, Niveis[11].maximo);
        //        }

        //        //Co
        //        if ((bool)nv.co || nv.elemento.Equals("Co"))
        //        {
        //            values[12] = GetNivel(bn.co, Niveis[12].minimo, Niveis[12].maximo);
        //        }
        //    }
        //    return values;
        //}

        //private AuxNivelElemento GetNivelSolo(TeorSolo bn, NivelSolo ns)
        //{
        //    if (bn != null && ns != null)
        //    {
        //        double val = 0;
        //        double nivel = 0;

        //        // N
        //        if (ns.elemento.Equals("N "))
        //        {
        //            val = bn.n;
        //        }

        //        //P
        //        if (ns.elemento.Equals("P "))
        //        {
        //            val = bn.p;
        //        }

        //        //K
        //        if (ns.elemento.Equals("K "))
        //        {
        //            val = bn.k;
        //        }

        //        //Ca
        //        if (ns.elemento.Equals("Ca"))
        //        {
        //            val = bn.ca;
        //        }

        //        //Mg
        //        if (ns.elemento.Equals("Mg"))
        //        {
        //            val = bn.mg;
        //        }

        //        //S
        //        if (ns.elemento.Equals("S "))
        //        {
        //            val = bn.s;
        //        }

        //        //B
        //        if (ns.elemento.Equals("B "))
        //        {
        //            val = bn.b;
        //        }
        //        //Zn
        //        if (ns.elemento.Equals("Zn"))
        //        {
        //            val = bn.zn;
        //        }

        //        //Mn
        //        if (ns.elemento.Equals("Mn"))
        //        {
        //            val = bn.mn;
        //        }

        //        //Fe
        //        if (ns.elemento.Equals("Fe"))
        //        {
        //            val = bn.fe;
        //        }

        //        //Cu
        //        if (ns.elemento.Equals("Cu"))
        //        {
        //            val = bn.cu;
        //        }

        //        //Mo
        //        if (ns.elemento.Equals("Mo"))
        //        {
        //            val = (Double)bn.mo;
        //        }

        //        //Co
        //        if (ns.elemento.Equals("Co"))
        //        {
        //            val = (Double)bn.co;
        //        }

        //        if (val <= 0)
        //            nivel = 0;
        //        else if (val < ns.deficiente)
        //            nivel = (val / ((double)ns.deficiente));
        //        else if (val < ns.minimo)
        //            nivel = 1 + (val - (Double)ns.deficiente) / ((Double)ns.minimo - (Double)ns.deficiente);
        //        else if (val < ns.maximo)
        //            nivel = 2 + (val - (Double)ns.minimo) / ((Double)ns.maximo - (Double)ns.minimo);
        //        else if (val < ns.toxico)
        //            nivel = 3 + (val - (Double)ns.maximo) / ((Double)ns.toxico - (Double)ns.maximo);
        //        else
        //            nivel = 4 + (val - (Double)ns.toxico) / ((Double)ns.toxico);

        //        return new AuxNivelElemento(ns.elemento.Trim(), nivel);
        //    }
        //    return null;
        //}

        //private AuxNivelElemento GetIndiceSolo(NivelSolo nv, TeorSolo bn)
        //{
        //    if (bn != null && nv != null)
        //    {
        //        double val = 0;
        //        double nivel = 0;

        //        // N
        //        if (nv.elemento.Equals("N "))
        //        {
        //            val = bn.n;
        //        }

        //        //P
        //        if (nv.elemento.Equals("P "))
        //        {
        //            val = bn.p;
        //        }

        //        //K
        //        if (nv.elemento.Equals("K "))
        //        {
        //            val = bn.k;
        //        }

        //        //Ca
        //        if (nv.elemento.Equals("Ca"))
        //        {
        //            val = bn.ca;
        //        }

        //        //Mg
        //        if (nv.elemento.Equals("Mg"))
        //        {
        //            val = bn.mg;
        //        }

        //        //S
        //        if (nv.elemento.Equals("S "))
        //        {
        //            val = bn.s;
        //        }

        //        //B
        //        if (nv.elemento.Equals("B "))
        //        {
        //            val = bn.b;
        //        }
        //        //Zn
        //        if (nv.elemento.Equals("Zn"))
        //        {
        //            val = bn.zn;
        //        }

        //        //Mn
        //        if (nv.elemento.Equals("Mn"))
        //        {
        //            val = bn.mn;
        //        }

        //        //Fe
        //        if (nv.elemento.Equals("Fe"))
        //        {
        //            val = bn.fe;
        //        }

        //        //Cu
        //        if (nv.elemento.Equals("Cu"))
        //        {
        //            val = bn.cu;
        //        }

        //        //Mo
        //        if (nv.elemento.Equals("Mo"))
        //        {
        //            val = (Double)bn.mo;
        //        }

        //        //Co
        //        if (nv.elemento.Equals("Co"))
        //        {
        //            val = (Double)bn.co;
        //        }

        //        if ((Double)nv.minimo == 0 || (Double)nv.maximo == 0)
        //            nivel = 0;
        //        else if (val > (Double)nv.maximo)
        //        {
        //            if ((val - (Double)nv.maximo) / (Double)nv.maximo > 1)
        //                nivel = 100;
        //            else
        //                nivel = ((val - (Double)nv.maximo) / (Double)nv.maximo) * 100;
        //        }
        //        else if (val == (Double)nv.maximo)
        //            nivel = 0;
        //        else if (val > (Double)nv.minimo)
        //            nivel = 0;
        //        else if (val < 0)
        //            nivel = -100;
        //        else
        //            nivel = ((val - (Double)nv.minimo) / (Double)nv.minimo) * 100;

        //        return new AuxNivelElemento(nv.elemento.Trim(), nivel);
        //    }
        //    return null;
        //}
        //#endregion
    }
}
