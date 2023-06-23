using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class UnidadeDeLaboratorio : ISelfValidation
    {
        public UnidadeDeLaboratorio()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDLaboratorio { get; set; }
        public Nullable<float> n { get; set; }
        public Nullable<float> p { get; set; }
        public Nullable<float> k { get; set; }
        public Nullable<float> s { get; set; }
        public Nullable<float> ca { get; set; }
        public Nullable<float> mg { get; set; }
        public Nullable<float> b { get; set; }
        public Nullable<float> zn { get; set; }
        public Nullable<float> cu { get; set; }
        public Nullable<float> co { get; set; }
        public Nullable<float> mo { get; set; }
        public Nullable<float> mn { get; set; }
        public Nullable<float> fe { get; set; }

        [JsonIgnore]
        public virtual Laboratorio Laboratorio { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        
        public bool IsValid
        {
            get
            {
                var validador = new UnidadeLaboratorioValidation();
                ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid; 
            }
        }


    }
}
