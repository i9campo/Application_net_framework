using System;
using System.ComponentModel;
using System.Data.Entity.Spatial;

namespace Sigma.Domain.ViewTables
{
    public class AnaliseSoloView
    {
        public AnaliseSoloView()
        {
            objID = Guid.NewGuid().ToString();
        }
        public string objID { get; set; }
        public string IDAreaServico { get; set; }
        public string IDTipoSolo { get; set; }
        public string IDGrid { get; set; }
        public string IDArea { get; set; }
        public string nome { get; set; }
        public string abreviacao { get; set; }
        public string tipo { get; set; }
        public string tiposolo { get; set; }
        public string solo { get; set; }
        public string descricao { get; set; }
        public string compactacao { get; set; }
        public string profundidade { get; set; }
        public string identificacao { get; set; }
        public string geoJson { get; set; }
        public string geoString { get; set; }
        public string Zona { get; set; }
        public string sequenciaSubA { get; set; }   // A-Z

        public Double? Agua { get; set;  }
        public Double? Cacl { get; set;  }
        public Double? MO { get; set; }
        [DefaultValue(0)]
        public Double? P { get; set; }
        public Double? PMehl { get; set; }
        public Double? PRes { get; set; }
        public Double? K { get; set; }
        public Double? S { get; set; }
        public Double? SomaBases { get; set; }
        public Double? Ca { get; set; }
        public Double? Mg { get; set; }
        public Double? Al { get; set; }
        public Double? HAl { get; set; }
        public Double? CTC { get; set; }
        public Double? V { get; set; }
        public Double? relCaMg { get; set; }
        public Double? relCaK { get; set; }
        public Double? relMgK { get; set; }
        public Double? relCaMgK { get; set; }
        public Double? CTCCa { get; set; }
        public Double? CTCMg { get; set; }
        public Double? CTCK { get; set; }
        public Double? CTCAl { get; set; }
        public Double? Argila { get; set; }
        public Double? B { get; set; }
        public Double? Zn { get; set;  }
        public Double? Fe { get; set; }
        public Double? Mn { get; set; }
        public Double? Cu { get; set;  }
        public Double? Co { get; set; }
        public Double? momicro { get; set; }
        public Double? tamanho { get; set; }
        public DateTime? data { get; set;}
        public Double? N { get; set; }
        public Double? p2o5 { get; set; }
        public Double? K2O { get; set; }
        public string ponto { get; set; }
        public bool? subAmostra { get; set; } // TRUE = SUBAMOSTRA, FALSE = AnáliseSolo   DEFAULT = FALSE
        public string jsonField { get; set; }
        //public DbGeography geo { get; set; }
    }


    public class MediaAnalise
    {
        public Guid objID { get; set; }
        public Guid? IDGrid { get; set; }
        public Double? Agua { get; set; }
        public Double? Cacl2 { get; set; }
        public Double? MO { get; set; }
        public Double? P { get; set; }
        public Double? PMeHl { get; set; }
        public Double? PRes { get; set; }
        public Double? K { get; set; }
        public Double? S { get; set; }
        public Double? SomaBases { get; set; }
        public Double? Ca { get; set; }
        public Double? Mg { get; set; }
        public Double? Al { get; set; }
        public Double? HAl { get; set; }
        public Double? CTC { get; set; }
        public Double? V { get; set; }
        public Double? relCaMg { get; set; }
        public Double? relCaK { get; set; }
        public Double? relMgK { get; set; }
        public Double? relCaMgK { get; set; }
        public Double? CTCCa { get; set; }
        public Double? CTCMg { get; set; }
        public Double? CTCK { get; set; }
        public Double? CTCAl { get; set; }
        public Double? Argila { get; set; }
        public Double? B { get; set; }
        public Double? Zn { get; set; }
        public Double? Fe { get; set;  }
        public Double? Mn { get; set; }
        public Double? Cu { get; set; }
        public Double? Co { get; set; }
        public Double? momicro { get; set; }
        public Double? tamanho { get; set; }
    }


    public class EditedAnalises
    {
        public string SEQUENCIA { get; set; }
        public int? PONTO { get; set; }
        public string tipo { get; set; }
    }


    public class UpdateGridForAnalise
    {
        public Guid? objID { get; set; }
        public Guid? IDGrid { get; set; }
        public Guid? IDAreaServico { get; set; }
        public int tipo { get; set; }
    }


}
