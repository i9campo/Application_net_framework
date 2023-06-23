using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class UnidadeMedida : ISelfValidation
    {
        public UnidadeMedida()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public string nome { get; set; }
        public double peso { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cultura> Cultura { get; set; }
        [JsonIgnore]
        public virtual ICollection<Produto> Produto { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProdutividadeVariedade> ProdutividadeVariedade { get; set; }
        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool  IsValid { 
            get
            {
                var validador = new UnidadeMedidaValidation();
                ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid; 
            }
        }

    }
}
