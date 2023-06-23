using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class ProprietarioFornecedor : ISelfValidation
    {
        public ProprietarioFornecedor()
        {
            objID = new Guid();
        }
        public Guid objID { get; set; }
        public Guid IDProprietario { get; set; }
        public Guid IDFornecedor { get; set; }

        [JsonIgnore]
        public virtual Proprietario Proprietario { get; set; }
        [JsonIgnore]
        public virtual Fornecedor Fornecedor { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new ProprietarioFornecedorValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
