using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class Regiao : ISelfValidation
    {
        public Regiao()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public string descricao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Propriedade> Propriedade { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProdutividadeVariedade> ProdutividadeVariedade { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new RegiaoValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }


    }
}
