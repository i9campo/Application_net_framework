using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Validation.CLS;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Sigma.Domain.Entities
{
    public class AreaServico : ISelfValidation
    {
        public AreaServico()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDArea { get; set; }
        public Guid IDSafra { get; set; }
        public Guid IDServico { get; set; }
        public Guid? IDCultura { get; set; }
        public Guid? IDProprietarioFatura { get; set; }
        public int codigo { get; set; }
        public int? numServico { get; set; }
        public int? contrato { get; set; }
        public double tamanho { get; set; }
        public string parametroTecnico { get; set; }
        public string parametroInterno { get; set; }
        public string resumoOperacional { get; set; }
        public string jsonField { get; set; }
        public string centerLegend { get; set; }
        public bool? revisado { get; set; }
        public DateTime? dataRevisao { get; set; }
        [JsonIgnore]
        public DbGeography geo { get; set; }

        public virtual Area Area { get; set; }
        public virtual Safra Safra { get; set; }
        public virtual Servico Servico { get; set; }
        public virtual Cultura Cultura { get; set; }
        public virtual Proprietario ProprietarioFatura { get; set; }
        [JsonIgnore]
        public ICollection<ParametroArea> ParametroArea { get; set; }
        [JsonIgnore]
        public ICollection<Simulacao> Simulacao { get; set; }
        [JsonIgnore]
        public ICollection<ParametroPropriedade> ParametroPropriedade { get; set; }
        [JsonIgnore]
        public virtual ICollection<AmostraFoliar> AmostraFoliar { get; set; }
        [JsonIgnore]
        public virtual ICollection<AnaliseSolo> AnaliseSolo { get; set; }
        [JsonIgnore]
        public virtual ICollection<CicloProducao> CicloProducao { get; set; }
        [JsonIgnore]
        public virtual ICollection<Corretivo> Corretivo { get; set; }
        [JsonIgnore]
        public virtual ICollection<Grid> Grid { get; set; }
        [JsonIgnore]
        public virtual ICollection<Imagem> Imagem { get; set; }

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                var validator = new AreaServicoValidation();
                this.ValidationResult = validator.Validate(this);
                return ValidationResult.IsValid;
            }
        }

    }

    public class AreaServicoViewer
    {
        public AreaServicoViewer()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDArea { get; set; }
        public Guid IDSafra { get; set; }
        public Guid IDServico { get; set; }
        public Guid? IDCultura { get; set; }
        public Guid? IDProprietarioFatura { get; set; }

        public string Area { get; set; }
        public string Safra { get; set; }
        public string Servico { get; set; }
        public string Cultura { get; set; }
        public string ProprietarioFatura { get; set; }
        public int codigo { get; set; }
        public int? numServico { get; set; }
        public int? contrato { get; set; }
        public double tamanho { get; set; }
        public string parametroTecnico { get; set; }
        public string parametroInterno { get; set; }
        public string resumoOperacional { get; set; }


        public bool? revisado { get; set; }
        public DateTime? dataRevisao { get; set; }
        public string geoJson { get; set; }
        
        public string jsonField { get; set; }
        public string centerLegend { get; set; }
    }

    public class AreaServicoGeo
    {
        public Guid objID { get; set; }
        public string coord { get; set; }

        public string jsonField { get; set; }
        public float tamanho { get; set; }
    }
}
