using System;
namespace Sigma.Domain.ViewTables
{
    public class ParametroAreaView
    {
        public Guid objID { get; set; }
        public Guid IDAreaServico { get; set; }
        public Guid? IDUltimaCultura { get; set; }
        public string areaArrendada { get; set; }
        public int? tempoRestante { get; set; }
        public string condicaoAtual { get; set; }
        public string tipoManejo { get; set; }
        public string fosfatoUtilizado { get; set; }
        public int? anoAbertura { get; set; }
        public DateTime? inicioPlantio { get; set; }
        public double? produtividadeArea { get; set; }
        public string perfilSolo { get; set; }
        public string ultimaGessagem { get; set; }
        public string doseGessagem { get; set; }
        public string taxaAplicacaoGessagem { get; set; }
        public string CondAplicGessagem { get; set; }
        public string correcaoSolo { get; set; }
        public string ultimaCalagem { get; set; }
        public string culturaValue { get; set; }
        public string tipoCalcario { get; set; }
        public string doseCalagem { get; set; }
        public string taxaAplicacaoCalagem { get; set; }
        public string CondAplicCalagem { get; set; }
        public string equipamentoIncorporado { get; set; }
        public string observacao { get; set; }
        public string efetuouFosfatagem { get; set; }
        public string anoUltimaFosfatagem { get; set; }
        public string formaAplicacao { get; set; }
        public string minaPreferencial { get; set; }
        public DateTime? dataInclusao { get; set; }
        public string proximaCultura { get; set; }
        public string plantarHF { get; set; }
        public string dosePrevistaGesso { get; set; }
        public string corrigirFosforo { get; set; }
        public string observacaoComplementar { get; set; }
        public double? v { get; set; }
        public string recomendacaoFosforo { get; set; }
        public string nomeCultura { get; set; }
        public string utilizarGesso { get; set; }

        public string AnoCalagem { get; set; }
    }
}
