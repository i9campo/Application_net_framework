using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.ViewTables
{
    public class PropriedadeView
    {   
        public Guid objID           { get; set; }
        public Guid IDAreaServico   { get; set; }
        public Guid? IDPropriedade   { get; set; }
        public Guid? IDServico       { get; set; }
        public Guid? IDArea          { get; set; }
        public Guid? IDSafra         { get; set; }
        public String Descricao     { get; set; }
        public String nome { get; set; }
        public String type { get; set; }

    }

    public class BNGPropriedade
    {
        public string objID { get; set; }
        public string nome { get; set; }
    }
}
