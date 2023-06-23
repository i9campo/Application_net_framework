using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class TeorFoliar : ISelfValidation
    {
        public TeorFoliar()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDAmostraFoliar { get; set; }
        public int numero { get; set; }
        public Nullable<double> n { get; set; }
        public Nullable<double> p { get; set; }
        public Nullable<double> k { get; set; }
        public Nullable<double> s { get; set; }
        public Nullable<double> ca { get; set; }
        public Nullable<double> mg { get; set; }
        public Nullable<double> b { get; set; }
        public Nullable<double> zn { get; set; }
        public Nullable<double> fe { get; set; }
        public Nullable<double> cu { get; set; }
        public Nullable<double> mn { get; set; }
        public Nullable<double> mo { get; set; }

        [JsonIgnore]
        public virtual AmostraFoliar AmostraFoliar { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }


        public bool IsValid
        {
            get
            {
                var validador = new TeorFoliarValidation();
                ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid; 
            }
        }
    }
}
