using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class Resteva : ISelfValidation
    {
        public Resteva()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDCultura { get; set; }
        public string tipo { get; set; }
        public string descricao { get; set; }
        public Nullable<double> n { get; set; }
        public Nullable<double> p2o5 { get; set; }
        public Nullable<double> s { get; set; }
        public Nullable<double> ca { get; set; }
        public Nullable<double> mg { get; set; }
        public Nullable<double> b { get; set; }
        public Nullable<double> zn { get; set; }
        public Nullable<double> cu { get; set; }
        public Nullable<double> co { get; set; }
        public Nullable<double> mo { get; set; }
        public Nullable<double> k2o { get; set; }
        public Nullable<double> mn { get; set; }
        public Nullable<double> k { get; set; }
        public Nullable<double> p { get; set; }
        public bool media { get; set; }

        [JsonIgnore]
        public virtual Cultura Cultura { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new RestevaValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
