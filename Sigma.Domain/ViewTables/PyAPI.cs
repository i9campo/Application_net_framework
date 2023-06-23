using Microsoft.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.ViewTables
{
    public class PostGeneratePoint
    {
        /// <summary> Representa a lista de poligonos em geostring. </summary>
        public IEnumerable<string> lst { get; set; }
        public string poligon { get; set; }
        /// <summary> Representa a quantidade de sub-amostra. </summary>
        public int sub { get; set; }
        public int orb { get; set; }
    }


    public class PyAPI
    {
        public List<string> poly { get; set; }
        public List<string> point { get; set; } 
    }


    public class PyReturn
    {
        public string type { get; set; }
        public List<List<Decimal>> coordinates { get; set; }
    }

    public class PyReturnPoint
    {
        public string type { get; set; }
        public List<Decimal> coordinates { get; set; }
    }



    public class RetornoPonto
    {
        public string jsonField { get; set; }
        public string geoJson { get; set; }
    }


    public class LstPontos
    {
        public string pontos { get; set; }
        public float distancia { get; set; }
    }


}
