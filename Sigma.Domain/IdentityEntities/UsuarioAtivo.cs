using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.Identity;
using System;

namespace Sigma.Domain.IdentityEntities
{
    public class UsuarioAtivo :ISelfValidation
    {
        public UsuarioAtivo()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDEmpresa { get; set; }
        public String IDUsuario { get; set; }
        public Boolean Ativo { get; set; }
        public Boolean Conectado { get; set; }

        [JsonIgnore]
        public virtual Empresa Empresa { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; private set; }
        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                var validador = new UsuarioAtivoValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
