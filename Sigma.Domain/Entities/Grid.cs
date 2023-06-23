using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Security.Policy;

namespace Sigma.Domain.Entities
{
    public class Grid : ISelfValidation
    {
        public Grid()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public string descricao { get; set; }
        public double tamanho { get; set; }
        public int codigo { get; set; }
        public string jsonField     { get; set; }
        public string centerLegend { get; set; }
        [JsonIgnore]
        public DbGeography geo { get; set; }

        [JsonIgnore]
        public virtual AreaServico AreaServico { get; set; }

        [JsonIgnore]
        public virtual ICollection<Corretivo> Corretivo { get; set; }

        [JsonIgnore]
        public ICollection<AnaliseSolo> AnaliseSolo { get; private set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new GridValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
    public class GridViewer
    {
        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public string descricao { get; set; }
        public decimal tamanho { get; set; }
        public int codigo { get; set; }
        public string geoJson { get; set; }
        public string geoString { get; set; }
        public string jsonField { get; set; }
        public string centerLegend { get; set; }
        public bool existAnalise { get; set; }
    }
    public class SplitPolyViewer
    {
        public string poly1 { get; set; }
        public string poly2 { get; set; }
    }


    public class GeoJsonSplitPoly
    {
        public string geoJson { get; set; }
        public string geoString { get; set; }
    }
}
