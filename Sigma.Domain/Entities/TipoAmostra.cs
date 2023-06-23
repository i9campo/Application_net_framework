using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class TipoAmostra : ISelfValidation
    {
        public TipoAmostra()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public string nome { get; set; }
        public bool ativo { get; set;}


        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool  IsValid
        {
            get
            {
                var validador = new TipoAmostraValidate();
                ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid; 
            }
        }


    }
}
