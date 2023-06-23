using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.Identity;

namespace Sigma.Domain.IdentityEntities
{
    public class UserLogin : ISelfValidation
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
        
        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; private set; }
        public bool IsValid
        {
            get
            {
                var validador = new UserLoginValidation();

                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
