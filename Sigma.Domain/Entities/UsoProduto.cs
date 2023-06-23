using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class UsoProduto : ISelfValidation
    {
        public UsoProduto()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDProduto { get; set; }
        public Guid IDCultura { get; set; }
        public string acao { get; set; }
        public string alvo { get; set; }
        public string classe { get; set; }
        public string localAplicacao { get; set; }
        public double doseBaixa { get; set; }
        public double doseMedia { get; set; }
        public double doseAlta { get; set; }

        [JsonIgnore]
        public virtual Produto Produto { get; set; }
        [JsonIgnore]
        public virtual Cultura Cultura { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new UsoProdutoValidation();
                ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid; 
            }
        }

    }
}
