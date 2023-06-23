using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class TeorSolo : ISelfValidation
    {
        public TeorSolo()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDAmostraFoliar { get; set; }
        public double n { get; set; }
        public double p { get; set; }
        public double k { get; set; }
        public double ca { get; set; }
        public double mg { get; set; }
        public double s { get; set; }
        public double b { get; set; }
        public double zn { get; set; }
        public double mn { get; set; }
        public double fe { get; set; }
        public double cu { get; set; }
        public Nullable<double> mo { get; set; }
        public Nullable<double> co { get; set; }

        [JsonIgnore]
        public virtual AmostraFoliar AmostraFoliar { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }


        public bool IsValid
        {
            get
            {
                var validador = new TeorSoloValidation();
                ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid; 
            }
        }

    }
}
