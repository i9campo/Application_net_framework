using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class VariedadeCultura : ISelfValidation
    {
        public VariedadeCultura()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDCultura { get; set; }
        public string nome { get; set; }
        public int ciclo { get; set; }
        public string empresaDetentora { get; set; }
        public int status { get; set; }
        public string exigencia { get; set; }
        public string acamamento { get; set; }
        public virtual Cultura Cultura { get; set; }
        [JsonIgnore]
        public virtual ICollection<CicloProducao> CicloProducao { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProdutividadeVariedade> ProdutividadeVariedade { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; private set; }
        public bool IsValid
        {
            get
            {
                var validador = new VariedadeCulturaValidation();

                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }

    }
}
