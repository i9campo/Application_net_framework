using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class ParametroPropriedade : ISelfValidation
    {
        public ParametroPropriedade()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDSafra { get; set; }
        public Guid IDPropriedade { get; set; }
        public string marcaEquipamento { get; set; }
        public string fosforo { get; set; }
        public string enxofre { get; set; }
        public string nitrogenio { get; set; }
        public string empresasPreferenciais { get; set; }
        public string tipoFosforo { get; set; }
        public string observacao { get; set; }
    

        [JsonIgnore]
        public virtual Propriedade Propriedade { get; set; }

        [JsonIgnore]
        public virtual Safra Safra { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new ParametroPropriedadeValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
