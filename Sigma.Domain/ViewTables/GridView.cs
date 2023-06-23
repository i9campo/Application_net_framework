using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Sigma.Domain.ViewTables
{
    public class GridView
    {
        public Guid? ID { get;set; }
        public Guid? objID { get; set; }
        public Guid? IDAreaServico { get; set; }
        public string descricao { get; set; }
        public string nome { get; set; }
        public string geoJson { get; set; }
        public string centerLegend { get; set; }
        public double? tamanho { get; set; }
        public double? AREA { get; set; }

        public string Color { get; set; }
        public string ZONA_AREA { get; set; }
        public string ZONA { get; set; }
        public string ZM_CODIGO { get; set; }
        public string jsonField { get; set; }

        public int? CODIGO_ZM { get; set; }
        public int? CODIGO { get; set; }

        public List<string> Rotulo { get; set; }

        public bool? exist { get; set; }
        public bool? loadcomp { get; set; }

        public int? intersection { get; set; }

        public IEnumerable<PositionPoint> coordenadas { get; set; }

        public String StringGeoJson { get; set; }

        [JsonIgnore]
        public DbGeography geo { get; set; }

        #region Valores para média das análises
        public decimal? mAgua { get; set; }
        public decimal? mCacl { get; set; }
        public decimal? mMO { get; set; }
        public decimal? mP { get; set; }
        public decimal? mPMehl { get; set; }
        public decimal? mPRes { get; set; }
        public decimal? mK { get; set; }
        public decimal? mS { get; set; }
        public decimal? mSomaBases { get; set; }
        public decimal? mCa { get; set; }
        public decimal? mMg { get; set; }
        public decimal? mAl { get; set; }
        public decimal? mHAl { get; set; }
        public decimal? mCTC { get; set; }
        public decimal? mV { get; set; }
        public decimal? mrelCaMg { get; set; }
        public decimal? mrelCaK { get; set; }
        public decimal? mrelMgK { get; set; }
        public decimal? mCTCCa { get; set; }
        public decimal? mCTCMg { get; set; }
        public decimal? mCTCK { get; set; }
        public decimal? mCTCAl { get; set; }
        public decimal? mArgila { get; set; }
        public decimal? mB { get; set; }
        public decimal? mZn { get; set; }
        public decimal? mFe { get; set; }
        public decimal? mMn { get; set; }
        public decimal? mCu { get; set; }
        public decimal? mCo { get; set; }
        public decimal? mMomicro { get; set; }
        #endregion

        #region Valores para níveis da média das análises

        #region INICIO. 
        public double? cnPMehl { get; set; }
        public double? cnSomaBases { get; set; }
        public double? cnAgua { get; set; }
        public double? cnMO { get; set; }
        public double? cnCTC { get; set; }
        public double? cnV { get; set; }
        public double? cnArgila { get; set; }
        #endregion

        #region Macronutrientes. 
        public double? cnCa { get; set; }
        public double? cnMg { get; set; }
        public double? cnK { get; set; }
        public double? cnP { get; set; }
        public double? cnPRes { get; set; }
        public double? cnS { get; set; }
        #endregion

        #region Micronutrientes. 
        public double? cnB { get; set; }
        public double? cnZn { get; set; }
        public double? cnFe { get; set; }
        public double? cnMn { get; set; }
        public double? cnCu { get; set; }
        public double? cnMomicro { get; set; }
        #endregion


        #region Participação  na CTC
        public double? cnCTCCa { get; set; }
        public double? cnCTCMg { get; set; }
        public double? cnCTCK { get; set; }
        public double? cnCTCAl { get; set; }
        #endregion

        #region Relações 
        public double? cnRelCaMg { get; set; }
        public double? cnRelCaK { get; set; }
        public double? cnRelMgK { get; set; }
        #endregion
        #endregion
    }
    public class PositionPoint
    {
        public IEnumerable<float> coord { get; set; }
    }
    public class GridViewRecomendacao
    {
        public Guid? ID { get; set; }
        public Guid? objID { get; set; }
        public Guid? IDAreaServico { get; set; }
        public string descricao { get; set; }
        public string nome { get; set; }

        public double? tamanho { get; set; }
        public double? AREA { get; set; }

        public string Color { get; set; }
        public string ZONA_AREA { get; set; }
        public string ZONA { get; set; }
        public string ZM_CODIGO { get; set; }

        public int? CODIGO_ZM { get; set; }
        public int? CODIGO { get; set; }

        public List<string> Rotulo { get; set; }

        public bool? exist { get; set; }
        public bool? loadcomp { get; set; }

        public int? intersection { get; set; }

        public IEnumerable<PositionPoint> coordenadas { get; set; }

        public String StringGeoJson { get; set; }

        #region Valores para média das análises
        public decimal? mAgua { get; set; }
        public decimal? mCacl { get; set; }
        public decimal? mMO { get; set; }
        public decimal? mP { get; set; }
        public decimal? mPMehl { get; set; }
        public decimal? mPRes { get; set; }
        public decimal? mK { get; set; }
        public decimal? mS { get; set; }
        public decimal? mSomaBases { get; set; }
        public decimal? mCa { get; set; }
        public decimal? mMg { get; set; }
        public decimal? mAl { get; set; }
        public decimal? mHAl { get; set; }
        public decimal? mCTC { get; set; }
        public decimal? mV { get; set; }
        public decimal? mrelCaMg { get; set; }
        public decimal? mrelCaK { get; set; }
        public decimal? mrelMgK { get; set; }
        public decimal? mCTCCa { get; set; }
        public decimal? mCTCMg { get; set; }
        public decimal? mCTCK { get; set; }
        public decimal? mCTCAl { get; set; }
        public decimal? mArgila { get; set; }
        public decimal? mB { get; set; }
        public decimal? mZn { get; set; }
        public decimal? mFe { get; set; }
        public decimal? mMn { get; set; }
        public decimal? mCu { get; set; }
        public decimal? mCo { get; set; }
        public decimal? mMomicro { get; set; }
        #endregion

        #region Valores para níveis da média das análises

        #region INICIO. 
        public double? cnPMehl { get; set; }
        public double? cnSomaBases { get; set; }
        public double? cnAgua { get; set; }
        public double? cnMO { get; set; }
        public double? cnCTC { get; set; }
        public double? cnV { get; set; }
        public double? cnArgila { get; set; }
        #endregion

        #region Macronutrientes. 
        public double? cnCa { get; set; }
        public double? cnMg { get; set; }
        public double? cnK { get; set; }
        public double? cnP { get; set; }
        public double? cnPRes { get; set; }
        public double? cnS { get; set; }
        #endregion

        #region Micronutrientes. 
        public double? cnB { get; set; }
        public double? cnZn { get; set; }
        public double? cnFe { get; set; }
        public double? cnMn { get; set; }
        public double? cnCu { get; set; }
        public double? cnMomicro { get; set; }
        #endregion


        #region Participação  na CTC
        public double? cnCTCCa { get; set; }
        public double? cnCTCMg { get; set; }
        public double? cnCTCK { get; set; }
        public double? cnCTCAl { get; set; }
        #endregion

        #region Relações 
        public double? cnRelCaMg { get; set; }
        public double? cnRelCaK { get; set; }
        public double? cnRelMgK { get; set; }
        #endregion
        #endregion
    }
    public class teste
    {
        public String text { get; set; }
    }
    public class UpdateFieldList
    {
        public Guid objID { get; set; }
        public string strField { get; set; }

    }

    public class PostGrid
    {
        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public string descricao { get; set; }
        public string coord { get; set; }
        public string tamanho { get; set; }
    }
}
