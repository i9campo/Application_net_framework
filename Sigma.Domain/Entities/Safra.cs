using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class Safra : ISelfValidation
    {
        public Safra()
        {
            objID = Guid.NewGuid();
        }


        public Guid objID { get; set; }
        public string descricao { get; set; }
        public Nullable<int> anoInicial { get; set; }
        public Nullable<int> anoFinal { get; set; }
        [JsonIgnore]
        public virtual ICollection<ParametroRecomendacao> ParametroRecomendacao { get; set; }

        [JsonIgnore]
        public virtual ICollection<AreaServico> AreaServico { get; set; }

        [JsonIgnore]
        public virtual ICollection<SequenciaImportacao> SequenciaImportacaos { get; set; }

        [JsonIgnore]
        public virtual ICollection<ParametroPropriedade> ParametroPropriedade { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new SafraValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
