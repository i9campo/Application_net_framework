using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Sigma.Domain.Entities
{
    public class Propriedade : ISelfValidation
    {
        public Propriedade()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDProprietario { get; set; }
        public Guid? IDRegiao { get; set; }
        public DbGeography geo { get; set; }
        public string nome { get; set; }
        public string endereco { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string fone { get; set; }
        public string funcao { get; set; }
        public string contato { get; set; }
        public string fax { get; set; }
        public Nullable<double> distancia { get; set; }
        public double areaTotal { get; set; }
        public Nullable<double> areaIrrigada { get; set; }
        public Nullable<double> areaPlantada { get; set; }
        public string infoAdicionais { get; set; }
        [JsonIgnore]
        public byte[] imgMap { get; set; }
        public string ie { get; set; }
        [JsonIgnore]
        public byte[] imgMGF { get; set; }

        [JsonIgnore]
        public virtual Proprietario Proprietario { get; set; }
        [JsonIgnore]
        public virtual Regiao Regiao { get; set; }
        [JsonIgnore]
        public virtual ICollection<Area> Area { get; set; }
        [JsonIgnore]
        public virtual ICollection<ParametroPropriedade> ParametroPropriedade { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new PropriedadeValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
