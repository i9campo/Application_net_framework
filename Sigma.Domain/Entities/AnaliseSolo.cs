using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using Sigma.Domain.ViewTables;
using System;
using System.Data.Entity.Spatial;
using System.Security.Policy;

namespace Sigma.Domain.Entities
{
    public class AnaliseSolo : ISelfValidation
    {
        public AnaliseSolo()
        {
            objID = Guid.NewGuid();
            agua = 0;
            cacl2 = 0;
            mo = 0;
            momicro = 0;
            co = 0;
            pmehl = 0;
            pres = 0;
            k = 0;
            s = 0;
            ca = 0;
            mg = 0;
            al = 0;
            hal = 0;
            ctc = 0;
            argila = 0;
            b = 0;
            zn = 0;
            fe = 0;
            mn = 0;
            cu = 0;
            somaBase = 0;
            v = 0;
            relcamg = 0;
            relcak = 0;
            relmgk = 0;
            relcamgk = 0;
            ctcca = 0;
            ctck = 0;
            ctcal = 0;
        }

        public AnaliseSolo(AnaliseSoloView item)
        {
            objID = Guid.NewGuid();
            IDAreaServico = Guid.Parse(item.IDAreaServico.ToString());
            IDGrid = Guid.Parse(item.IDGrid.ToString());
            descricao = item.descricao;
            compactacao = item.compactacao;
            profundidade = item.profundidade;
            data = item.data;
            ponto = int.Parse(item.ponto.ToString());
            subAmostra = bool.Parse(item.subAmostra == null ? "false" : item.subAmostra.ToString());
            sequenciaSubA = item.sequenciaSubA;
            agua = double.Parse(item.Agua.ToString());
            cacl2 = double.Parse(item.Cacl.ToString());

            if (!String.IsNullOrEmpty(item.IDTipoSolo))
                IDTipoSolo =  Guid.Parse(item.IDTipoSolo); 

            mo = double.Parse(item.MO.ToString());
            momicro = double.Parse(item.momicro.ToString());
            co = double.Parse(item.Co.ToString());
            pmehl = double.Parse(item.PMehl.ToString());
            pres = double.Parse(item.PRes.ToString());
            k = double.Parse(item.K.ToString());
            s = double.Parse(item.S.ToString());
            ca = double.Parse(item.Ca.ToString());
            s = double.Parse(item.S.ToString());
            mg = double.Parse(item.Mg.ToString());
            al = double.Parse(item.Al.ToString());
            hal = double.Parse(item.HAl.ToString());
            ctc = double.Parse(item.CTC.ToString());
            argila = double.Parse(item.Argila.ToString());
            b = double.Parse(item.B.ToString());
            zn = double.Parse(item.Zn.ToString());
            fe = double.Parse(item.Fe.ToString());
            mn = double.Parse(item.Mn.ToString());
            cu = double.Parse(item.Cu.ToString());
            somaBase = double.Parse(item.SomaBases.ToString());
            v = double.Parse(item.V.ToString());
            relcamg = double.Parse(item.relCaMg.ToString());
            relcak = double.Parse(item.relCaK.ToString());
            relmgk = double.Parse(item.relMgK.ToString());
            relcamgk = double.Parse(item.relCaMgK.ToString());
            ctcca = double.Parse(item.CTCCa.ToString());
            ctcmg = double.Parse(item.CTCMg.ToString());
            ctck = double.Parse(item.CTCK.ToString());
            ctcal = double.Parse(item.CTCAl.ToString());
        }

