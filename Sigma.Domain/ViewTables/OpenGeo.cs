using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.ViewTables
{
    /// <summary>
    /// Essa classe será utilizada para carregar os dados geograficos no zoneamento. 
    /// </summary>

    public class OpenGeo
    {
        public class GeoOBJ
        {
            public Guid? ID { get; set; }
            public Guid? objID { get; set;  }
            public Guid? IDArea { get; set; }
            public Guid? IDSafra { get; set; }
            public Guid? IDServico { get; set; }
            public Guid? IDCultura { get; set; }
            public Guid? IDProprietarioFatura { get; set; }
            public string safra { get; set; }
            public string proprietario { get; set; }
            public string propriedade { get; set; }
            public string area { get; set;  }
            public string nome { get; set; }
            public string servico { get; set; }
            public int? numServico { get; set; }
            public int? contrato { get; set; } 
            public Guid? IDAreaServico { get; set; }
            public string ZONA { get; set; }
            public Double? TAMANHO { get; set; }
            public int? CODIGO { get; set; }
            public string ZN_AREA { get; set; }
            public string ZN_CODIGO { get; set; }
            public string geoJson { get; set; }
            public string jsonField { get; set; }
            public string type { get; set; }
            public IEnumerable<string> Rotulo { get; set; }
        }
    }


}
