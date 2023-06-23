using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.Identity;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.IdentityEntities
{
    public class Usuario :ISelfValidation
    {
        public string Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual bool EmailConfirmed { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual bool PhoneNumberConfirmed { get; set; }
        public virtual bool TwoFactorEnabled { get; set; }
        public virtual DateTime? LockoutEndDateUtc { get; set; }
        public virtual bool LockoutEnabled { get; set; }
        public virtual int AccessFailedCount { get; set; }


        [JsonIgnore]
        public virtual ICollection<Claims> Claims { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRoles> UserRoles { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserLogin> UserLogins { get; set; }

        [JsonIgnore]
        public virtual ICollection<UsuarioAtivo> UsuarioAtivo { get; set; }

        [JsonIgnore]
        public virtual ICollection<Simulacao> Simulacao { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProdutoSimulador> ProdutoSimulador { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                // Validação para a tabela AspNetUser. 
                var validador = new UsuarioValidation();

                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
