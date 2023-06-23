using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class RecomendacaoFoliar : ISelfValidation
    {
        public RecomendacaoFoliar()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDCultura { get; set; }
        public string codigo { get; set; }
        public string descritivo { get; set; }
        public string elemento { get; set; }
        public bool excecao { get; set; }

        [JsonIgnore]
        public virtual Cultura Cultura { get; set; }
        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new RecomendacaoFoliarValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
