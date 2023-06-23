using System;
namespace Sigma.Domain.ViewTables
{
    public class AreaView
    {
        public Guid objID { get; set; }
        public Guid IDPropriedade { get; set; }
        public string nome { get; set; }
        //public string nomeTipoArea { get; set; }
        public string nomePropriedade { get; set; }
        public string tipoPredSolo { get; set; }
        public string geoJson { get; set; }
        public int codigo { get; set; }
        public Nullable<double> tamanho { get; set; }
        public Nullable<int> anoAbertura { get; set; }
        public Nullable<int> altitudeMedia { get; set; }
        public String contemgeo { get; set; }
    }

    public class AreaGrid
    {
        public Guid objID { get; set; }
        public Guid IDPropriedade { get; set; }
        public string nome { get; set; }
        public int codigo { get; set; }
    }

    public class AreaPropriedadeView
    {
        public Guid objArea { get; set;}
        public Guid IDPropriedade { get; set; }
        public string nomeArea { get; set; }
        public string nomePropriedade { get; set;}
    }

    public class BNGAreaView
    {
        public string objID { get; set; }
        public string nome { get; set; }
    }
}
