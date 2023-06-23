using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.Entities
{
    public class Simulacao : ISelfValidation
    {
        public Simulacao()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDUltimaCultura { get; set; }
        public Guid IDProximaCultura { get; set; }
        public Guid IDAreaServico { get; set; }
        public string IDUsuarioINC { get; set; }
        public string IDUsuarioALT { get; set; }
        public DateTime dateINC { get; set; }
        public DateTime? dateALT { get; set; }
        public int opcao { get; set; }
        public string nematoideCisto { get; set; }
        public bool utilizarCalcario { get; set; }
        public bool utilizarGesso { get; set; }
        public bool utilizarFosforo { get; set; }
        public bool considerarResteva { get; set; }
        public string metodoAcidezSuperficie { get; set; }
        public float perVMinimoAcidezSuperf { get; set; }
        public float mgMinimoAcidezSuperf { get; set; }
        public int profundidadeAcidezSuperf { get; set; }
        public bool doseZeroAcidezSuperf { get; set; }
        public bool aplicouCorretivoAcidezSuperf { get; set; }
        public bool corrigirPerfilAcidezSuperf { get; set; }
        public float perVacidezPerfil { get; set; }
        public int profundidadeAcidezPerfil { get; set; }
        public bool utilizarCorretivoAcidezPerfil { get; set; }
        public string metodoFosforo { get; set; }
        public float fosforoDesejado { get; set; }
        public bool doseZeroFosforo { get; set; }
        public float doseMediaFosforo { get; set; }
        public float baseFosforo { get; set; }
        public string metodoEnxofre { get; set; }
        public float enxofreDesejado { get; set; }
        public bool doseZeroEnxofre { get; set; }
        public float doseMediaEnxofre { get; set; }
        public bool corrigirPerfilEnxofre { get; set; }
        public string metodoPotassio { get; set; }
        public float potassioDesejado { get; set; }
        public bool doseZeroPotassio { get; set; }
        public float doseMediaPotassio { get; set; }
        public string paramRecomendFert { get; set; }
        public DateTime dataPlantioFert { get; set; }
        public float produtividadeFert { get; set; }
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
        [JsonIgnore]
        public virtual Cultura Cultura { get; set; }
        [JsonIgnore]
        public ICollection<ProdutoSimulador> ProdutoSimulador { get; set; }
        [JsonIgnore]
        public virtual AreaServico AreaServico { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new SimulacaoValidate();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }


    }
}
