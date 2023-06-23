using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class EstagioCultura : ISelfValidation
    {
        public EstagioCultura()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDCultura { get; set; }
        public string codigo { get; set; }
        public string descricao { get; set; }
        public string detalhamento { get; set; }
        public Nullable<double> dapMedio { get; set; }
        public Nullable<double> dapPrecoce { get; set; }
        public Nullable<double> dapTardio { get; set; }

        [JsonIgnore]
        public virtual Cultura Cultura { get; set; }
        [JsonIgnore]
        public virtual ICollection<AmostraFoliar> AmostraFoliar { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProdutoSimulador> ProdutoSimulador { get; set; }
        [JsonIgnore]
        public virtual ICollection<FaixaTeor> FaixaTeor { get; set; }
        [JsonIgnore]
        public virtual ICollection<Fertilizante> Fertilizante { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new EstagioCulturaValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }

    }
}
