using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.ViewTables
{
    public class GridEditView
    {
        public Guid objID { get; set; }
        public Guid? IDAreaServico { get; set; }
        public string descricao { get; set; }
        public Double? tamanho { get; set; }
        public int? codigo { get; set; }
        public string jsonField { get; set; }
        public string nome { get; set; }
        public string value { get; set; }
        public string delField { get; set; }
    }
}
