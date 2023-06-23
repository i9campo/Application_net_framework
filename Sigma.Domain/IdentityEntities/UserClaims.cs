using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.Identity;

namespace Sigma.Domain.IdentityEntities
{
    public class UserClaims :ISelfValidation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                var validador = new UserClaimsValidation();

                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
