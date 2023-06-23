using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Sigma.Domain.Entities
{
    public class Area : ISelfValidation
    {
        public Area()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDPropriedade { get; set; }
        public int codigo { get; set; }
        public string nome { get; set; }
        public string tipoPredSolo { get; set; }
        public Nullable<double> tamanho { get; set; }
        public Nullable<int> anoAbertura { get; set; }
        public Nullable<int> altitudeMedia { get; set; }
        [JsonIgnore]
        public DbGeography area_geo { get; set; }
        [JsonIgnore]
        public virtual ICollection<Problema> Problema { get; set; }
        public virtual Propriedade Propriedade { get; set; }
        [JsonIgnore]
        public virtual ICollection<AreaServico> AreaServico { get; set; }
        [JsonIgnore]
        public virtual ICollection<ParametroRecomendacao> ParametroRecomendacao { get; set; }
        [NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                var validador = new AreaValidation();

                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }

    public class Area_Viewer
    {
        public string GeoString { get; set; }
    }

}
