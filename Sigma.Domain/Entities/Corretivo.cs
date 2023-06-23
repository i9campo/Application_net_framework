using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class Corretivo : ISelfValidation
    {
        public Corretivo()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public Guid? IDGrid { get; set; }
        public Guid? IDFornecedor { get; set; }
        public string descricao { get; set; }
        public double qtde { get; set; }
        public double prnt { get; set; }
        public double perCaO { get; set; }
        public double perMgO { get; set; }
        public double perP2O5 { get; set; }
        public double perK2O { get; set; }
        public double perCa { get; set; }
        public double perMg { get; set; }
        public double perS { get; set; }
        public double s { get; set; }
        public double ca { get; set; }
        public double mg { get; set; }
        public double k { get; set; }
        public double p { get; set; }
        public double b { get; set; }
        public double zn { get; set; }
        public double fe { get; set; }
        public double mn { get; set; }
        public double cu { get; set; }
        public double co { get; set; }
        public double momicro { get; set; }
        public double v { get; set; }
        public int marcado { get; set; }
        public int opcaoMarcado { get; set; }
        public int opcao { get; set; }
        public double custo { get; set; }
        public double eficiencia { get; set; }
        public int perfil { get; set; }
        public bool corretivo { get; set; }

        [JsonIgnore]
        public virtual Grid Grid { get; set; }
        [JsonIgnore]
        public virtual AreaServico AreaServico { get; set; }
        [JsonIgnore]
        public virtual Fornecedor Fornecedor { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new CorretivoValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
    public class UpdateChecked
    {
        public bool chk { get; set; }

        public int? opcao { get; set; }
        public Guid IDGrid { get; set; }
        public Guid IDAreaServico { get; set; }
    }
    public class DeleteCorretivo
    {
        public int opcao { get; set; }
        public Guid IDCiclo { get; set; }
    }
    public class SetDivideDoseCorretivo
    {
        public string IDGrid { get; set; }
        public string IDAreaServico { get; set; }
        public string IDCorretivo { get; set; }
        public string NomeCorretivo { get; set; }
        public string NovoCorretivo { get; set; }
        public string opcao { get; set; }
    }
}
