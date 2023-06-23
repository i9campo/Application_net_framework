using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class Problema : ISelfValidation
    {
        public Problema()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDArea { get; set; }
        public string tipo { get; set; }
        public string descricao { get; set; }
        public string nivel { get; set; }
        public int ano { get; set; }

        public virtual Area Area { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new ProblemaValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