        public AnaliseSolo(AnaliseSoloView item, AnaliseSolo db)
        {
            double result = 0;

            descricao           = item.descricao;
            compactacao         = item.compactacao;
            profundidade        = item.profundidade;
            data                = item.data;
            ponto               = int.Parse(item.ponto.ToString());
            subAmostra          = bool.Parse(item.subAmostra == null ? "false" : item.subAmostra.ToString());
            sequenciaSubA       = item.sequenciaSubA;
            agua                = double.Parse(item.Agua.ToString());
            cacl2               = double.Parse(item.Cacl.ToString());

            mo                  = double.TryParse(item.MO.ToString()        , out result) ? double.Parse(item.MO.ToString()) : 0;
            momicro             = double.TryParse(item.momicro.ToString()   , out result) ? double.Parse(item.momicro.ToString()) : 0;
            co                  = double.TryParse(item.Co.ToString()        , out result) ? double.Parse(item.Co.ToString()):0;
            pmehl               = double.TryParse(item.PMehl.ToString(), out result)      ? double.Parse(item.PMehl.ToString()):0;
            pres                = double.TryParse(item.PRes.ToString() , out result)      ? double.Parse(item.PRes.ToString()):0;
            k                   = double.TryParse(item.K.ToString() , out result) ? double.Parse(item.K.ToString()) : 0;
            s                   = double.TryParse(item.S.ToString() , out result) ? double.Parse(item.S.ToString()) : 0;
            ca                  = double.TryParse(item.Ca.ToString(), out result) ? double.Parse(item.Ca.ToString()): 0;
            s                   = double.TryParse(item.S.ToString() , out result) ? double.Parse(item.S.ToString()) : 0;
            mg                  = double.TryParse(item.Mg.ToString(), out result) ? double.Parse(item.Mg.ToString()): 0;
            al                  = double.TryParse(item.Al.ToString(), out result) ? double.Parse(item.Al.ToString()): 0;
            hal                 = double.TryParse(item.HAl.ToString(), out result) ? double.Parse(item.HAl.ToString()):0;
            ctc                 = double.TryParse(item.CTC.ToString(), out result) ? double.Parse(item.CTC.ToString()):0;
            argila              = double.TryParse(item.Argila.ToString(), out result) ? double.Parse(item.Argila.ToString()):0;
            b                   = double.TryParse(item.B.ToString(), out result) ? double.Parse(item.B.ToString()):0;
            zn                  = double.TryParse(item.Zn.ToString(), out result) ? double.Parse(item.Zn.ToString()):0;
            fe                  = double.TryParse(item.Fe.ToString() , out result) ? double.Parse(item.Fe.ToString()):0;
            mn                  = double.TryParse(item.Mn.ToString(), out result) ? double.Parse(item.Mn.ToString()):0;
            cu                  = double.TryParse(item.Cu.ToString(), out result) ? double.Parse(item.Cu.ToString()):0;
            somaBase            = double.TryParse(item.SomaBases.ToString(), out result) ? double.Parse(item.SomaBases.ToString()):0;
            v                   = double.TryParse(item.V.ToString(), out result) ? double.Parse(item.V.ToString()) : 0;
            relcamg             = double.TryParse(item.relCaMg.ToString(), out result) ? double.Parse(item.relCaMg.ToString()) : 0;
            relcak              = double.TryParse(item.relCaK.ToString(), out result) ? double.Parse(item.relCaK.ToString()) : 0;
            relmgk              = double.TryParse(item.relMgK.ToString() , out result) ? double.Parse(item.relMgK.ToString()) : 0;
            relcamgk            = double.TryParse(item.relCaMgK.ToString(), out result) ? double.Parse(item.relCaMgK.ToString()) : 0;
            ctcca               = double.TryParse(item.CTCCa.ToString(), out result) ? double.Parse(item.CTCCa.ToString()) : 0;
            ctcmg               = double.TryParse(item.CTCMg.ToString(), out result) ? double.Parse(item.CTCMg.ToString()) : 0;
            ctck                = double.TryParse(item.CTCK.ToString() , out result) ? double.Parse(item.CTCK.ToString()) : 0;
            ctcal               = double.TryParse(item.CTCAl.ToString(), out result) ? double.Parse(item.CTCAl.ToString()) : 0;
            geo                 = db.geo;
            jsonField           = db.jsonField;
        }

        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public Guid? IDTipoSolo { get; set; }
        public Guid? IDGrid { get; set; }
        public string descricao { get; set; }
        public DateTime? data { get; set; }
        public string compactacao { get; set; }
        public string profundidade { get; set; }
        public int ponto { get; set; }
        public bool subAmostra { get; set; } // TRUE = SUBAMOSTRA, FALSE = AnáliseSolo   DEFAULT = FALSE
        public String sequenciaSubA { get; set; }   // 0.1 - 0.9999
        public Nullable<double> agua { get; set; }
        public Nullable<double> cacl2 { get; set; }
        public Nullable<double> mo { get; set; }
        public Nullable<double> momicro { get; set; }
        public Nullable<double> co { get; set; }
        public Nullable<double> pmehl { get; set; }
        public Nullable<double> pres { get; set; }
        public Nullable<double> k { get; set; }
        public Nullable<double> s { get; set; }
        public Nullable<double> ca { get; set; }
        public Nullable<double> mg { get; set; }
        public Nullable<double> al { get; set; }
        public Nullable<double> hal { get; set; }
        public Nullable<double> ctc { get; set; }
        public Nullable<double> argila { get; set; }
        public Nullable<double> b { get; set; }
        public Nullable<double> zn { get; set; }
        public Nullable<double> fe { get; set; }
        public Nullable<double> mn { get; set; }
        public Nullable<double> cu { get; set; }
        public Nullable<double> somaBase { get; set; }
        public Nullable<double> v { get; set; }
        public Nullable<double> relcamg { get; set; }
        public Nullable<double> relcak { get; set; }
        public Nullable<double> relmgk { get; set; }
        public Nullable<double> relcamgk { get; set; }
        public Nullable<double> ctcca { get; set; }
        public Nullable<double> ctcmg { get; set; }
        public Nullable<double> ctck { get; set; }
        public Nullable<double> ctcal { get; set; }
        public string jsonField { get; set; }
        [JsonIgnore]
        public DbGeography geo { get; set; }

