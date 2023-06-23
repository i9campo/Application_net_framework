using System;
namespace Sigma.Domain.ViewTables
{
    public class FertilizanteView
    {
        public Guid? objID { get; set; }
        public Guid? IDCicloProducao { get; set; }
        public Guid? IDFornecedor { get; set; }
        public Guid? IDEstagioCultura { get; set; }
        public double? dias { get; set; }
        public string unidadeMedida { get; set; }
        public string estagio { get; set; }
        public bool? foliar { get; set; }
        public string nome { get; set; }
        public int? daedap { get; set; }
        public int? marcado { get; set; }
        public int? opcao { get; set; }
        public int? opcaoMarcada { get; set; }
        public double? qtde { get; set; }
        public double? eficiencia { get; set; }
        public double? densidade { get; set; }
        public double? custo { get; set; }
        public double? n { get; set; }
        public double? p2o5 { get; set; }
        public double? k2o { get; set; }
        public double? ca { get; set; }
        public double? mg { get; set; }
        public double? s { get; set; }
        // MICROS
        public double? b { get; set; }
        public double? zn { get; set; }
        public double? cu { get; set; }
        public double? mn { get; set; }
        public double? co { get; set; }
        public double? mo { get; set; }

    }
}
