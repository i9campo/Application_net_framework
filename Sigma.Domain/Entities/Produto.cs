using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class Produto : ISelfValidation
    {
        public Produto()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDFornecedor { get; set; }
        public Guid IDUnidadeMedida { get; set; }
        public string nome { get; set; }
        public string tipo { get; set; }
        public string alvo { get; set; }
        public string formato { get; set; }
        public string classe { get; set; }
        public string principioAtivo { get; set; }
        public bool ativo { get; set; }
        public double eficiencia { get; set; }
        public double densidade { get; set; }
        public double preco { get; set; }
        public double prnt { get; set; }
        public double cao { get; set; }
        public double mgo { get; set; }
        public double p2o5 { get; set; }
        public double k2o { get; set; }
        public double s { get; set; }
        public double n { get; set; }
        public double ca { get; set; }
        public double mg { get; set; }
        public double b { get; set; }
        public double zn { get; set; }
        public double cu { get; set; }
        public double mn { get; set; }
        public double mo { get; set; }
        public double co { get; set; }
        public double fe { get; set; }
        public double si { get; set; }
        public double ni { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProdutoSimulador> ProdutoSimulador { get; set; }
        [JsonIgnore]
        public virtual Fornecedor Fornecedor { get; set; }
        [JsonIgnore]
        public virtual UnidadeMedida UnidadeMedida { get; set; }
        [JsonIgnore]
        public virtual ICollection<UsoProduto> UsoProduto { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new ProdutoValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;

            }
        }
    }
}
