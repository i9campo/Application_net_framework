using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
namespace Sigma.Domain.Entities
{
    public class SaveTemporaryImgByte : ISelfValidation
    {
        public Guid objID { get; set; }
        public Guid ConjuntoByteID { get; set; }
        public byte[] bts { get; set; }

        public int idx { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new SaveTemporaryImgByteValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }

    }
}
