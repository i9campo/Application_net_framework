using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Infra.Data.Context.MapConfig
{
    public class UnidadeDeLaboratorioConfig : EntityTypeConfiguration<UnidadeDeLaboratorio>
    {
        public UnidadeDeLaboratorioConfig()
        {
            HasRequired(o => o.Laboratorio)
                .WithMany(o => o.UnidadeDeLaboratorio)
                .HasForeignKey(o => o.IDLaboratorio);
        }
    }
}
