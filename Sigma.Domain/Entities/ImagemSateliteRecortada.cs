using Newtonsoft.Json;
using Sigma.Domain.Validation.CLS;
using System.Data.Entity.Spatial;
using System;
using FluentValidation.Results;
using Sigma.Domain.Interfaces;
namespace Sigma.Domain.Entities
{
    public class ImagemSateliteRecortada : ISelfValidation
    {
        public ImagemSateliteRecortada() { 
            objID = Guid.NewGuid(); 
        }

        public Guid objID { get; set; }
        public string orbita { get; set; }
        public string banda { get; set; }
        public string dateIMG { get; set; }
        public string satelite { get; set; }
        public string extension { get; set; }
        public DbGeography polyIMG { get; set; }
        public bool visualizar { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new ImagemSateliteRecortadaValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
