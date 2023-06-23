using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class FaixaTeor  : ISelfValidation
    {
        public FaixaTeor()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDEstagioCultura { get; set; }
        public Guid IDPartePlanta { get; set; }
        public string nutriente { get; set; }
        public Nullable<double> nivel1 { get; set; }
        public Nullable<double> nivel2 { get; set; }
        public Nullable<double> nivel3 { get; set; }
        public Nullable<double> nivel4 { get; set; }

        [JsonIgnore]
        public virtual EstagioCultura EstagioCultura { get; set; }
        [JsonIgnore]
        public virtual PartePlanta PartePlanta { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new FaixaTeorValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
