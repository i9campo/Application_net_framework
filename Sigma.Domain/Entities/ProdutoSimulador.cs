using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class ProdutoSimulador : ISelfValidation
    {
        public ProdutoSimulador()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDSimulacao { get; set; }
        public Guid IDProduto { get; set; }
        public Guid? IDCultura { get; set; }
        public Guid? IDEstagioCultura { get; set; }
        public String IDUsuarioINC { get; set; }
        public String IDUsuarioALT { get; set; }
        public DateTime dateINC { get; set; }
        public DateTime? dateALT { get; set; }
        public String produto { get; set; } // tamanho da string 200
        public float doseMin { get; set; }
        public float? doseMax { get; set; }
        public float? dap { get; set; }
        public int tipo { get; set; }


        [JsonIgnore]
        public virtual Simulacao Simulacao { get; set; }
        [JsonIgnore]
        public virtual Cultura Cultura { get; set; }
        [JsonIgnore]
        public virtual EstagioCultura EstagioCultura { get; set; }
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
        [JsonIgnore]
        public virtual Produto Produto { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new ProdutoSimuldaroValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
