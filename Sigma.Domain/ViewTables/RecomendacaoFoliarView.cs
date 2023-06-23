using System; 
namespace Sigma.Domain.ViewTables
{
    public class RecomendacaoFoliarView
    {
        public RecomendacaoFoliarView()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDCultura { get; set; }
        public string codigo { get; set; }
        public string descritivo { get; set; }
        public string elemento { get; set; }
        public bool excecao { get; set; }
    }
}
