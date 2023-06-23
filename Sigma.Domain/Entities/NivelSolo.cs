using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class NivelSolo : ISelfValidation
    {
        public NivelSolo()
        {
            this.objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDCultura { get; set; }
        public string elemento { get; set; }
        public Nullable<double> deficiente { get; set; }
        public Nullable<double> minimo { get; set; }
        public Nullable<double> maximo { get; set; }
        public Nullable<double> toxico { get; set; }
        public Nullable<bool> n { get; set; }
        public Nullable<bool> p { get; set; }
        public Nullable<bool> k { get; set; }
        public Nullable<bool> ca { get; set; }
        public Nullable<bool> mg { get; set; }
        public Nullable<bool> s { get; set; }
        public Nullable<bool> b { get; set; }
        public Nullable<bool> zn { get; set; }
        public Nullable<bool> mn { get; set; }
        public Nullable<bool> fe { get; set; }
        public Nullable<bool> cu { get; set; }
        public Nullable<bool> mo { get; set; }
        public Nullable<bool> co { get; set; }

        [JsonIgnore]
        public virtual Cultura Cultura { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new NivelSoloValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
