using System;
namespace Sigma.Domain.ViewTables
{
    public class AreaServicoView
    {
        public Guid? objID { get; set; }
        public String ID { get; set; }
        public Guid? IDArea { get; set; }
        public Guid? IDSafra { get; set; }
        public Guid? IDServico { get; set; }
        public Guid? IDCultura { get; set; }
        public Guid? IDProprietarioFatura { get; set; }
        public Guid? IDProprietario { get; set; }
        public Guid? IDPropriedade { get; set; }
        public string parametroTecnico { get; set; }
        public string parametroInterno { get; set; }
        public string resumoOperacional { get; set; }
        public string safra { get; set; }
        public string proprietario { get; set; }
        public string propriedade { get; set; }
        public string nome { get; set; }
        public string area { get; set; }
        public string Servico { get; set; }
        public string geoJson { get; set; }
        public string Color { get; set; }
        public string PossuiArea { get; set; }
        public int? PossuiZona { get; set; }
        public int? PossuiParametroArea { get; set; }
        public int? codigo { get; set; }
        public int? contrato { get; set; }
        public int? numServico { get; set; }
        public Double? tamanho { get; set; }
        public Double? tamanhoArea { get; set; }
        public bool? revisado { get; set; }
        public bool? semciclo { get; set;  }
        public DateTime? dataRevisao { get; set; }
    }
}
