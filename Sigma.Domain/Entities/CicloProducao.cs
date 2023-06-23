using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
namespace Sigma.Domain.Entities
{
    public class CicloProducao : ISelfValidation
    {
        public CicloProducao()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public Guid? IDCultura { get; set; }
        public Guid? IDVariedadeCultura { get; set; }
        public Guid? IDCulturaAnterior { get; set; }

        /// <summary>
        /// CP : CicloProdução
        /// CI : CicloIntermediário
        /// RT : Resteva
        /// </summary>
        
        public string tipo { get; set; }
        public int ciclo { get; set; }
        public string identificacao { get; set; }
        public double tamanho { get; set; }
        public Nullable<DateTime> dataPlantio { get; set; }
        public Nullable<DateTime> dataRealPlantio { get; set; }
        public Nullable<DateTime> dataColheita { get; set; }
        public Nullable<double> prodMinima { get; set; }
        public Nullable<double> prodMaxima { get; set; }
        public Nullable<double> prodReal { get; set; }
        public string observacoes { get; set; }
        public string parametroTecnico { get; set; }
        public string parametroInterno { get; set; }
        public bool inoculante { get; set; }
        public int codigo { get; set; }
        public string jsonField { get; set; }
        public string centerLegend { get; set; }

        [JsonIgnore]
        public DbGeography geo { get; set; }

        public virtual AreaServico AreaServico { get; set; }

        public virtual Cultura Cultura { get; set; }

        public virtual Cultura CulturaAnterior { get; set; }

        public virtual VariedadeCultura VariedadeCultura { get; set; }

        [JsonIgnore]
        public virtual ICollection<Fertilizante> Fertilizante { get; set; }


        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new CicloProducaoValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }

    }
    public class CicloPost
    {
        public Guid IDAreaServico { get; set; }
        public string tipo { get; set; }
        public string identificacao { get; set; }
        public float tamanho { get; set; }
        public string geostring { get; set; }
        public string centerLegend { get; set; }
        public string jsonField { get; set;  }
    }
    public class CicloViewer
    {
        public AreaServico oAreaServico { get; set; }
        public Cultura oCultura { get; set; }
        public VariedadeCultura oVariedadeCultura { get; set; }
        public Cultura oCulturaAnterior { get; set; }
        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public Guid? IDCultura { get; set; }
        public Guid? IDVariedadeCultura { get; set; }
        public Guid? IDCulturaAnterior { get; set; }
        public string tipo { get; set; }
        public int ciclo { get; set; }
        public string identificacao { get; set; }
        public double tamanho { get; set; }
        public DateTime? dataPlantio { get; set; }
        public DateTime? dataRealPlantio { get; set; }
        public DateTime? dataColheita { get; set; }
        public double? prodMinima { get; set; }
        public double? prodMaxima { get; set; }
        public double? prodReal { get; set; }
        public double? observacoes { get; set; }
        public string parametroTecnico { get; set; }
        public string parametroInterno { get; set; }
        public bool inoculante { get; set; }
        public int codigo { get; set; }
        public string geoJson { get; set; }
        public string jsonField { get; set; }
        public string centerLegend { get; set; }
    }

}
