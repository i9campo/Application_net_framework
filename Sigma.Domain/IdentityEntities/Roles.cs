using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.Identity;
using System.Collections.Generic;

namespace Sigma.Domain.IdentityEntities
{
    public class Roles : ISelfValidation
    {
        public string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Tipo { get; set; }
        public virtual string ViewerRoler { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRoles> UserRoles { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new RolesValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }

    }
}
