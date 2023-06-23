using System;
using System.ComponentModel;

namespace Sigma.Domain.ViewTables
{
    public class CorretivoView
    {
        public String ID { get; set; }
        public Guid? objID { get; set; }
        public Guid? IDArea { get; set; }
        public Guid? IDAreaServico { get; set; }
        public Guid? IDGrid { get; set; }
        public Guid? IDFornecedor { get; set; }
        public Guid? IDProprietario { get; set; }

        //---------------------------------------//------------------------------------------------//
        public string Area { get; set; }
        public string Cultura { get; set; }
        public string Corretivo { get; set; }
        public string Fertilizante { get; set; }
        public string Grid { get; set; }
        public string descricao { get; set; }
        public string Proprietario { get; set; }
        public string Propriedade { get; set; }
        public string Safra { get; set; }
        public string ZM { get; set; }
        public double TamanhoZM { get; set; }

        //---------------------------------------//------------------------------------------------//
        public int? marcado { get; set; }
        public int? ordemCiclo { get; set; }
        public int? opcao { get; set; }
        public int? qtd_opcao { get; set; }
        public int? opcaoMarcado { get; set; }
        public int? opcaoPerfil { get; set; }
        public int? opcaoCorretivo { get; set; }
        public int? opcaoFertilizante { get; set; }
        public int? retornoGridOrAreaServico { get; set; }
        public int? perfil { get; set; }
        public String Opcoes { get; set; }
        public String ExistOpcao { get; set; }
        //---------------------------------------//------------------------------------------------//
        [DefaultValue(0)]
        public double? custo { get; set; }
        [DefaultValue(0)]
        
        public double eficiencia { get; set; }
        [DefaultValue(0)]
        public double? prnt { get; set; }
        [DefaultValue(0)]
        public double? qtde { get; set; }
        [DefaultValue(0)]
        public double KgHa { get; set; }
        [DefaultValue(0)]
        public double? Ha { get; set; }


        //---------------------------------------//------------------------------------------------//
        [DefaultValue(0)]
        public double? perCaO { get; set; }
        [DefaultValue(0)]
        public double? perMgO { get; set; }
        [DefaultValue(0)]
        public double? perP2O5 { get; set; }
        [DefaultValue(0)]
        public double? perK2O { get; set; }
        [DefaultValue(0)]
        public double? perCa { get; set; }
        [DefaultValue(0)]
        public double? perMg { get; set; }
        [DefaultValue(0)]
        public double? perS { get; set; }

        //---------------------------------------//------------------------------------------------//
        [DefaultValue(0)]
        public double? b { get; set; }
        [DefaultValue(0)]
        public double? zn { get; set; }
        [DefaultValue(0)]
        public double? fe { get; set; }
        [DefaultValue(0)]
        public double? mn { get; set; }
        [DefaultValue(0)]
        public double? cu { get; set; }
        [DefaultValue(0)]
        public double? co { get; set; }
        [DefaultValue(0)]
        public double? momicro { get; set; }
        [DefaultValue(0)]
        public double? Ca { get; set; }
        [DefaultValue(0)]
        public double? Mg { get; set; }
        [DefaultValue(0)]
        public double? K { get; set; }
        [DefaultValue(0)]
        public double? P { get; set; }
        [DefaultValue(0)]
        public double? S { get; set; }
        [DefaultValue(0)]
        public double? V { get; set; }

        //---------------------------------------//------------------------------------------------//
        // Variáveis relacionada a analise. 
        //public double? pmehl { get; set; }
        //public double? pres { get; set; }
        //public double? ctc { get; set; }
        //public double? ctcca { get; set; }
        //public double? ctcmg { get; set; }
        //public double? ctck { get; set; }

        // Variáveis relacionada com a tabela de quantidades adiconadas no solo. 
        [DefaultValue(0)]
        public double? mediaCa { get; set; }
        [DefaultValue(0)]
        public double? mediaCaO { get; set; }
        [DefaultValue(0)]
        public double? mediaMgO { get; set; }
        [DefaultValue(0)]
        public double? mediaP2O5 { get; set; }
        [DefaultValue(0)]
        public double? mediaK2O { get; set; }
        [DefaultValue(0)]
        public double? mediaMg { get; set; }
        [DefaultValue(0)]
        public double? mediaP { get; set; }
        [DefaultValue(0)]
        public double? mediaK { get; set; }
        [DefaultValue(0)]
        public double? mediaS { get; set; }
        [DefaultValue(0)]
        public bool? marcar { get; set; }


        //---------------------------------------//------------------------------------------------//
        [DefaultValue(0)]
        public double? CaMg { get; set; }
        [DefaultValue(0)]
        public double? CaK { get; set; }
        [DefaultValue(0)]
        public double? MgK { get; set; }
        [DefaultValue(0)]
        public double? CaMgK { get; set; }
        [DefaultValue(0)]
        public double? CTCCA { get; set; }
        [DefaultValue(0)]
        public double? CTCMG { get; set; }
        [DefaultValue(0)]
        public double? CTCK { get; set; }
        [DefaultValue(0)]
        public double? somatorioP2O5 { get; set; }
        [DefaultValue(0)]
        public double? somatorioK2O { get; set; }
        [DefaultValue(0)]
        public double? sCaO { get; set; }
        [DefaultValue(0)]
        public double? sMgO { get; set; }
        [DefaultValue(0)]
        public double? sP2O5 { get; set; }
        [DefaultValue(0)]
        public double? sK2O { get; set; }
        [DefaultValue(0)]
        public double? sS { get; set; }
    }
    public class Options
    {
        public int? Opcoes { get; set; }
        public int? ExistOpcao { get; set; }
        public bool Marcado { get; set; }
        public int? qtd { get; set; }
    }
    public class ResultadoCorrecao
    {
        public char ObjID  { get; set;} 
        public char Area { get; set; }
        public char  Grid { get; set; } 
        public string Descricao { get; set; }
        public double?  Qtde { get; set; }
        public double? Total { get; set; }
        public double? Tamanho { get; set; } 
        public double?  Ctc { get; set; }
        public double? KAnalise { get; set; }
	    public double? CaInicial { get; set; }
        public double? MgInicial { get; set; }
        public double? KInicial { get; set; }
        public double?  PInicial { get; set; }
        public double? SInicial { get; set; }
        public double? VInicial { get; set; }
	    public double? CaFinal { get; set; }
        public double? MgFinal { get; set; }
        public double? KFinal { get; set; }
        public double? PFinal { get; set; }
        public double?  SFinal { get; set; }
        public double? VFinal { get; set; }
	    public double? relCaMg { get; set; }
        public double? relCaK { get; set; }
        public double? relMgK { get; set; }
        public double? relCaMgK { get; set; }
        public double? CTCCa { get; set; }
        public double? CTCMg { get; set; }
        public double? CTCK { get; set; }
        public double? CTCAl { get; set; }
	    public double? BFinal { get; set; }
        public double? ZnFinal { get; set; }
        public double? FeFinal { get; set; } 
        public double? MnFinal { get; set; } 
        public double? CuFinal { get; set; }
        public double? CoFinal { get; set; }
        public double? MoFinal { get; set; }
    }
    public class MediaCorretivo
    {
        public string IDGrid { get; set; }
        public double? Qtde { get; set; }
        public double? Ca { get; set; }
        public double? Mg { get; set; }
        public double? K { get; set; }
        public double? P { get; set; }
        public double? S { get; set; }
        public double? Tamanho { get; set; }
    }
}
