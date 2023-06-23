using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS; 
using System;

namespace Sigma.Domain.Entities
{
    public class Amostra: ISelfValidation
    {
        public Amostra()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDCultura { get; set; }
        public string descricao { get; set; }
        public double n { get; set; }
        public double p2o5 { get; set; }
        public double k2o { get; set; }
        public double s { get; set; }
        public double ca { get; set; }
        public double mg { get; set; }
        public double b { get; set; }
        public double zn { get; set; }
        public double cu { get; set; }
        public double mn { get; set; }
        public double co { get; set; }
        public double mo { get; set; }
        public int media { get; set; }

        [JsonIgnore]
        public virtual Cultura Cultura { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                var validador = new AmostraValidation();

                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
