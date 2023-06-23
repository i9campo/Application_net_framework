using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class Cultura : ISelfValidation
    {
        public Cultura()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDUnidadeMedida { get; set; }
        public string nome { get; set; }
        public double nitrogenio { get; set; }
        public int ciclo { get; set; }
        public int decomposicao { get; set; }
        public double nSimb { get; set; }
        public int nSimbMedida { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }

        [JsonIgnore]
        public virtual ICollection<Amostra> Amostra { get; set; }
        [JsonIgnore]
        public virtual ICollection<AreaServico> AreaServico { get; set; }
        [JsonIgnore]
        public virtual ICollection<EstagioCultura> EstagioCultura { get; set; }
        [JsonIgnore]
        public virtual ICollection<ExtracaoCultura> ExtracaoCultura { get; set; }
        [JsonIgnore]
        public virtual ICollection<CicloProducao> CicloProducao { get; set; }
        [JsonIgnore]
        public virtual ICollection<CicloProducao> CicloProducaoAnterior { get; set; }
        [JsonIgnore]
        public virtual ICollection<Resteva> Resteva { get; set; }
        [JsonIgnore]
        public virtual ICollection<NivelSolo> NivelSolo { get; set; }
        [JsonIgnore]
        public virtual ICollection<PartePlanta> PartePlanta { get; set; }
        [JsonIgnore]
        public virtual ICollection<RecomendacaoFoliar> RecomendacaoFoliar { get; set; }
        [JsonIgnore]
        public virtual ICollection<UsoProduto> UsoProduto { get; set; }
        [JsonIgnore]
        public virtual ICollection<VariedadeCultura> VariedadeCultura { get; set; }
        [JsonIgnore]
        public ICollection<ParametroArea> ParametroArea { get; set; }
        [JsonIgnore]
        public ICollection<ProdutoSimulador> ProdutoSimulador { get; set; }
        [JsonIgnore]
        public ICollection<Simulacao> Simulacao { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new CulturaValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
