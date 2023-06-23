using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Entities
{
    public class Proprietario : ISelfValidation
    {
        public Proprietario()
        {
            objID = Guid.NewGuid();
        }
        public Guid objID { get; set; }
        public Guid IDEmpresa { get; set; }
        public string nome { get; set; }
        public string tipoProprietario { get; set; }
        public string pfpj { get; set; }
        public string endereco { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
        public string fone { get; set; }
        public string email { get; set; }
        public string infoAdicionais { get; set; }
        public string representante { get; set; }
        public string cpfRepresentante { get; set; }
        public string telefoneRepresentante { get; set; }
        public Nullable<int> ativo { get; set; }

        [JsonIgnore]
        public virtual Empresa Empresa { get; set; }

        [JsonIgnore]
        public virtual ICollection<AreaServico> AreaServico { get; set; }
        [JsonIgnore]
        public virtual ICollection<Propriedade> Propriedade { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProprietarioFornecedor> ProprietarioFornecedor { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid
        {
            get
            {
                var validador = new ProprietarioValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }

    public class Proprietario_Viewer
    {
        public Guid objID { get; set; }
        public Guid IDEmpresa { get; set; }
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
        public string ativo { get; set; }
    }

}
