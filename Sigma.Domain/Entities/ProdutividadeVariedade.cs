using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class ProdutividadeVariedade : ISelfValidation
    {
        public ProdutividadeVariedade()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDRegiao { get; set; }
        public Guid IDVariedadeCultura { get; set; }
        public Guid IDUnidadeMedida { get; set; }
        public double qtdeProduzida { get; set; }
        public string ciclo { get; set; }
        public string publicacao { get; set; }
        public string autor { get; set; }
        public int ano { get; set; }
        public string condicao { get; set; }

        public virtual Regiao Regiao { get; set; }
        public virtual VariedadeCultura VariedadeCultura { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new ProdutividadeVariedadeValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
