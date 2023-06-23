using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Infra.Data.Context.MapConfig
{
    public class ParametroPropriedadeConfig : EntityTypeConfiguration<ParametroPropriedade>
    {
        public ParametroPropriedadeConfig()
        {
            HasRequired(o => o.Propriedade)
                .WithMany(o => o.ParametroPropriedade)
                .HasForeignKey(o=>o.IDPropriedade);

            HasRequired(o => o.Safra)
               .WithMany(o => o.ParametroPropriedade)
               .HasForeignKey(o => o.IDSafra);

            Property(o => o.marcaEquipamento)
                .HasColumnType("text")
                .IsOptional();

            Property(o => o.observacao)
                .HasColumnType("text")
                .IsOptional();

            Property(o => o.fosforo)
                .HasMaxLength(50)
                .IsOptional();

            Property(o => o.enxofre)
                .HasMaxLength(50)
                .IsOptional();

            Property(o => o.nitrogenio)
                .HasMaxLength(50)
                .IsOptional();

            Property(o => o.empresasPreferenciais)
                .HasMaxLength(1000)
                .IsOptional();
           
            Property(o => o.tipoFosforo)
                .HasMaxLength(50)
                .IsOptional();

            
        }
    }
}
