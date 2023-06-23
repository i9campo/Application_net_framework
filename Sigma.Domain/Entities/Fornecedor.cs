using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class Fornecedor : ISelfValidation
    {
        public Fornecedor()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public string nome { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string local { get; set; }

        [JsonIgnore]
        public virtual ICollection<Fertilizante> Fertilizante { get; set; }
        [JsonIgnore]
        public virtual ICollection<Produto> Produto { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProprietarioFornecedor> ProprietarioFornecedor { get; set; }
        [JsonIgnore]
        public virtual ICollection<Corretivo> Corretivo { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new FornecedorValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }

    }
}
