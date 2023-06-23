using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class Laboratorio : ISelfValidation
    {
        public Laboratorio()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public string nome { get; set; }
        public string cnpj { get; set; }
        public string endereco { get; set; }
        public string cep { get; set; }
        public string telefone { get; set; }

        [JsonIgnore]
        public virtual ICollection<UnidadeDeLaboratorio> UnidadeDeLaboratorio { get; set; }

        [JsonIgnore]
        public virtual ICollection<SequenciaImportacao> SequenciaImportacaos { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new LaboratorioValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }


    }
}
