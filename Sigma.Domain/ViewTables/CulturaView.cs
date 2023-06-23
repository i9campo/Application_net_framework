using System;
namespace Sigma.Domain.ViewTables
{
    public class CulturaView
    {
        public Guid objID { get; set; }
        public Guid IDUnidadeMedida { get; set; }
        public string nome { get; set; }
        public string nitrog { get; set; }
        public double nitrogenio { get; set; }
        public int ciclo { get; set; }
        public int decomposicao { get; set; }
        public double nSimb { get; set; }
        public int nSimbMedida { get; set; }
        public string UnidadeMedida { get; set; }
    }
}
