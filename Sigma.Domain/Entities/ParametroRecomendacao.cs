using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class ParametroRecomendacao : ISelfValidation
    {
        public ParametroRecomendacao()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDSafra { get; set; }
        public Guid IDArea { get; set; }
        public string tipo { get; set; }
        public int? opcao { get; set; }
        public string observacao { get; set; }
        public string obsInterno { get; set; }

        [JsonIgnore]
        public virtual Safra Safra { get; set; }
        [JsonIgnore]
        public virtual Area Area { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new ParametroRecomendacaoValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
