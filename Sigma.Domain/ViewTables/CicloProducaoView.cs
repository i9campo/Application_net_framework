using Newtonsoft.Json;
using Sigma.Domain.Entities;
using System;
namespace Sigma.Domain.ViewTables
{
    public class CicloProducaoView
    {
        public CicloProducaoView()
        {
            objID = Guid.NewGuid();

        }
        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public int ciclo { get; set; }
        public Guid IDCultura { get; set; }
        public Nullable<Guid> IDCulturaAnterior { get; set; }
        public bool inoculante { get; set; }
        public Nullable<Double> prodMinima { get; set; }
        public Nullable<Double> prodMaxima { get; set; }
        public Nullable<Double> prodReal { get; set; }
        public Nullable<DateTime> dataRealPlantio { get; set; }
        public Nullable<DateTime> dataPlantio { get; set; }
        public Nullable<DateTime> dataColheita { get; set; }
        public Nullable<Double> tamanho { get; set; }
        public string observacoes { get; set; }
        public Nullable<int> codigo { get; set; }
        public string identificacao { get; set; }
        public string tipo { get; set; }
        public string UnidadeMedida { get; set; }
        [JsonIgnore]
        public virtual Cultura Cultura { get; set; }

        [JsonIgnore]
        public virtual Cultura CulturaAnterior { get; set; }
        public string area { get; set; }
        public Nullable<int> dias { get; set; }
        public string geoJson { get; set; }
        public string StringGeoJson { get; set; }
        public String ID { get; set; }
        public String cultura { get; set; }
        public string jsonField { get; set; }
        public string centerLegend { get; set; }

    }
}
