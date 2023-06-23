using System;
using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class AmostraConfig : EntityTypeConfiguration<Amostra>
    {
        public AmostraConfig()
        {
            Property(o => o.descricao)
                .IsRequired()
                .HasMaxLength(50);


            HasRequired(o => o.Cultura)
                    .WithMany(o=>o.Amostra)
                    .HasForeignKey(o => o.IDCultura);
        }
    }
}
