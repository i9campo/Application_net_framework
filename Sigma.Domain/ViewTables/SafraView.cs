using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.ViewTables
{
    public class SafraView
    {
        public string objID { get; set; }
        public string descricao { get; set; }
        public Nullable<int> anoInicial { get; set; }
        public Nullable<int> anoFinal { get; set; }
    }
}
