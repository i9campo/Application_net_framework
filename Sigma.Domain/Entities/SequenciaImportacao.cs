using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;

namespace Sigma.Domain.Entities
{
    public class SequenciaImportacao : ISelfValidation
    {
        public SequenciaImportacao()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDSafra { get; set; }
        public Guid IDLaboratorio { get; set; }
        public string Area { get; set; }
        public string Grid { get; set; }
        public string Nome { get; set; }
        public string NomeLaboratorio { get; set; }
        public string NPonto { get; set; }
        public string TpSolo { get; set; }
        public string Compac { get; set; }
        public string PHCaCl2 { get; set; }
        public string MO { get; set; }
        public string PMeHl { get; set; }
        public string PRes { get; set; }
        public string K { get; set; }
        public string S { get; set; }
        public string Ca { get; set; }
        public string Mg { get; set; }
        public string Al { get; set; }
        public string CTC { get; set; }
        public string Argila { get; set; }
        public string B { get; set; }
        public string Zn { get; set; }
        public string Fe { get; set; }
        public string Mn { get; set; }
        public string Cu { get; set; }
        public string Co { get; set; }
        public string Momicro { get; set; }

        public string umphcacl2 { get; set; }
        public string ummo { get; set; }
        public string umpmehl { get; set; }
        public string umpres { get; set; }
        public string umk2o { get; set; }
        public string ums { get; set; }
        public string umca { get; set; }
        public string ummg { get; set; }
        public string umal { get; set; }
        public string umctc { get; set; }
        public string umargila { get; set; }
        public string umb { get; set; }
        public string umzn { get; set; }
        public string umfe { get; set; }
        public string ummn { get; set; }
        public string umcu { get; set; }
        public string umco { get; set; }
        public string ummomicro { get; set; }
        public virtual Safra Safra { get; set; }
        public virtual Laboratorio Laboratorio { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validador = new SequenciaImportacaoValidation();
                this.ValidationResult = validador.Validate(this);
                return ValidationResult.IsValid;
            }
        }

    }

    public class FileCSV
    {
        public string ArquivoB64 { get; set; }
        public string extension { get; set; }
    }
}