        [JsonIgnore]
        public virtual AreaServico AreaServico { get; set; }
        [JsonIgnore]
        public virtual TipoSolo TipoSolo { get; set; }
        [JsonIgnore]
        public virtual Grid Grid { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                var validador = new AnaliseSoloValidation();

                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }

    public class AnaliseSoloViewer
    {
        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public Guid? IDTipoSolo { get; set; }
        public Guid? IDGrid { get; set; }
        public string Area { get; set; }
        public string Safra { get; set; }
        public string TipoSolo { get; set; }
        public string Grid { get; set; }
        public string descricao { get; set; }
        public DateTime? data { get; set; }
        public string compactacao { get; set; }
        public string profundidade { get; set; }
        public int ponto { get; set; }
        public bool subAmostra { get; set; } // TRUE = SUBAMOSTRA, FALSE = AnáliseSolo   DEFAULT = FALSE
        public String sequenciaSubA { get; set; }   // 0.1 - 0.9999
        public Nullable<double> agua { get; set; }
        public Nullable<double> cacl2 { get; set; }
        public Nullable<double> mo { get; set; }
        public Nullable<double> momicro { get; set; }
        public Nullable<double> co { get; set; }
        public Nullable<double> pmehl { get; set; }
        public Nullable<double> pres { get; set; }
        public Nullable<double> k { get; set; }
        public Nullable<double> s { get; set; }
        public Nullable<double> ca { get; set; }
        public Nullable<double> mg { get; set; }
        public Nullable<double> al { get; set; }
        public Nullable<double> hal { get; set; }
        public Nullable<double> ctc { get; set; }
        public Nullable<double> argila { get; set; }
        public Nullable<double> b { get; set; }
        public Nullable<double> zn { get; set; }
        public Nullable<double> fe { get; set; }
        public Nullable<double> mn { get; set; }
        public Nullable<double> cu { get; set; }
        public Nullable<double> somaBase { get; set; }
        public Nullable<double> v { get; set; }
        public Nullable<double> relcamg { get; set; }
        public Nullable<double> relcak { get; set; }
        public Nullable<double> relmgk { get; set; }
        public Nullable<double> relcamgk { get; set; }
        public Nullable<double> ctcca { get; set; }
        public Nullable<double> ctcmg { get; set; }
        public Nullable<double> ctck { get; set; }
        public Nullable<double> ctcal { get; set; }
        public string jsonField { get; set; }

        public string geoJson { get; set; }

    }

}
