using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class Servico : ISelfValidation
    {
        public Servico()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public String nome { get; set; }
        public String descricao { get; set; }
        public String tipoTaxa { get; set; }

        [JsonIgnore]
        public virtual ICollection<AreaServico> AreaServico { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new ServicoValidate();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
