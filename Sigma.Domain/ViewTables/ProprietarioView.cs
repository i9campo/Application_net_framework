using System;
namespace Sigma.Domain.ViewTables
{
    public class ProprietarioView
    {
        public Guid objID { get; set; }
        public string nome { get; set; }
        public string ie { get; set; }
        public string rg { get; set; }
        public string tipoProprietario { get; set; }
        public string pfpj { get; set; }
        public string endereco { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
        public string fone { get; set; }
        public string fax { get; set; }
        public string site { get; set; }
        public string email { get; set; }
        public string infoAdicionais { get; set; }
        public string representante { get; set; }
        public string cpfRepresentante { get; set; }
        public string telefoneRepresentante { get; set; }
        public Nullable<int> ativo { get; set; }
    }


    public class BNGProprietario
    {
        public string objID { get; set; }
        public string nome { get; set; }
    }
}
