using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class ExtracaoCultura : ISelfValidation
    {
        public ExtracaoCultura()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDCultura { get; set; }
        public string nutriente { get; set; }
        public double nivel1 { get; set; }
        public double nivel2 { get; set; }
        public double nivel3 { get; set; }
        public double nivel4 { get; set; }
        public double nivel5 { get; set; }

        [JsonIgnore]
        public virtual Cultura Cultura { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new ExtracaoCulturaValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
