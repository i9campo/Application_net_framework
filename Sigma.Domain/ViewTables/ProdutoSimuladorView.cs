using System;
namespace Sigma.Domain.ViewTables
{
    /// <summary>
    /// <para>Essa classe será utilizada para unir ProdutoSimulador e Produto</para>
    /// </summary>
    public class ProdutoSimuladorProduto
    {
        public Guid objID { get; set; }
        public Guid IDSimulacao { get; set; }
        public Guid IDProduto { get; set; }
        public Guid? IDCultura { get; set; }
        public Guid? IDEstagioCultura { get; set; }
        public String IDUsuarioINC { get; set; }
        public String IDUsuarioALT { get; set; }
        public DateTime dateINC { get; set; }
        public DateTime? dateALT { get; set; }
        public string produto { get; set; }
        public float doseMin { get; set; }
        public float? doseMax { get; set; }
        public float? dap { get; set; }
        public double prnt { get; set; }
        public double p2o5 { get; set; }
        public double k2o { get; set; }
        public double s { get; set; }
        public int tipo { get; set; }
    }


    /// <summary>
    /// <para>Essa classe será utilizada para unir ProdutoSimulador e Produto e retorna a lista de ProdutosFertilizantes</para>
    /// </summary>
    public class ProdutoFertilizante
    {
        public Guid objID { get; set; }
        public Guid IDSimulacao { get; set; }
        public Guid IDProduto { get; set; }
        public Guid? IDCultura { get; set; }
        public Guid? IDEstagioCultura { get; set; }
        public String IDUsuarioINC { get; set; }
        public String IDUsuarioALT { get; set; }
        public DateTime dateINC { get; set; }
        public DateTime? dateALT { get; set; }
        public string produto { get; set; }
        public float doseMin { get; set; }
        public float? doseMax { get; set; }
        public float? dap { get; set; }
        public double prnt { get; set; }
        public double p2o5 { get; set; }
        public double k2o { get; set; }
        public double s { get; set; }
        public int tipo { get; set; }
        public string descricao { get; set; }
        public string nomeCultura { get; set; }
    }
}
