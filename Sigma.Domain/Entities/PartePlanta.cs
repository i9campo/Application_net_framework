using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.Entities
{
    public class PartePlanta : ISelfValidation
    {
        public PartePlanta()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDCultura { get; set; }
        public string nome { get; set; }

        [JsonIgnore]
        public virtual Cultura Cultura { get; set; }
        [JsonIgnore]
        public virtual ICollection<AmostraFoliar> AmostraFoliar { get; set; }
        [JsonIgnore]
        public virtual ICollection<FaixaTeor> FaixaTeor { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new PartePlantaValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
