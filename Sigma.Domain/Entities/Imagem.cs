using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class Imagem : ISelfValidation
    {
        public Imagem()
        {
            objID = new Guid();
        }
        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public string tipo { get; set; }
        public byte[] image { get; set; }
        public int indice { get; set; }
        public string nome { get; set; }
        public string legenda1 { get; set; }
        public string legenda2 { get; set; }
        public string legenda3 { get; set; }
        public string legenda4 { get; set; }
        public string legenda5 { get; set; }
        public string legenda6 { get; set; }
        public string formatoGerado { get; set; }
        public int? distPontos { get; set; }
        public int? perBuffer { get; set; }
        public int? potencia { get; set; }
        public bool? tipoDistPontos { get; set; }

        public virtual AreaServico AreaServico { get; set; }
        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new ImagemValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }

    }
}
