using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class Empresa : ISelfValidation
    {
        public Empresa()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public string nome { get; set; }
        public string cnpj { get; set; }
        public string fone { get; set; }
        public string email { get; set; }
        public string site { get; set; }
        public DateTime dataCadastro { get; set; }
        public bool ativo { get; set; }

        [JsonIgnore]
        public virtual ICollection<UsuarioAtivo> UsuarioAtivo { get; set; }

        [JsonIgnore]
        public virtual ICollection<Proprietario> Proprietario { get; set; }


        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new EmpresaValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
