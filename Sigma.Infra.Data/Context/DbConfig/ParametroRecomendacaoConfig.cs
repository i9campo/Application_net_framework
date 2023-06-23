using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Infra.Data.Context.MapConfig
{
   public class ParametroRecomendacaoConfig : EntityTypeConfiguration<ParametroRecomendacao>
    {
        public ParametroRecomendacaoConfig()
        {
            HasRequired(o => o.Safra)
            .WithMany(o => o.ParametroRecomendacao)
            .HasForeignKey(o => o.IDSafra);

            HasRequired(o => o.Area)
            .WithMany(o => o.ParametroRecomendacao)
            .HasForeignKey(o => o.IDArea);

            Property(o => o.tipo)
            .IsRequired()
            .HasMaxLength(10);

            Property(o => o.opcao)
                .IsOptional()
                .HasColumnType("int");

            Property(o => o.observacao)
                .IsOptional()
                .HasColumnType("text");

            Property(o => o.obsInterno)
                .IsOptional()
                .HasColumnType("text");

                

        }

    }
}
