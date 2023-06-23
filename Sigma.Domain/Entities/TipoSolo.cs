using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class TipoSolo : ISelfValidation
    {
        public TipoSolo()
        {
            objID = new Guid();
        }
        public Guid objID { get; set; }
        public string abreviacao { get; set; }
        public string descricao { get; set; }
        public string cor { get; set; }

        [JsonIgnore]
        public ICollection<AnaliseSolo> AnaliseSolo { get; private set; }


        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new TipoSoloValidate();
                ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid; 
            }
        }
    }
}
