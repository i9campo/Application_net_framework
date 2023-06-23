using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class AmostraFoliar : ISelfValidation
    {
        public AmostraFoliar()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public Guid IDEstagioCultura { get; set; }
        public Guid IDPartePlanta { get; set; }
        public System.DateTime data { get; set; }
        public string nome { get; set; }
        public Nullable<int> ciclo { get; set; }

        [JsonIgnore]
        public virtual AreaServico AreaServico { get; set; }

        [JsonIgnore]
        public virtual EstagioCultura EstagioCultura { get; set; }

        [JsonIgnore]
        public virtual PartePlanta PartePlanta { get; set; }

        [JsonIgnore]
        public virtual ICollection<TeorFoliar> TeorFoliar { get; set; }

        [JsonIgnore]
        public virtual ICollection<TeorSolo> TeorSolo { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                var validador = new AmostraFoliarValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }

    }
}
