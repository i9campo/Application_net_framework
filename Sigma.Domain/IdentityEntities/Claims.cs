using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.Identity;
using System;

namespace Sigma.Domain.IdentityEntities
{
    public class Claims :ISelfValidation
    {
        public Claims()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public String ClaimType { get; set; }
        public String ClaimValue { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                var validador = new ClaimsValidation();

                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
